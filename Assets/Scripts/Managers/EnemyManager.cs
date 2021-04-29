using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // ===== Set in inspector =====
    // Enemy Prefabs
    public GameObject redEnemyPrefab, blueEnemyPrefab, yellowEnemyPrefab;

    // ===== Set at Start() =====
    public Wave currentWave;
    public GameObject enemies;
    private float spawnTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        enemies = new GameObject("enemies");

        currentWave = CreateWaves();
        spawnTimer = currentWave.SpawnDelay;     // Allows the first enemy to spawn immediately
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<GameManager>().currentMenuState == MenuState.game) {
            // The current wave has been cleared
            if(currentWave.HasCleared) {
                // If there is a next wave, that wave is the new current wave
                if(currentWave.NextWave != null) {
                    currentWave = currentWave.NextWave;
                    spawnTimer = currentWave.SpawnDelay;
                }
				// No Waves left, the game is over
				else {
                    gameObject.GetComponent<GameManager>().ChangeMenuState(MenuState.gameOver);
				}
            }
			else {
                CheckEnemies();
            }
        }
    }

	void FixedUpdate()
	{
        if(gameObject.GetComponent<GameManager>().currentMenuState == MenuState.game
            && currentWave.HasSpawned
            && !currentWave.HasCleared) {
            // Increments timer
            spawnTimer += Time.deltaTime;

            if(spawnTimer >= currentWave.SpawnDelay
                && currentWave.EnemiesSpawned < currentWave.EnemyCount) {
                // Resets timer and spawns an enemy
                spawnTimer = 0.0f;
                SpawnEnemy(currentWave.EnemyPrefab);
            }

            UpdateEnemies();
        }
    }

    /// <summary>
    /// Starts the current wave if it hasnt been spawned, nor spawned
    /// </summary>
    public void StartWave()
	{
        if(!currentWave.HasCleared
            && !currentWave.HasSpawned) {
            currentWave.StartSpawn();
        }
    }

    /// <summary>
    /// A method to hold all lines of code dealing with creating waves
    /// </summary>
    /// <returns>The first wave</returns>
    Wave CreateWaves()
    {
        Wave wave6 = new Wave("Wave 6", blueEnemyPrefab, 4, 1.0f);
        Wave wave5 = new Wave("Wave 5", yellowEnemyPrefab, 6, 0.5f, wave6);
        Wave wave4 = new Wave("Wave 4", redEnemyPrefab, 4, 0.5f, wave5);
        Wave wave3 = new Wave("Wave 3", yellowEnemyPrefab, 4, 0.5f, wave4);
		Wave wave2 = new Wave("Wave 2", blueEnemyPrefab, 2, 1.0f, wave3);
        Wave wave1 = new Wave("Wave 1", redEnemyPrefab, 3, 0.5f, wave2);

        return wave1;
    }

    /// <summary>
    /// Creates an enemy of the given prefab and places it in the scene
    /// </summary>
    /// <param name="enemy">The prefab of the to-be created enemy</param>
    void SpawnEnemy(GameObject enemy)
    {
		// Calculate the position of the entrance checkpoint, zero-ing out its y-value
		GameObject spawnPoint = gameObject.GetComponent<LevelManager>().level.transform.Find("checkpoints").transform.Find("entrance").gameObject;
        Vector3 position = spawnPoint.transform.position;
        position.y = 0.0f;
        // Creates an enemy and adds it to the parent GO
        GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity, enemies.transform);
        newEnemy.name = "enemy" + currentWave.EnemiesSpawned;
        newEnemy.GetComponent<Enemy>().currentCheckpoint = spawnPoint.GetComponent<Checkpoint>().nextCheckpoint;

        // Updates the Wave object that an enemy was spawned from it
        currentWave.EnemySpawned();
    }

    /// <summary>
    /// Loops through all enemies, moving them accordingly
    /// </summary>
	void UpdateEnemies()
	{
		foreach(Transform enemyTransform in enemies.transform) {
            enemyTransform.gameObject.GetComponent<Enemy>().Move();
		}
	}

	/// <summary>
	/// Checks all enemies to see if they lost all health or reached the exit
	/// </summary>
	void CheckEnemies()
	{
        List<GameObject> destroyedEnemies = new List<GameObject>();
        Vector3 exitPos = gameObject.GetComponent<LevelManager>().level.transform.Find("checkpoints").transform.Find("exit").position;

		// Loops through each enemy child, checking if they need to be removed
        foreach(Transform enemyTransform in enemies.transform) {
			GameObject enemy = enemyTransform.gameObject;
			// If the enemy has no health
			if(enemy.GetComponent<Enemy>().health <= 0.0f) {
                gameObject.GetComponent<GameManager>().money += enemy.GetComponent<Enemy>().worth;
                destroyedEnemies.Add(enemy);
            }
			// If the enemy has reached the exit
			else if(Vector3.Distance(enemy.transform.position, exitPos) <= 0.5f) {
				gameObject.GetComponent<GameManager>().health -= enemy.GetComponent<Enemy>().damage;
                destroyedEnemies.Add(enemy);
            }
		}

        // At the end, loops through and destroys each enemy in the list
        foreach(GameObject enemy in destroyedEnemies) {
            Destroy(enemy);
            currentWave.EnemyRemoved();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Set in inspector
    public GameObject redEnemyPrefab;
    public GameObject blueEnemyPrefab;

    // Set at Start()
    public GameObject enemies;
    Wave currentWave;
    public string currentWaveName;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        enemies = new GameObject("enemies");

        currentWave = CreateWaves();
        currentWaveName = currentWave.Name;
        timer = currentWave.SpawnDelay;     // Allows the first enemy to spawn immediately
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<StateManager>().currentMenuState == MenuState.game) {
            if(Input.GetKeyDown(KeyCode.Space)
            && !currentWave.HasCleared
            && !currentWave.HasSpawned) {
                currentWave.StartSpawn();
            }

            // The current wave has been cleared
            if(currentWave.HasCleared) {
                // If there is a next wave, that wave is the new current wave
                if(currentWave.NextWave != null) {
                    currentWave = currentWave.NextWave;
                    currentWaveName = currentWave.Name;
                    timer = currentWave.SpawnDelay;
                }
                // No Waves left, the game is over
                else
                    gameObject.GetComponent<StateManager>().ChangeMenuState(MenuState.gameOver);
            }
        }
    }

	void FixedUpdate()
	{
        if(currentWave.HasSpawned
            && !currentWave.HasCleared) {
            // Increments timer
            timer += Time.deltaTime;

            if(timer >= currentWave.SpawnDelay
                && enemies.transform.childCount != currentWave.WaveCount) {
                // Resets timer, spawns an enemy
                timer = 0.0f;
                SpawnEnemy(currentWave.EnemyPrefab);
            }
        }
    }

    /// <summary>
    /// A method to hold all lines of code dealing with creating waves
    /// </summary>
    /// <returns>The first wave</returns>
    Wave CreateWaves()
	{
        Wave wave2 = new Wave("Wave 2", redEnemyPrefab, 2, 2.0f);
        Wave wave1 = new Wave("Wave 1", blueEnemyPrefab, 4, 1.0f, wave2);

        return wave1;
    }

    /// <summary>
    /// Creates an enemy of the given prefab and places it in the scene
    /// </summary>
    /// <param name="enemy">The prefab of the to-be created enemy</param>
    void SpawnEnemy(GameObject enemy)
    {
        // Creates an enemy and adds it to the empty GO
        GameObject spawnPoint = GameObject.Find("entrance");
        Vector3 position = spawnPoint.transform.position;
        position.y = 0.0f;
        GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity, enemies.transform);
        newEnemy.name = "enemy" + enemies.transform.childCount;

        // Updates the Wave object that an enemy was spawned from it
        currentWave.EnemySpawned();
    }

    void ClearEnemies()
	{
        // Loops through each enemy child, deactivating any that has collided with the exit
        for(int child = 0; child < enemies.transform.childCount; child++) {
            //if(enemies.transform.GetChild(child).gameObject)
		}
	}
}

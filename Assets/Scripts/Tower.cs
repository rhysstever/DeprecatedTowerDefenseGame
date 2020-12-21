using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTier
{
	Basic,
	Advanced,
	Expert
}

public enum TowerType
{
	Air,
	Water,
	Earth,
	Fire
}

public class Tower : MonoBehaviour
{
    // Set in inspector
    public TowerTier towerTier;
    public TowerType[] towerTypes;
    public float damage;
    public float attackSpeed;
    public float range;
    
    // Set at Start()
    float shotTimer;
	public GameObject currentEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        shotTimer = AttackSpeedToTimer();    // the tower can shoot immediately
		currentEnemy = null;
    }

    // Update is called once per frame
    void Update()
    {
		TargetEnemy();
		RotateToCurrentEnemy();
    }

	void FixedUpdate()
	{
		if(currentEnemy != null
			&& shotTimer >= AttackSpeedToTimer())
			Shoot(currentEnemy);
		else
			shotTimer += Time.deltaTime;
	}

	/// <summary>
	/// Loops through all enemies in the scene, finding any within range
	/// </summary>
	void TargetEnemy()
	{
		currentEnemy = null;
		GameObject enemies = GameObject.Find("enemies");
		for(int child = enemies.transform.childCount - 1; child >= 0; child--) {
			if(Vector3.Distance(
				gameObject.transform.position,
				enemies.transform.GetChild(child).position) <= range)
				currentEnemy = enemies.transform.GetChild(child).gameObject;
		}
	}

	/// <summary>
	/// Deals damage to a targeted enemy
	/// </summary>
	/// <param name="enemy">The enemy to be shot</param>
    void Shoot(GameObject enemy)
	{
		// Checks that the enemy is not null and is actually an enemy
		if(enemy == null
			|| enemy.GetComponent<Enemy>() == null) {
			Debug.Log("This is not an enemy!");
			return;
		}

		// Deals damage to enemy, resets timer
		enemy.GetComponent<Enemy>().health -= damage;
		shotTimer = 0.0f;
	}

	/// <summary>
	/// Calculate the correct rotation, zero-ing out the x and z values so it does not tilt in any direction
	/// </summary>
	void RotateToCurrentEnemy()
	{
		if(currentEnemy != null) {
			gameObject.transform.LookAt(currentEnemy.transform);
			Quaternion newQuat = gameObject.transform.rotation;
			newQuat.x = 0.0f;
			newQuat.z = 0.0f;
			gameObject.transform.rotation = newQuat;
		}
	}

	/// <summary>
	/// Converts the tower's attackSpeed value to a timer
	/// </summary>
	/// <returns>The timer the tower will use to calculate when it can shot</returns>
	float AttackSpeedToTimer()
	{
		float initialTimer = 3.0f;
		initialTimer -= (attackSpeed * 0.01f);
		return initialTimer;
	}
}

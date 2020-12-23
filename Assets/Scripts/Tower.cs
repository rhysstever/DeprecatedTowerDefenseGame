using System;
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
	None,
	Air			=    1,
	Earth		=   20,
	Fire		=  300, 
	Water		= 4000,
	Lightning	=  301,
	Ice			= 4001,
	Quicksand	= 4020,
	Volcano		=  320
}

public class Tower : MonoBehaviour
{
    // Set in inspector
    public TowerTier towerTier;
    public TowerType towerType;
    public float damage;
    public float attackTime;
    public float range;
	public int numOfTargets;

	// Set at Start()
    float shotTimer;
	public GameObject currentEnemy;
    
	// Set on creation
	public GameObject tile;
    
	// Start is called before the first frame update
    void Start()
    {
        shotTimer = attackTime;    // the tower can shoot immediately
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
			&& shotTimer >= attackTime)
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

		// Deals damage to enemy, applys the attack modifier's affliction to the enemy
		enemy.GetComponent<Enemy>().TakeDamage(damage);
		if(gameObject.GetComponent<Affliction>() != null) {
			Type type = Type.GetType("Affliction");
			enemy.AddComponent(type);
			enemy.GetComponent<Affliction>().type = gameObject.GetComponent<Affliction>().type;
			enemy.GetComponent<Affliction>().totalDuration = gameObject.GetComponent<Affliction>().totalDuration;
			enemy.GetComponent<Affliction>().currentTime = gameObject.GetComponent<Affliction>().totalDuration;
			enemy.GetComponent<Affliction>().tickFrequency = gameObject.GetComponent<Affliction>().tickFrequency;
			enemy.GetComponent<Affliction>().amount = gameObject.GetComponent<Affliction>().amount;
		}

		// Resets timer
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
}

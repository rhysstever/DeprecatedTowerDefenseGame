using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTier
{
	Basic,
	Advanced
	// Expert -- to be implemented
}

public enum TowerType
{
	None,
	Air			=    1,
	Earth		=   20,
	Fire		=  300, 
	Water		= 4000,
	Lightning	=  301,
	Volcano		=  320,
	Tornado		= 4001,
	Ice			= 4020
}

public class Tower : MonoBehaviour
{
    // Set in inspector
    public TowerTier towerTier;
    public TowerType towerType;
	public int cost;
    public float damage;
    public float attackTime;
    public float range;
	public int numOfTargets;

	// Set at Start()
	public GameObject targetedEnemy;
	public bool isUpgradable;
    public float shotTimer;
    
	// Set on creation
	public GameObject tile;
    
	// Start is called before the first frame update
    void Start()
    {
		targetedEnemy = null;
		isUpgradable = Enum.IsDefined(typeof(TowerType), (int)(towerTier + 1));
		shotTimer = attackTime;    // the tower can shoot immediately
    }

    // Update is called once per frame
    void Update()
    {
		RotateToCurrentEnemy();
	}

	void FixedUpdate()
	{
		shotTimer += Time.deltaTime;
	}

	/// <summary>
	/// Determines whether it can fire on an enemy
	/// </summary>
	/// <returns>Returns true if the tower has a targeted enemy and its timer is high enough</returns>
	public bool CanShoot()
	{
		if(targetedEnemy != null
			&& shotTimer >= attackTime)
			return true;

		return false;
	}

	/// <summary>
	/// Deals damage to the current enemy
	/// </summary>
    public void DealDamage(GameObject enemy)
	{
		// Deals damage to the targeted enemy
		enemy.GetComponent<Enemy>().TakeDamage(damage);
		// Applies the tower's affliction to the enemy if it has one
		if(gameObject.GetComponent<Affliction>() != null) {
			Type type = Type.GetType("Affliction");
			enemy.AddComponent(type);
			enemy.GetComponent<Affliction>().type = gameObject.GetComponent<Affliction>().type;
			enemy.GetComponent<Affliction>().totalDuration = gameObject.GetComponent<Affliction>().totalDuration;
			enemy.GetComponent<Affliction>().currentTime = gameObject.GetComponent<Affliction>().totalDuration;
			enemy.GetComponent<Affliction>().tickFrequency = gameObject.GetComponent<Affliction>().tickFrequency;
			enemy.GetComponent<Affliction>().amount = gameObject.GetComponent<Affliction>().amount;
		}

		// Reset timer
		shotTimer = 0.0f;
	}

	/// <summary>
	/// Calculate the correct rotation, zero-ing out the x and z values so it does not tilt in any direction
	/// </summary>
	void RotateToCurrentEnemy()
	{
		if(targetedEnemy != null) {
			gameObject.transform.LookAt(targetedEnemy.transform);
			Quaternion newQuat = gameObject.transform.rotation;
			newQuat.x = 0.0f;
			newQuat.z = 0.0f;
			gameObject.transform.rotation = newQuat;
		}
	}
}

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
		// If the tower has an affliction
		if(gameObject.GetComponent<Affliction>() != null) {
			Type compType = null;
			// Checks the type of affliction the tower has,
			// if the enemy doesn't have the affliction alreayd, then the afflcition is added to the enemy,
			// if the enemy already has it then its timer is reset
			switch(gameObject.GetComponent<Affliction>().type) {
				case AfflictionType.DamageOverTime:
					compType = Type.GetType("DamageOverTime");
					if(enemy.GetComponent<DamageOverTime>() == null) {
						enemy.AddComponent(compType);
						enemy.GetComponent<DamageOverTime>().type = gameObject.GetComponent<DamageOverTime>().type;
						enemy.GetComponent<DamageOverTime>().totalDuration = gameObject.GetComponent<DamageOverTime>().totalDuration;
						enemy.GetComponent<DamageOverTime>().procFrequency = gameObject.GetComponent<DamageOverTime>().procFrequency;
						enemy.GetComponent<DamageOverTime>().procDamage = gameObject.GetComponent<DamageOverTime>().procDamage;
					} else {
						enemy.GetComponent<DamageOverTime>().ResetTimer();
					}
					break;
				case AfflictionType.Slow:
					compType = Type.GetType("Slow");
					if(enemy.GetComponent<Slow>() == null) {
						enemy.AddComponent(compType);
						enemy.GetComponent<Slow>().type = gameObject.GetComponent<Slow>().type;
						enemy.GetComponent<Slow>().totalDuration = gameObject.GetComponent<Slow>().totalDuration;
						enemy.GetComponent<Slow>().slowPercentage = gameObject.GetComponent<Slow>().slowPercentage;
					} else {
						enemy.GetComponent<Slow>().ResetTimer();
					}
					break;
				case AfflictionType.Stun:
					compType = Type.GetType("Affliction");
					if(enemy.GetComponent<Affliction>() == null) {
						enemy.AddComponent(compType);
						enemy.GetComponent<Affliction>().type = gameObject.GetComponent<Affliction>().type;
						enemy.GetComponent<Affliction>().totalDuration = gameObject.GetComponent<Affliction>().totalDuration;
					} else {
						enemy.GetComponent<Affliction>().ResetTimer();
					}
					break;
			}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        shotTimer = attackSpeed;    // the tower can shoot immediately
		currentEnemy = null;
    }

    // Update is called once per frame
    void Update()
    {
		CheckForEnemies();
		RotateToCurrentEnemy();
    }

	void FixedUpdate()
	{
		if(currentEnemy != null
			&& shotTimer >= attackSpeed)
			Shoot(currentEnemy);
		else
			shotTimer += Time.deltaTime;
	}

	void CheckForEnemies()
	{
		currentEnemy = null;
		GameObject enemies = GameObject.Find("enemies");
		for(int child = 0; child < enemies.transform.childCount; child++) {
			if(Vector3.Distance(
				gameObject.transform.position,
				enemies.transform.GetChild(child).position) <= range)
				currentEnemy = enemies.transform.GetChild(child).gameObject;
		}
	}

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

	void RotateToCurrentEnemy()
	{
		if(currentEnemy != null) {
			// Calculate the correct rotation, zero-ing out the x and z values 
			// so it does not tilt in any direction
			gameObject.transform.LookAt(currentEnemy.transform);
			Quaternion newQuat = gameObject.transform.rotation;
			newQuat.x = 0.0f;
			newQuat.z = 0.0f;
			gameObject.transform.rotation = newQuat;
		}
	}
}

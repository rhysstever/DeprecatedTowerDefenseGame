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
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = attackSpeed;    // the tower can shoot immediately
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(GameObject enemy)
	{
        // Checks that the enemy is not null and is actually an enemy
        if(enemy != null
            && enemy.GetComponent<Enemy>()) {
            Debug.Log("This is not an enemy!");
            return;
		}

        // Deals damage to enemy
        enemy.GetComponent<Enemy>().health -= damage;
	}
}

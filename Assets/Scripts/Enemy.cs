using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
	public int damage;
    public float moveSpeed;
    public GameObject currentCheckpoint;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void FixedUpdate()
	{
        Move();
    }

    /// <summary>
    /// Moves the enemy closer to its next checkpoint until it gets close enough to move to the next checkpoint
    /// </summary>
    void Move()
	{
        // If the enemy is close enough to the checkpoint, the current checkpoint is update to 
        // the next checkpoint and the enemy is rotated accordingly
        if(Vector3.Distance(gameObject.transform.position, currentCheckpoint.transform.position)
            <= GameObject.Find("GameManager").GetComponent<EnemyManager>().checkpointRange) {
            currentCheckpoint = currentCheckpoint.GetComponent<Checkpoint>().nextCheckpoint;
            RotateToNextCP();
        }
        // If the enemy is still too far away from the checkpoint, it is moved to be closer
        else {
            gameObject.transform.position += NextMoveVec();
		}
    }

    /// <summary>
    /// Calculates the next move of the enemy to get it closer to the checkpoint
    /// </summary>
    /// <returns>The next move of the enemy, scaled to the enemy's movement speed</returns>
    Vector3 NextMoveVec()
	{
		// Finds the distance to the next checkpoint, zeros out the y value, normalizes it, 
		// and then scales it by the enemy's move speed
        Vector3 distVec = currentCheckpoint.transform.position - gameObject.transform.position;
        distVec.y = 0.0f;
        distVec.Normalize();
        distVec /= 100;
        distVec *= moveSpeed;

        return distVec;
	}
    
    /// <summary>
    /// Rotates the enemy to look at the current checkpoint
    /// </summary>
    void RotateToNextCP()
    {
		// If there is no checkpoint to look at, the method ends
		if(currentCheckpoint == null)
			return;

        // Calculate the correct rotation, zero-ing out the x and z values 
        // so it does not tilt in any direction
        gameObject.transform.LookAt(currentCheckpoint.transform);
        Quaternion newQuat = gameObject.transform.rotation;
        newQuat.x = 0.0f;
        newQuat.z = 0.0f;
        gameObject.transform.rotation = newQuat;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
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
        PathFollow();
    }

    /// <summary>
    /// Moves the enemy closer to its next checkpoint until it gets close enough to move to the next checkpoint
    /// </summary>
    void PathFollow()
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
        Vector3 distToNextCP = currentCheckpoint.transform.position - gameObject.transform.position;
        distToNextCP.y = 0.0f;
        distToNextCP.Normalize();
        distToNextCP *= moveSpeed;

        return distToNextCP;
	}
    
    /// <summary>
    /// Rotates the enemy to look at the current checkpoint
    /// </summary>
    void RotateToNextCP()
    {
        // Calculate the correct rotation, zero-ing out the x and z values 
        // so the enemy does not tilt in any direction
        gameObject.transform.LookAt(currentCheckpoint.transform);
        Quaternion newQuat = gameObject.transform.rotation;
        newQuat.x = 0.0f;
        newQuat.z = 0.0f;
        gameObject.transform.rotation = newQuat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ===== Set in inspector =====
    public string enemyName;
    public float health;
	public int damage;
    public int worth;
    public float moveSpeed;
    public float currentMoveSpeed;

    // Set when created
    public GameObject currentCheckpoint;

    // ===== Set at Start() ===== 
    bool canMove;
    
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        RotateToNextCP();
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the current move speed to its initial speed
        currentMoveSpeed = moveSpeed;

        if(gameObject.GetComponents<Affliction>().Length > 0)
            ProcessAfflictions(gameObject.GetComponents<Affliction>());

        // If the enemy is close enough to the checkpoint, the current checkpoint is update to 
        // the next checkpoint and the enemy is rotated accordingly
        if(Vector3.Distance(gameObject.transform.position, currentCheckpoint.transform.position) <= 0.5f)
            canMove = false;
		else  // If the enemy is still too far away from the checkpoint, it is moved to be closer
            canMove = true;

		if(!canMove) {
            currentCheckpoint = currentCheckpoint.GetComponent<Checkpoint>().nextCheckpoint;
            RotateToNextCP();
        }
    }

	void FixedUpdate()
	{
        if(canMove)
            Move();
    }

    /// <summary>
    /// Moves the enemy closer to its next checkpoint until it gets close enough to move to the next checkpoint
    /// </summary>
    void Move()
    {
        gameObject.transform.position += NextMoveVec();
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
        distVec *= currentMoveSpeed;

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

    /// <summary>
    /// A simple method that reduces the health of the enemy
    /// </summary>
    /// <param name="damage">The amount of health being lost by the enemy</param>
    public void TakeDamage(float damage) { health -= damage; }

    /// <summary>
    /// Removes any active afflictions, if they have expired
    /// </summary>
    /// <param name="afflictions">The list of active afflictions on the enemy</param>
    void ProcessAfflictions(Affliction[] afflictions)
	{
        for(int a = 0; a < afflictions.Length; a++) {
            if(afflictions[a].currentTime <= 0.0f)
                Destroy(afflictions[a]);
		}
	}
}

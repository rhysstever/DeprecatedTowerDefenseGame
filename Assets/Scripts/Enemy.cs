using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public int currentCheckpoint;
    
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
        Vector3 newPos = gameObject.transform.position;
        newPos.z += moveSpeed;
        gameObject.transform.position = newPos;
    }
}

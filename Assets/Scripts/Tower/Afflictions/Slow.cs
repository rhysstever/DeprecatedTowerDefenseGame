using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Affliction
{
    public float slowPercentage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ProcessAffliction()
    {
		float newMoveSpeed = gameObject.GetComponent<Enemy>().currentMoveSpeed * (1 - (slowPercentage / 100));
        gameObject.GetComponent<Enemy>().currentMoveSpeed = newMoveSpeed;

        base.ProcessAffliction();
    }
}

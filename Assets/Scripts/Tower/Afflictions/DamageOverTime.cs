using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : Affliction
{
    public float procDamage;
    public float procFrequency;

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
        // Makes sure the damage over time does not proc immediately
        if(currentTime < totalDuration) {
            // Procs damage if the timer gets within a millisecond of the procFrequency
            float millisecDiff = (currentTime * 1000) % (procFrequency * 1000);
            if((int)millisecDiff == 0) {
                Debug.Log("Burned at " + currentTime);
                gameObject.GetComponent<Enemy>().TakeDamage(procDamage);
			}
		}

        base.ProcessAffliction();
    }
}

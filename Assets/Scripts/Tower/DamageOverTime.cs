using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : Affliction
{
    // ===== Set at creation =====
    public float fireDamageOverTime;
    public float procFrequency;

    public DamageOverTime(string name, Modifier type, float duration, float fireDamageOverTime, float procFrequency) 
        : base(name, type, duration)
    {
        this.fireDamageOverTime = fireDamageOverTime;
        this.procFrequency = procFrequency;
    }

    public override void ProcessAffliction(GameObject enemy)
	{
        if(currentTime < totalDuration
            && ((int)(currentTime * 1000) % procFrequency) == 0) {
            Debug.Log(currentTime);
            enemy.GetComponent<Enemy>().TakeDamage(fireDamageOverTime);
		}

        base.ProcessAffliction(enemy);
    }
}

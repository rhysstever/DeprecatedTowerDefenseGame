using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affliction
{
    // ===== Set on creation =====
    public string afflictionName;
    public Modifier type;
    public float totalDuration;
    public float currentTime;

    public Affliction(string name, Modifier type, float duration)
	{
        afflictionName = name;
        this.type = type;
        totalDuration = duration;
        currentTime = duration;
	}

    public virtual void ProcessAffliction(GameObject enemy)
    {
        currentTime -= Time.deltaTime;

        switch(type) {
            case Modifier.Stun:
                enemy.GetComponent<Enemy>().currentMoveSpeed = 0.0f;
                break;
        }
    }
}

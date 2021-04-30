using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Affliction
{
    // ===== Set at creation =====
    public float slowPercentage;

    public Slow(string name, Modifier type, float duration, float slowPercentage)
        : base(name, type, duration)
    {
        this.slowPercentage = slowPercentage;
    }

    public override void ProcessAffliction(GameObject enemy)
    {
		float newMoveSpeed = enemy.GetComponent<Enemy>().currentMoveSpeed * (1 - (slowPercentage / 100));

		enemy.GetComponent<Enemy>().currentMoveSpeed = newMoveSpeed;

		Debug.Log("slowed to " + newMoveSpeed);
        base.ProcessAffliction(enemy);
    }
}

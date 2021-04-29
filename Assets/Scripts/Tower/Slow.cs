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
        enemy.GetComponent<Enemy>().currentMoveSpeed *= (1 - (slowPercentage / 100));
        Debug.Log("slowed by " + slowPercentage);
        base.ProcessAffliction(enemy);
    }
}

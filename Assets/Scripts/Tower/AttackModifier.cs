using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Modifier
{
    Slow,
    DamageOverTime,
    Stun
}

public class AttackModifier : MonoBehaviour
{
    // ===== Set in inspector =====
    public string modifierName;
    public Modifier modifierType;
    public float totalDuration;
    public float amount;
    public float procFrequency;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

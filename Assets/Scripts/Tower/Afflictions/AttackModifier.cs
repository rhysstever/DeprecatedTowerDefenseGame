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
    public float effectAmount;
    public float procFrequency;

    // Start is called before the first frame update
    void Start()
    {
        // Depending on which modifier it is, some values are changed
        switch(modifierType) {
            case Modifier.Slow:
                procFrequency = -1;
                break;
            case Modifier.Stun:
                effectAmount = -1;
                procFrequency = -1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

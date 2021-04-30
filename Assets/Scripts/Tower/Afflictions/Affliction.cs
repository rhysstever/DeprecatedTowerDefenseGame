using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AfflictionType
{
    DamageOverTime,
    Slow,
    Stun    // nothing special, uses base Afflcition code
}

public class Affliction : MonoBehaviour
{
	public string afflictionName;
    public AfflictionType type;
    public float totalDuration;
    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalDuration;
    }

    // Update is called once per frame
    void Update()
    {

    }

	public virtual void ProcessAffliction()
    {
        currentTime -= Time.deltaTime;

        switch(type) {
            case AfflictionType.Stun:
                gameObject.GetComponent<Enemy>().currentMoveSpeed = 0.0f;
                break;
        }
    }

    public void ResetTimer()
	{
        currentTime = totalDuration;
    }
}

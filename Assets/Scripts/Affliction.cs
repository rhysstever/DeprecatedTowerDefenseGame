using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AfflictionType
{
    Slow,
    DamageOverTime,
    Stun
}

public class Affliction : MonoBehaviour
{
    public AfflictionType type; 
    public float totalDuration;
    public float currentTime;
    public float tickFrequency;
    public float amount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Enemy>() != null)
            ActivateAffliction();
    }

	void FixedUpdate()
    {
        if(gameObject.GetComponent<Enemy>() != null)
            currentTime -= Time.deltaTime;
    }

    void ActivateAffliction()
	{
        switch(type) {
            case AfflictionType.Slow:
                gameObject.GetComponent<Enemy>().currentMoveSpeed *= (1 - (amount / 100));
                break;
            case AfflictionType.DamageOverTime:
                if(currentTime < totalDuration
                    && (int)(currentTime * 1000) % tickFrequency == 0)
                    gameObject.GetComponent<Enemy>().TakeDamage(amount);
                break;
            case AfflictionType.Stun:
                gameObject.GetComponent<Enemy>().currentMoveSpeed = 0.0f;
                break;
        }
    }
}

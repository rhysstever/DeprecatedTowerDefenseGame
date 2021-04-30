using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affliction : MonoBehaviour
{
	public string afflictionName;
    public Modifier type;
    public float totalDuration;
    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	public virtual void ProcessAffliction()
    {
        currentTime -= Time.deltaTime;

        switch(type) {
            case Modifier.Stun:
                gameObject.GetComponent<Enemy>().currentMoveSpeed = 0.0f;
                break;
        }
    }

    public void ResetTimer()
	{
        currentTime = totalDuration;
    }
}

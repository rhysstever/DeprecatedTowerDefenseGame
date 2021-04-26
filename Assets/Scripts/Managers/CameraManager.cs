using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // ===== Set in inspector =====
    public GameObject camParent;

    // ===== Set at Start() =====
    private float rotationSpeed;
    private float mulitplier;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 0.1f;
        mulitplier = 2;
    }

    // Update is called once per frame
    void Update()
    {
        float finalRotationSpeed = rotationSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
            finalRotationSpeed *= mulitplier;

        if(gameObject.GetComponent<GameManager>().currentMenuState == MenuState.game) {
            // Controls
            // A - Rotate Camera to the left
            // D - Rotate Camera to the right
            if(Input.GetKey(KeyCode.A))
                camParent.transform.Rotate(0.0f, finalRotationSpeed, 0.0f);
            else if(Input.GetKey(KeyCode.D))
                camParent.transform.Rotate(0.0f, -finalRotationSpeed, 0.0f);
        }
    }
}

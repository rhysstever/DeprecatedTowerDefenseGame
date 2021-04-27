using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // ===== Set in inspector =====
    public GameObject camParent;

    // ===== Set at Start() =====
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<GameManager>().currentMenuState == MenuState.game) {
            // Controls
            //  W                - Pans camera up
            //  A                - Pans camera to the left
            //  S                - Pans camera down
            //  D                - Pans camera to the right
            //  Scrolling Up     - Zooms the camera in
            //  Scrolling Down   - Zooms the camera out

            if(Input.GetKey(KeyCode.W))
                camParent.transform.Translate(0.0f, 0.0f, moveSpeed);
            else if(Input.GetKey(KeyCode.A))
                camParent.transform.Translate(-moveSpeed, 0.0f, 0.0f);
            else if(Input.GetKey(KeyCode.S))
                camParent.transform.Translate(0.0f, 0.0f, -moveSpeed);
            else if(Input.GetKey(KeyCode.D))
                camParent.transform.Translate(moveSpeed, 0.0f, 0.0f);

            if(Input.mouseScrollDelta.y != 0) {
                Vector3 camForward = camParent.transform.GetChild(0).forward;
                camParent.transform.position += camForward * Input.mouseScrollDelta.y;
                while(camParent.transform.position.y < 5.0f
                    || camParent.transform.position.y > 15.0f) {
                    if(camParent.transform.position.y < 5.0f)
                        camParent.transform.position -= camForward * 0.1f;
                    else if(camParent.transform.position.y > 15.0f)
                        camParent.transform.position += camForward * 0.1f;
                }
            }
        }
    }
}

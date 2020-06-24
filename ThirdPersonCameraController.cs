using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{


    [SerializeField] private float RotationSpeed = 1;
    [SerializeField] private float UpperAngleLimit = 75f;
    [SerializeField] private float LowerAngleLimit = -70f;
    [SerializeField] private Transform Target, Player;
    private float mouseX, mouseY;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    void Update() {
        CamController();
    }



    void CamController() {

        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, LowerAngleLimit, UpperAngleLimit);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);

    }
}

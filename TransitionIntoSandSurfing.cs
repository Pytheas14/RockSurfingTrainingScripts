using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionIntoSandSurfing : MonoBehaviour
{
    [SerializeField] private GameObject ridingRock;
    [SerializeField] private GameObject cameraPivot;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpHeight = 2;
    [SerializeField] float gravity = 20;
    [SerializeField] float heightToRiseBeforeSwitching = 1.5f;
    [Range(0, 10), SerializeField] float airControl = 5;
    
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 input = new Vector3(0, 0, 0);
    private Vector3 originalPosition = new Vector3(0, 0, 0);
    private Quaternion originalRotation;
    private Quaternion newPlayerRotation;
    private Vector3 newPosition;
    CharacterController controller;



    void OnEnable()
    {
        ridingRock.SetActive(true);
        controller = GetComponent<CharacterController>();
        
        originalPosition = this.transform.position;
        
        this.GetComponent<CharacterController>().enabled = false;

        newPosition = originalPosition;
        newPosition.y = originalPosition.y - 1.5f;
        ridingRock.transform.position = newPosition;

        Vector3 newRotation = new Vector3(0, cameraPivot.transform.eulerAngles.y, 0);
        ridingRock.transform.eulerAngles = newRotation;

        ridingRock.transform.parent = null;
        this.transform.parent = ridingRock.transform;
        
    }



    void FixedUpdate()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        input *= moveSpeed;

        input = transform.TransformDirection(input);
        
        input.y = 2;
        moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);


        ridingRock.transform.position += moveDirection * Time.deltaTime;

        if (ridingRock.transform.position.y > newPosition.y + heightToRiseBeforeSwitching) {
            ridingRock.GetComponent<SurfingController>().enabled = true;
        }
    }
}

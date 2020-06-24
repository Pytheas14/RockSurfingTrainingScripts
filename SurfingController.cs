using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfingController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ridingRock;
    [SerializeField] private ObjectPool fallingRockPool;
    [SerializeField] private ObjectPool rockParticleSystemPool;
    [SerializeField] float constantForwardForcedSpeed = 1f;
    [SerializeField] float speedIncreaseModifier = 2f;
    [SerializeField] float speedDecreaseModifier = .5f;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpHeight = 2;
    [SerializeField] float gravity = 20;
    [Range(0, 10), SerializeField] float airControl = 5;
    
    private Vector3 moveDirection = Vector3.zero;

    CharacterController controller;

    [SerializeField] float turnSpeed = 90f;

    float yaw = 0f;

    Quaternion rockStartOrientation;

    [SerializeField] private float summonFallingRockDelay = .4f;
    
    private float nextSummonFallingRockTime = 0;



    void OnEnable()
    {
        
        player.GetComponent<TransitionIntoSandSurfing>().enabled = false;
        this.GetComponent<CharacterController>().enabled = true;
        controller = GetComponent<CharacterController>();

        nextSummonFallingRockTime = Time.time + summonFallingRockDelay;
        
        Vector3 rot = player.transform.rotation.eulerAngles;
        rockStartOrientation = Quaternion.Euler(rot);
        yaw = 0f;
    }



    void Update()
    {
        var forwardButtonPushed = false;
        var backButtonPushed = false;
        var leftButtonPushed = false;
        var rightButtonPushed = false;
        
        
        if (Input.GetButton("InFront")) {
            forwardButtonPushed = true;
        }
        if (Input.GetButton("InBack")) {
            backButtonPushed = true;
        }
        if (Input.GetButton("ToLeft")) {
            leftButtonPushed = true;
        }
        if (Input.GetButton("ToRight")) {
            rightButtonPushed = true;
        }
                
        var turnDirection = 0;
        
        if (leftButtonPushed && !rightButtonPushed) {
            turnDirection = -1;
        }

        if (rightButtonPushed && !leftButtonPushed) {
            turnDirection = 1;
        }

        var horizontal = turnDirection * Time.deltaTime * turnSpeed;

        yaw += horizontal;

        var rockRotation = Quaternion.AngleAxis(yaw, Vector3.up);

        transform.rotation = rockRotation * rockStartOrientation;
    
        var speedModifier = 1f;
        if (forwardButtonPushed && !backButtonPushed) {
            speedModifier = speedModifier * speedIncreaseModifier;
        }

        if (backButtonPushed && !forwardButtonPushed) {
            speedModifier = speedModifier * speedDecreaseModifier;
        }

        var input = new Vector3(0, 0, constantForwardForcedSpeed * speedModifier);

        input *= moveSpeed;

        input = transform.TransformDirection(input);
        
        if (controller.isGrounded) {
            moveDirection = input;
            if (Input.GetButton("Jump")) {
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            } else {
                moveDirection.y = 0;
            }
        } else {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);

        if(Time.time > nextSummonFallingRockTime) {
            var newRockParticleSystem = rockParticleSystemPool.GetObject();

            newRockParticleSystem.transform.localPosition = new Vector3(ridingRock.transform.localPosition.x, ridingRock.transform.localPosition.y - 0.8f, ridingRock.transform.localPosition.z);
            newRockParticleSystem.transform.rotation = ridingRock.transform.rotation;

            nextSummonFallingRockTime = Time.time + summonFallingRockDelay;
        }
    }
}

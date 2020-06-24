using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpHeight = 2;
    [SerializeField] float gravity = 20;
    [Range(0, 10), SerializeField] float airControl = 5;
    
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] private GameObject sera;
    CharacterController controller;
    private Animator animator;
    [SerializeField] private AudioSource jumpSoundEffect1;
    [SerializeField] private AudioSource jumpSoundEffect2;
    [SerializeField] private AudioSource jumpSoundEffect3;

    [SerializeField] private GameObject ridingRock;

    [SerializeField] private float ModeToggleDelay = 3.1f;
    
    private float nextModeToggleTime = 0f;

    [SerializeField] private float jumpToggleDelay = 1.9f;
    
    private float nextJumpToggleTime = 0f;

    [SerializeField] private float hitByPillarDelay = 1f;
    
    private float nextHitByPillarTime = 0f;

    public bool inRegularMode;
    public bool inSurfingMode;



    void Start()
    {
        animator = sera.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();    

        inRegularMode = true;
        inSurfingMode = false;
        nextModeToggleTime = Time.time;
        nextJumpToggleTime = Time.time;
        nextHitByPillarTime = Time.time;
    }


    void Update()
    {

        if (Time.time > nextModeToggleTime) {
            

            if (inRegularMode && Input.GetButton("Sand Surfing Toggle")) {
                
                this.GetComponent<TransitionIntoSandSurfing>().enabled = true;
                inRegularMode = false;
                inSurfingMode = true;
                animator.SetBool("inSurfState", true);
                nextModeToggleTime = Time.time + ModeToggleDelay;
            } else if (inSurfingMode && Input.GetButton("Sand Surfing Toggle")) {
                
                ridingRock.GetComponent<SurfingSinkBackDown>().enabled = true;

                inSurfingMode = false;
                inRegularMode = true;
                animator.SetBool("inSurfState", false);
                nextModeToggleTime = Time.time + ModeToggleDelay;
            }
        }

        sera.transform.localPosition = new Vector3(0.0f, -1.08f, 0.0f);

        if (inRegularMode) {


            var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            input *= moveSpeed;

            input = transform.TransformDirection(input);
            
            if (controller.isGrounded) {
                animator.SetBool("onGround", true);
                animator.SetBool("jumping", false);
                moveDirection = input;
                if (Input.GetButton("Jump") && Time.time > nextJumpToggleTime) {
                    var soundSelector = Random.Range(1f, 3f);
                    if (soundSelector <= 1f) {
                        jumpSoundEffect1.Play();
                    } else if (soundSelector <= 2f) {
                        jumpSoundEffect2.Play();
                    } else {
                        jumpSoundEffect3.Play();
                    }
                    animator.SetBool("jumping", true);
                    moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
                    nextJumpToggleTime = Time.time + jumpToggleDelay;
                } else {
                    moveDirection.y = 0;
                }
            } else {
                animator.SetBool("onGround", false);
                animator.SetBool("jumping", false);
                input.y = moveDirection.y;
                moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
            }

            moveDirection.y -= gravity * Time.deltaTime;
            
            controller.Move(moveDirection * Time.deltaTime);
            
            if (Input.GetAxis("Vertical") > 0) {
                animator.SetBool("movingForward", true);
            } else {
                animator.SetBool("movingForward", false);
            }

            if (Input.GetAxis("Vertical") < 0) {
                animator.SetBool("movingBack", true);
            } else {
                animator.SetBool("movingBack", false);
            }

            if (Input.GetAxis("Horizontal") > 0) {
                animator.SetBool("movingRight", true);
            } else {
                animator.SetBool("movingRight", false);
            }

            if (Input.GetAxis("Horizontal") < 0) {
                animator.SetBool("movingLeft", true);
            } else {
                animator.SetBool("movingLeft", false);
            }


            
            var seraX = sera.transform.localRotation.x;
            var seraZ  = sera.transform.localRotation.z;
            if (Input.GetAxis("Horizontal") == 0) {
                // Point forward
                sera.transform.localRotation = Quaternion.Euler(seraX, 0, seraZ);
            } else if (Input.GetAxis("Horizontal") > 0) {
                 if (Input.GetAxis("Vertical") < 0) {
                    // Turn -45 degrees
                    sera.transform.localRotation = Quaternion.Euler(seraX, 315, seraZ);
                } else {
                    // Point forward
                    sera.transform.localRotation = Quaternion.Euler(seraX, 0, seraZ);
                }
            } else if (Input.GetAxis("Horizontal") < 0) {
                if (Input.GetAxis("Vertical") < 0) {
                    // Turn -45 degrees
                    sera.transform.localRotation = Quaternion.Euler(seraX, 45, seraZ);
                } else {
                    // Point forward
                    sera.transform.localRotation = Quaternion.Euler(seraX, 0, seraZ);
                }
            }
        }
    }    
}

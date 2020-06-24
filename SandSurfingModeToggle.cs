using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSurfingModeToggle : MonoBehaviour
{
    [SerializeField] private GameObject ridingRock;

    [SerializeField] private float ModeToggleDelay = 3.1f;
    
    private float nextModeToggleTime = 0f;

    private bool inRegularMode;
    private bool inSurfingMode;



    void Start()
    {
        
        inRegularMode = true;
        inSurfingMode = false;
        nextModeToggleTime = Time.time;
    }



    void FixedUpdate()
    {

        if (Time.time > nextModeToggleTime) {
            if (inRegularMode && Input.GetButton("Sand Surfing Toggle")) {
                this.GetComponent<TransitionIntoSandSurfing>().enabled = true;
                inRegularMode = false;
                inSurfingMode = true;
                nextModeToggleTime = Time.time + ModeToggleDelay;
            } else if (inSurfingMode && Input.GetButton("Sand Surfing Toggle")) {
                ridingRock.GetComponent<SurfingSinkBackDown>().enabled = true;
                inSurfingMode = false;
                inRegularMode = true;
                nextModeToggleTime = Time.time + ModeToggleDelay;
            }
        }
    }
}

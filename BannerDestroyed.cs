// File name: BannerDestroyed.cs
// By: Dylan Greek
//
// Input: None, run upon script activation
// Output: This script tells the attached object (intended to be a target banner) to rotate on the x 
// in the backwards directions for 1 second before destroying itself.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerDestroyed : MonoBehaviour
{

    [SerializeField] private float scriptLifeSpan = 1f;
    
    private float deactivateTime = 1f;

    void Start()
    {
        deactivateTime = Time.time + scriptLifeSpan;
    }


    void FixedUpdate()
    {


        transform.parent.Rotate(4.0f, 0.0f, 0.0f, Space.Self);
        
        if (Time.time >= deactivateTime) {
            transform.parent.gameObject.active = false;
        }
    }
}

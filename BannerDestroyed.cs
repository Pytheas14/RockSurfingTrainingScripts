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

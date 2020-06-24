using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingRockParticles : MonoBehaviour
{
    [SerializeField] private float scriptLifeSpan = 2f;
    
    private float deactivateTime = 0f;
    
    private Vector3 movement = new Vector3(0, 0, 0);
    
    [SerializeField] private float sinkSpeed = .5f;



    void Start()
    {
        deactivateTime = Time.time + scriptLifeSpan;
    }



    void Update()
    {
        movement = new Vector3(0, -1, 0);
        movement *= sinkSpeed;
        this.transform.position += movement * Time.deltaTime;


        if (Time.time > deactivateTime) {
            Destroy(gameObject);
        }
    }
}

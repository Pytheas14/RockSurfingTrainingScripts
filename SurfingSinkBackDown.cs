using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfingSinkBackDown : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ridingRock;

    [SerializeField] private float scriptLifeSpan = 3f;
    
    private float deactivateTime = 0f;
    
    private Vector3 movement = new Vector3(0, 0, 0);
    
    [SerializeField] private float fallSpeed = .5f;


    
    void OnEnable()
    {
        player.transform.parent = null;
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<SurfingController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = true;
        deactivateTime = Time.time + scriptLifeSpan;
    }
    


    void Update()
    {

        
        movement = new Vector3(0, -1, 0);
        movement *= fallSpeed;

        this.transform.position += movement * Time.deltaTime;

        if (Time.time > deactivateTime) {
            ridingRock.transform.parent = player.transform;
            ridingRock.transform.rotation = player.transform.rotation;
            ridingRock.SetActive(false);
            this.GetComponent<SurfingSinkBackDown>().enabled = false;
        }
        
    }
}

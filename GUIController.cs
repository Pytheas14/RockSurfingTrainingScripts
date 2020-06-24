using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour
{

    private bool objectivesTextVisible;
    private bool helpTextVisible;

    [SerializeField] private Canvas helpCanvas;
    [SerializeField] private Canvas objectivesCanvas;
    [SerializeField] private Canvas zone1Canvas;
    [SerializeField] private Canvas zone2Canvas;
    [SerializeField] private float textToggleDelay = .2f;
    
    private float nextTextToggleTime = 0f;

    private GameObject player;

    private GameObject firingRangeZoneNorth;
    private GameObject firingRangeZoneSouth;
    private GameObject firingRangeZoneWest;
    private GameObject firingRangeZoneEast;
    private GameObject sandSurfingZoneNorth;
    private GameObject sandSurfingZoneSouth;
    private GameObject sandSurfingZoneWest;
    private GameObject sandSurfingZoneEast;
    
    private bool playerInFiringRangeZone;
    private bool playerInSandSurfingZone;


    void Start()
    {
        helpTextVisible = false;
        objectivesTextVisible = true;
        nextTextToggleTime = Time.time + textToggleDelay;

        player = GameObject.Find("Player");
        firingRangeZoneNorth = GameObject.Find("FiringRangeZoneNorthWall");
        firingRangeZoneSouth = GameObject.Find("FiringRangeZoneSouthWall");
        firingRangeZoneWest = GameObject.Find("FiringRangeZoneWestWall");
        firingRangeZoneEast = GameObject.Find("FiringRangeZoneEastWall");

        sandSurfingZoneNorth = GameObject.Find("SandSurfingZoneNorthWall");
        sandSurfingZoneSouth = GameObject.Find("SandSurfingZoneSouthWall");
        sandSurfingZoneWest = GameObject.Find("SandSurfingZoneWestWall");
        sandSurfingZoneEast = GameObject.Find("SandSurfingZoneEastWall");
        playerInFiringRangeZone = false;
        playerInSandSurfingZone = false;
    }

    void Update()
    {
        var playerPosX = player.transform.position.x;
        var playerPosZ = player.transform.position.z;

        var firingZoneNorthX = firingRangeZoneNorth.transform.position.x;
        var firingZoneSouthX = firingRangeZoneSouth.transform.position.x;
        var firingZoneWestZ = firingRangeZoneWest.transform.position.z;
        var firingZoneEastZ = firingRangeZoneEast.transform.position.z;

        if (playerPosX <= firingZoneNorthX && playerPosX >= firingZoneSouthX && playerPosZ <= firingZoneWestZ && playerPosZ >= firingZoneEastZ) {
            playerInFiringRangeZone = true;
        } else {
            playerInFiringRangeZone = false;
        }

        var surfingZoneNorthX = sandSurfingZoneNorth.transform.position.x;
        var surfingZoneSouthX = sandSurfingZoneSouth.transform.position.x;
        var surfingZoneWestZ = sandSurfingZoneWest.transform.position.z;
        var surfingZoneEastZ = sandSurfingZoneEast.transform.position.z;

        if (playerPosX <= surfingZoneNorthX && playerPosX >= surfingZoneSouthX && playerPosZ <= surfingZoneWestZ && playerPosZ >= surfingZoneEastZ) {
            playerInSandSurfingZone = true;
        } else {
            playerInSandSurfingZone = false;
        }

        if (playerInFiringRangeZone) {
            zone1Canvas.GetComponent<CanvasGroup>().alpha = 1;
            zone2Canvas.GetComponent<CanvasGroup>().alpha = 0;
        } else if (playerInSandSurfingZone) {
            zone1Canvas.GetComponent<CanvasGroup>().alpha = 0;
            zone2Canvas.GetComponent<CanvasGroup>().alpha = 1;
        }


        
        if (Input.GetButton("ToggleHelp") && objectivesTextVisible && !helpTextVisible && Time.time >= nextTextToggleTime) {
            helpCanvas.GetComponent<CanvasGroup>().alpha = 0;
            objectivesCanvas.GetComponent<CanvasGroup>().alpha = 1;
            helpTextVisible = true;
            objectivesTextVisible = false;
            nextTextToggleTime = Time.time + textToggleDelay;
        } else if (Input.GetButton("ToggleHelp") && helpTextVisible && !objectivesTextVisible && Time.time >= nextTextToggleTime) {
            helpCanvas.GetComponent<CanvasGroup>().alpha = 1;
            objectivesCanvas.GetComponent<CanvasGroup>().alpha = 0;
            helpTextVisible = false;
            objectivesTextVisible = true;
            nextTextToggleTime = Time.time + textToggleDelay;
        }
    }
}

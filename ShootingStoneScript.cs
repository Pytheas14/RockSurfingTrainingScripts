using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStoneScript : MonoBehaviour
{

    public bool stoneJustFired = false;
    public bool stoneFlying = false;
    [SerializeField] private float stoneFireSpeed = 30;

    [SerializeField] private float scriptLifeSpan = 3f;
    
    private float deactivateTime = 1f;

    private GameObject aimToObject;

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
    private bool bannerInFiringRangeZone;
    private bool bannerInSandSurfingZone;



    public void UpdateStoneJustFired(bool newValue)
    {
        stoneJustFired = newValue;
    }


    
    void Start() {
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

        if (stoneJustFired) {
            
            deactivateTime = Time.time + scriptLifeSpan;
            
            transform.parent = null;
            aimToObject = GameObject.Find("AimSpot");

            transform.LookAt(aimToObject.transform);
            
            stoneJustFired = false;
            stoneFlying = true;
            
        }

        if (stoneFlying) {
            transform.position += transform.forward * Time.deltaTime * stoneFireSpeed;

            if (Time.time > deactivateTime) {
                stoneFlying = false;
                gameObject.ReturnToPool();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        var playerPosX = player.transform.position.x;
        var playerPosZ = player.transform.position.z;

        var bannerPosX = other.transform.position.x;
        var bannerPosZ = other.transform.position.z;

        var firingZoneNorthX = firingRangeZoneNorth.transform.position.x;
        var firingZoneSouthX = firingRangeZoneSouth.transform.position.x;
        var firingZoneWestZ = firingRangeZoneWest.transform.position.z;
        var firingZoneEastZ = firingRangeZoneEast.transform.position.z;

        if (playerPosX <= firingZoneNorthX && playerPosX >= firingZoneSouthX && playerPosZ <= firingZoneWestZ && playerPosZ >= firingZoneEastZ) {
            playerInFiringRangeZone = true;
        } else {
            playerInFiringRangeZone = false;
        }

        if (bannerPosX <= firingZoneNorthX && bannerPosX >= firingZoneSouthX && bannerPosZ <= firingZoneWestZ && bannerPosZ >= firingZoneEastZ) {
            bannerInFiringRangeZone = true;
        } else {
            bannerInFiringRangeZone = false;
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

        if (bannerPosX <= surfingZoneNorthX && bannerPosX >= surfingZoneSouthX && bannerPosZ <= surfingZoneWestZ && bannerPosZ >= surfingZoneEastZ) {
            bannerInSandSurfingZone = true;
        } else {
            bannerInSandSurfingZone = false;
        }

        if(other.gameObject.tag != "SummonedStones" && other.gameObject.tag != "InvisibleWalls" && stoneFlying) {

            if(other.gameObject.tag == "TargetBanners" && playerInFiringRangeZone && bannerInFiringRangeZone) {
                //pass hit value
                player.GetComponent<ObjectiveManager>().BannerTargetHitInFiringRangeZone(1);
                other.gameObject.GetComponent<BannerDestroyed>().enabled = true;
            }
            
            if(other.gameObject.tag == "TargetBanners" && playerInSandSurfingZone && bannerInSandSurfingZone && player.GetComponent<GeneralPlayerController>().inSurfingMode == true) {
                //pass hit value
                player.GetComponent<ObjectiveManager>().BannerTargetHitInSandSurfingZone(1);
                other.gameObject.GetComponent<BannerDestroyed>().enabled = true;
            }
            
            gameObject.ReturnToPool();
            stoneFlying = false;
        }
    }
    
}

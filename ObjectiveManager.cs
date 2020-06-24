using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{

    [SerializeField] private Text firingBannersHitTextObject;
    [SerializeField] private Text surfingBannersHitTextObject;
    private int bannersHitInZone1 = 0;
    private int bannersHitInZone2 = 0;

    public void BannerTargetHitInFiringRangeZone(int points)
    {
        bannersHitInZone1 += points;
        if (bannersHitInZone1 < 22) {
            firingBannersHitTextObject.text = "Banners hit: " + bannersHitInZone1 + " / 22";
        } else {
            firingBannersHitTextObject.text = "CONGRATULATIONS!";
        }
    }

    public void BannerTargetHitInSandSurfingZone(int points)
    {
        bannersHitInZone2 += points;
        if (bannersHitInZone2 < 52) {
            surfingBannersHitTextObject.text = "Banners hit: " + bannersHitInZone2 + " / 52";
        } else {
            surfingBannersHitTextObject.text = "CONGRATULATIONS!";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrowing : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    [SerializeField] private GameObject playerObject;

    [SerializeField] private float summonDelay = 1f;
    [SerializeField] private float fireDelay = .4f;
    [SerializeField] private AudioSource stoneWooshSoundEffect;
    
    private float nextSummonTime = 0;
    private float nextFireTime = 0;
    private int numOfStonesSummoned = 0;

    private Stack<GameObject> summonedStonesStack = new Stack<GameObject>();



    void Update()
    {
        if ( numOfStonesSummoned < 6 && Time.time > nextSummonTime) {
            var newStone = pool.GetObject();

            newStone.transform.parent = playerObject.transform;
            
            var summonToObject = GameObject.Find("StoneOrbitPoint");
            var summonToPosition = summonToObject.transform.localPosition;

            var rotationCalc = 60 * numOfStonesSummoned;
            var summonRotationY = Quaternion.Euler(0, rotationCalc, 0);

            var summonRotationMatrix = Matrix4x4.Rotate(summonRotationY);

            var summonOffset = new Vector3(0f, -.5f, .5f);

            var newOffset = summonRotationMatrix.MultiplyPoint(summonOffset);
            
            newStone.transform.localPosition = summonToPosition + newOffset;
            
            newStone.GetComponent<FloatingScript>().enabled = true;
            
            nextSummonTime = Time.time + summonDelay;
            summonedStonesStack.Push(newStone);
            numOfStonesSummoned++;
        }
        
        if (Input.GetButton("Shoot Stone") && Time.time > nextFireTime) {
            stoneWooshSoundEffect.Play();
            

            var firedStone = summonedStonesStack.Peek();

            
            firedStone.GetComponent<ShootingStoneScript>().UpdateStoneJustFired(true);
            firedStone.GetComponent<FloatingScript>().enabled = false;

            nextFireTime = Time.time + fireDelay;
            numOfStonesSummoned--;
            summonedStonesStack.Pop();
        }
    }
}

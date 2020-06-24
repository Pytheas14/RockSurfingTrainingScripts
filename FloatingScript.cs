using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 moveDirection;
    [SerializeField] private float baseFloatSpeed = 1f;
    [SerializeField] private float floatSpeedRange = .1f;
    [SerializeField] private float floatDistance = .2f;
    private float floatSpeed = .3f;
    


    void Start()
    {
        originalPosition = this.transform.localPosition;

        floatSpeed = Random.Range(baseFloatSpeed - (floatSpeedRange / 2), baseFloatSpeed + (floatSpeedRange / 2));
        moveDirection.y = floatSpeed;
    }



    void Update()
    {
        var currentPosition = this.transform.localPosition;

        var percentInPiFromOrigin = Mathf.Sin((Mathf.Abs(currentPosition.y - originalPosition.y) / floatDistance) * (Mathf.PI / 2));
        var adjustedFloatSpeed = floatSpeed * percentInPiFromOrigin;

        if (currentPosition.y > originalPosition.y + floatDistance) {
            moveDirection.y = -adjustedFloatSpeed;
        } else if (currentPosition.y < originalPosition.y - floatDistance) {
            moveDirection.y = adjustedFloatSpeed;
        }
        


        transform.position += moveDirection * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpTextToggle : MonoBehaviour
{

    private bool helpTextVisible;
    [SerializeField] private Canvas helpCanvas;
    [SerializeField] private Canvas objectivesCanvas;
    [SerializeField] private float textToggleDelay = .2f;
    
    private float nextTextToggleTime = 0f;


    void Start()
    {
        helpTextVisible = false;
        nextTextToggleTime = Time.time + textToggleDelay;
    }

    void Update()
    {
        if (Input.GetButton("ToggleHelp") && Time.time >= nextTextToggleTime) {
            if (!helpTextVisible) {
                helpCanvas.GetComponent<CanvasGroup>().alpha = 0;
                objectivesCanvas.GetComponent<CanvasGroup>().alpha = 1;
                helpTextVisible = true;
                nextTextToggleTime = Time.time + textToggleDelay;
            } 
            else if (helpTextVisible) {
                helpCanvas.GetComponent<CanvasGroup>().alpha = 1;
                objectivesCanvas.GetComponent<CanvasGroup>().alpha = 0;
                helpTextVisible = false;
                nextTextToggleTime = Time.time + textToggleDelay;
            }
        }
    }
}

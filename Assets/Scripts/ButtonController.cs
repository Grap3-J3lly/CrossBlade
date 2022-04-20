using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class ButtonController : MonoBehaviour
{

    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    private InteractionButton interactionButton;

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Start() {
        interactionButton = GetComponent<InteractionButton>();
    }

    private void Update() {
        
    }

    //------------------------------------------------------
    //                  CONTROL FUNCTIONS
    //------------------------------------------------------

    public void ButtonHit() {
        Debug.Log("Button has been hit successfully");
        Debug.Log("IsPressed: " + interactionButton.isPressed);
    }

    public void StopHit() {
        Debug.Log("Button no longer pressed");
        Debug.Log("IsPressed: " + interactionButton.isPressed);
    }

}

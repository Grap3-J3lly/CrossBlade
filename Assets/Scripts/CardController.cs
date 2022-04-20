using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    [SerializeField] private bool changeSize = false;
    [SerializeField] private Vector3 smallScale = new Vector3(.1f, -.0025f, .15f);
    [SerializeField] private Vector3 largeScale = new Vector3(1f, -.0025f, 1.5f);
    
    private InteractionBehaviour interactionBehavior;
    private GameObject objectOfCollision;
    private bool grasping;
    private bool inSnapArea = false;
    private bool currentSize = false;

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    public void Start() {
        interactionBehavior = GetComponent<InteractionBehaviour>();
        interactionBehavior.OnGraspBegin += HandleBeginGrasp;
        interactionBehavior.OnGraspEnd += HandleEndGrasp;
    }

    public void Update() {
        if(changeSize) {
            ChangeSize(!currentSize);
        }
    }

    //------------------------------------------------------
    //          STANDARD COLLISION FUNCTIONS
    //------------------------------------------------------

    private void OnTriggerEnter(Collider colliderInfo) {
        if(colliderInfo.gameObject.GetComponent<SnapController>() != null) {
            inSnapArea = true;
            objectOfCollision = colliderInfo.gameObject;
        }
        
    }

    private void OnTriggerExit(Collider colliderInfo) {
        if(colliderInfo.gameObject.GetComponent<SnapController>() != null) {
            inSnapArea = false;
            objectOfCollision = null;
        }
    }

    //------------------------------------------------------
    //          CUSTOM COLLISION FUNCTIONS
    //------------------------------------------------------

    IEnumerator HandleCurrentGrab() {
        yield return new WaitUntil(() => !grasping && inSnapArea);
        HandleCardSnap(objectOfCollision);
    }

    private void HandleBeginGrasp() {
        grasping = true;
        StartCoroutine(HandleCurrentGrab());
    }

    private void HandleEndGrasp() {
        grasping = false;
    }

    //------------------------------------------------------
    //              GENERAL CARD FUNCTIONS
    //------------------------------------------------------

    private void ChangeSize(bool changeToSmall) {
        if(changeToSmall) {
            currentSize = true;
            GetComponent<Transform>().localScale = smallScale;
        }
        else {
            currentSize = false;
            GetComponent<Transform>().localScale = largeScale;
        }
        changeSize = false;
    }

    //------------------------------------------------------
    //              SNAP-TO-LOCATION FUNCTIONS
    //------------------------------------------------------

    private void HandleCardSnap(GameObject targetObject) {
        bool canSnap = false;
        
        canSnap = targetObject.GetComponent<SnapController>().SnapCheck("CardController");

        if(canSnap) {
            SnapToLocation(targetObject);
        }
    }

    private void SnapToLocation(GameObject targetObject) {
        transform.position = targetObject.transform.position;
        transform.rotation = targetObject.transform.rotation;
    }

}

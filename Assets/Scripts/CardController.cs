using Leap.Unity.Interaction;
using System;
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
    
    [SerializeField] private bool inPlay = false;

    private InteractionBehaviour interactionBehavior;
    private GameObject objectOfCollision;
    private bool grasping;
    private bool inSnapArea = false;
    private bool currentSize = false;

    public event Action onCardInPlay;

    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------

    public bool GetInPlay() {return inPlay;}
    public void SetInPlay(bool newValue) {inPlay = newValue;}

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        interactionBehavior = GetComponent<InteractionBehaviour>();
    }

    private void OnEnable() {
        interactionBehavior.OnGraspBegin += HandleBeginGrasp;
        interactionBehavior.OnGraspEnd += HandleEndGrasp;
    }

    private void Start() {
        
        
    }

    private void Update() {
        if(changeSize) {
            ChangeSize(!currentSize);
        }
    }

    private void OnDisable() {
        interactionBehavior.OnGraspBegin -= HandleBeginGrasp;
        interactionBehavior.OnGraspEnd -= HandleEndGrasp;
    }

    //------------------------------------------------------
    //          STANDARD COLLISION FUNCTIONS
    //------------------------------------------------------

    private void OnTriggerEnter(Collider colliderInfo) {
        if(colliderInfo.gameObject.GetComponent<SnapController>() != null) {
            inSnapArea = true;
            objectOfCollision = colliderInfo.gameObject;

            EnterPlayArea(colliderInfo.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider colliderInfo) {
        if(colliderInfo.gameObject.GetComponent<SnapController>() != null) {
            inSnapArea = false;
            objectOfCollision = null;

            ExitPlayArea(colliderInfo.gameObject);
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

    private void EnterPlayArea(GameObject potentialPlayArea) {
        if(potentialPlayArea.name.ToLower().Equals("playlocation")) {
            inPlay = true;
            CardInPlayEvent();
        }
    }

    private void ExitPlayArea(GameObject potentialPlayArea) {
        if(potentialPlayArea.name.ToLower().Equals("playlocation")) {
            inPlay = false;
            CardInPlayEvent();
        }
    }

    //------------------------------------------------------
    //              GENERAL CARD FUNCTIONS
    //------------------------------------------------------

    public void ChangeSize(bool changeToSmall) {
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

    //------------------------------------------------------
    //              EVENT RELATED FUNCTIONS
    //------------------------------------------------------

    public void CardInPlayEvent() {
        if(onCardInPlay != null) {
            onCardInPlay();
        }
    }

}

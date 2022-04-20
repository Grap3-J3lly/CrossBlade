using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    GameManager gameManager;
    GameObject interactionManager;
    [SerializeField] private GameObject interactionHandLeft;
    [SerializeField] private GameObject interactionHandRight;

    //------------------------------------------------------
    //                  GETTERS/SETTERS
    //------------------------------------------------------

    public GameObject GetInteractionHandLeft() {return interactionHandLeft;}
    public void SetInteractionHandLeft(GameObject newHandLeft) {interactionHandLeft = newHandLeft;}

    public GameObject GetInteractionHandRight() {return interactionHandRight;}
    public void SetInteractionHandRight(GameObject newHandRight) {interactionHandRight = newHandRight;}

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        gameManager = GameManager.Instance;
        interactionManager = gameObject;
        gameManager.SetInteractionController(this);
    }


}

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
    private bool readyToPlay = false;
    private GameManager gameManager;
    
    [SerializeField] private GameObject currentPlayer;

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Start() {
        interactionButton = GetComponent<InteractionButton>();
        gameManager = GameManager.Instance;
    }

    private void Update() {
        
    }

    //------------------------------------------------------
    //                  CONTROL FUNCTIONS
    //------------------------------------------------------

    public void ButtonHit() {
        //Debug.Log("Button has been hit successfully");
        
        readyToPlay = !readyToPlay;
        UpdateCorrectPlayerStatus();
    }

    public void StopHit() {
        //Debug.Log("Button no longer pressed");
        
    }

    private void UpdateCorrectPlayerStatus() {
        CheckGMInstance();

        List<GameObject> allPlayers = gameManager.GetCurrentPlayers();
        
        // Debug.Log("Is issue with gameManager list: " + gameManager);

        // Debug.Log("Is issue with Certain part of list: " + allPlayers[0]);
        // Debug.Log("Is issue with Certain part of list: " + allPlayers[0].name);
        // Debug.Log("Is issue with currentPlayer: " + currentPlayer);

        if(currentPlayer == allPlayers[0]) {
            Debug.Log("Changing Player One's Ready Status.");
            gameManager.SetPlayerOneReady(readyToPlay);
        }
        else {
            Debug.Log("Changing Player Two's Ready Status.");
            gameManager.SetPlayerTwoReady(readyToPlay);
        }
    }

    private void CheckGMInstance() {
        if(gameManager == null) {
            gameManager = GameManager.Instance;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SpawnController : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------
    
    // Public Variables
    public Vector3 startingPosition = new Vector3(0,0,0);

    // Private Variables
    private string playerType = "Prefabs/Players/";
    private GameManager gameManager;

    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Start() {
        StartCoroutine(InitialSetup());
    }

    //------------------------------------------------------
    //                  SETUP FUNCTIONS
    //------------------------------------------------------

    IEnumerator InitialSetup() {
        yield return new WaitUntil(() => GameManager.Instance != null);
        gameManager = GameManager.Instance;
        LoadHMDChoice();
        yield return new WaitUntil(() => gameManager.GetHeadsetChoiceMade());
        DeterminePlayerType();
        AddNewPlayer();
    }

    private void DeterminePlayerType() {
        bool usingMR = gameManager.GetUsingMixedReality();
        if(usingMR) {
            //isMr = true;
            playerType += "MR_Player";
        } else {
            //isMr = false;
            playerType += "VR_Player";
        }
    }

    private void LoadHMDChoice() {
        SceneManager.LoadSceneAsync("HMDChoice", LoadSceneMode.Additive);
    }

    private void AddNewPlayer() {
        Quaternion startingAngle;
        if(gameManager.GetCurrentPlayers().Count < 1) {
            startingPosition = gameManager.GetPlayerOneStartPos();
            startingAngle = gameManager.GetPlayerOneAngle();
        }
        else {
            startingPosition = gameManager.GetPlayerTwoStartPos();
            startingAngle = gameManager.GetPlayerTwoAngle();
        }

        PhotonNetwork.Instantiate(playerType, startingPosition, startingAngle);
    }

}

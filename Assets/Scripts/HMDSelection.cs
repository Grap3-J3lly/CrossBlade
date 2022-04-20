using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class HMDSelection : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------

    private GameManager gameManager;

    //------------------------------------------------------
    //                   GENERAL FUNCTIONS
    //------------------------------------------------------

    public void SelectVR() {
        gameManager.SetHeadsetChoiceMade(true);
        gameManager.SetUsingMixedReality(false);
        SceneManager.UnloadSceneAsync("HMDChoice");
    }

    public void SelectMR() {
        gameManager.SetHeadsetChoiceMade(true);
        gameManager.SetUsingMixedReality(true);
        SceneManager.UnloadSceneAsync("HMDChoice");
    }

    //------------------------------------------------------
    //                   STANDARD FUNCTIONS
    //------------------------------------------------------

    public void Start() {
        gameManager = GameManager.Instance;
    }
}

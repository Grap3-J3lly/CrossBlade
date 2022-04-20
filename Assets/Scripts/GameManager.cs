using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------

    // General Variables
    public static GameManager Instance;
    private InteractionController interactionController;
    private bool usingMixedReality;
    private bool headsetChoiceMade;
    private List<GameObject> currentPlayers = new List<GameObject>();

    // MR Variables
    private GameObject mrPlayer;
    private PhotonView mrView;
    // VR Variables
    private GameObject vrPlayer;
    private PhotonView vrView;
    

    //------------------------------------------------------
    //                   GETTERS/SETTERS
    //------------------------------------------------------

    // General Variables    

    public InteractionController GetInteractionController() {return interactionController;}
    public void SetInteractionController(InteractionController newController) {interactionController = newController;}

    public bool GetUsingMixedReality() {return usingMixedReality;}
    public void SetUsingMixedReality(bool newValue) {usingMixedReality = newValue;}

    public bool GetHeadsetChoiceMade() {return headsetChoiceMade;}
    public void SetHeadsetChoiceMade(bool newValue) {headsetChoiceMade = newValue;}

    public List<GameObject> GetCurrentPlayers() {return currentPlayers;}
    public void SetCurrentPlayers(List<GameObject> newPlayers) {currentPlayers = newPlayers;}

    // MR Variables
    public GameObject GetMrPlayer() {return mrPlayer;}
    public void SetMrPlayer(GameObject newPlayer) {mrPlayer = newPlayer;}

    public PhotonView GetMrView() {return mrView;}
    public void SetMrView(PhotonView newView) {mrView = newView;}

    // VR Variables
    public GameObject GetVrPlayer() {return vrPlayer;}
    public void SetVrPlayer(GameObject newPlayer) {vrPlayer = newPlayer;}

    public PhotonView GetVrView() {return vrView;}
    public void SetVrView(PhotonView newView) {vrView = newView;}

    

    //------------------------------------------------------
    //                   STANDARD FUNCTIONS
    //------------------------------------------------------

    public void Start() {
        Instance = this;
        Debug.Log("GameManager Instance: " + Instance);
    }

    

}

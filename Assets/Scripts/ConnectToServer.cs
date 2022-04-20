using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class ConnectToServer : MonoBehaviourPunCallbacks
{
    
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------
    
    public string startingSceneName;
    public string lobbySceneName = "Lobby";
    private bool finishedSceneLoad = false;
    private bool finishedSceneUnload = false;
    
    //------------------------------------------------------
    //                  STANDARD FUNCTIONS
    //------------------------------------------------------

    void Start()
    {
        startingSceneName = "LoadScreen"; 
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //------------------------------------------------------
    //                   CONNECTION FUNCTIONS
    //------------------------------------------------------

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        SceneManager.LoadSceneAsync(lobbySceneName);
    }

}

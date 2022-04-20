using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------

    public GameObject createInput;
    public GameObject joinInput;

    private string roomName;

    //------------------------------------------------------
    //                  CONNECTION FUNCTIONS
    //------------------------------------------------------

    public void CreateRoom() {
        roomName = createInput.GetComponent<TMP_InputField>().text;
        Debug.Log("Creating Room: " + roomName);
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom() {
        roomName = joinInput.GetComponent<TMP_InputField>().text;
        Debug.Log("Joining Room: " + roomName);
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("GameManager");
    }
}

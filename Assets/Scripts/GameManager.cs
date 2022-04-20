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

    [SerializeField] private Vector3 playerOneStartingPos;
    [SerializeField] private Vector3 playerTwoStartingPos;

    [SerializeField] private Vector3 playerOneStartingAngle;
    [SerializeField] private Vector3 playerTwoStartingAngle;

    private Quaternion playerOneAngle;
    private Quaternion playerTwoAngle;

    [SerializeField] private bool playerOneReady = false;
    [SerializeField] private bool playerTwoReady = false;

    private InteractionController interactionController;
    private bool usingMixedReality;
    private bool headsetChoiceMade;
    [SerializeField] private List<GameObject> currentPlayers = new List<GameObject>();

    [SerializeField] private List<GameObject> weaponObjects = new List<GameObject>();

    private GameLogic gameLogic;

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
    public Quaternion GetPlayerOneAngle() {return playerOneAngle;}
    public void SetPlayerOneAngle(Quaternion newAngle) {playerOneAngle = newAngle;}

    public Quaternion GetPlayerTwoAngle() {return playerTwoAngle;}
    public void SetPlayerTwoAngle(Quaternion newAngle) {playerTwoAngle = newAngle;}

    public bool GetPlayerOneReady() {return playerOneReady;}
    public void SetPlayerOneReady(bool newValue) {playerOneReady = newValue;}

    public bool GetPlayerTwoReady() {return playerTwoReady;}
    public void SetPlayerTwoReady(bool newValue) {playerTwoReady = newValue;}

    public InteractionController GetInteractionController() {return interactionController;}
    public void SetInteractionController(InteractionController newController) {interactionController = newController;}

    public bool GetUsingMixedReality() {return usingMixedReality;}
    public void SetUsingMixedReality(bool newValue) {usingMixedReality = newValue;}

    public bool GetHeadsetChoiceMade() {return headsetChoiceMade;}
    public void SetHeadsetChoiceMade(bool newValue) {headsetChoiceMade = newValue;}

    public List<GameObject> GetCurrentPlayers() {return currentPlayers;}
    public void SetCurrentPlayers(List<GameObject> newPlayers) {currentPlayers = newPlayers;}

    public List<GameObject> GetWeaponObjects() {return weaponObjects;}
    public void SetWeaponObjects(List<GameObject> newList) {weaponObjects = newList;}

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

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        playerOneAngle = Quaternion.Euler(playerOneStartingAngle);
        playerTwoAngle = Quaternion.Euler(playerTwoStartingAngle);
    }

    private void Update() {
        CheckForGameStart();
    }

    //------------------------------------------------------
    //                   GAME LOGIC FUNCTIONS
    //------------------------------------------------------

    private void CheckForGameStart() {
        if(playerOneReady && playerTwoReady) {
            playerOneReady = false;
            playerTwoReady = false;
            gameLogic = gameObject.AddComponent<GameLogic>();
            gameLogic.SetReadyToStart(true);
            gameLogic.StartGame();
        }
    }

}

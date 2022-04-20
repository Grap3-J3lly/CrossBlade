using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------

    // General Variables
    private bool usingMixedReality;
    private GameManager gameManager;
    private PhotonView view;
    private bool newPlayerAdded;

    private DeckController deckController;

    // MR Player Variables
    [SerializeField] private GameObject mrCamera1;
    [SerializeField] private GameObject mrCamera2;

    // VR Player Variables
    [SerializeField] private GameObject vrCamera;
    
    //------------------------------------------------------
    //                   GETTERS/SETTERS
    //------------------------------------------------------

    // General Variables
    public bool GetUsingMixedReality() {return usingMixedReality;}
    public void SetUsingMixedReality(bool newValue) {usingMixedReality = newValue;}

    public DeckController GetDeckController() {return deckController;}
    public void SetDeckController(DeckController newController) {deckController = newController;}

    // MR Player Variables
    public GameObject GetMrCamera1() {return mrCamera1;}
    public void SetMrCamera1(GameObject newCamera) {mrCamera1 = newCamera;}

    public GameObject GetMrCamera2() {return mrCamera2;}
    public void SetMrCamera2(GameObject newCamera) {mrCamera2 = newCamera;}
    
    // VR Player Variables
    public GameObject GetVrCamera() {return vrCamera;}
    public void SetVrCamera(GameObject newCamera) {vrCamera = newCamera;}

    //------------------------------------------------------
    //                   STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Awake() {
        newPlayerAdded = true;
        gameManager = GameManager.Instance;
        gameManager.GetCurrentPlayers().Add(gameObject);
        AssignParent(gameManager.gameObject);
        StartCoroutine(HandleOtherPlayers());
    }

    private void Start()
    {
        newPlayerAdded = false;
        StartCoroutine(InitialSetup());
        if(view.IsMine && usingMixedReality) {
            Debug.Log("Spawned A MR Player");
            HandleMRSetup(gameManager.GetVrPlayer().GetComponent<PlayerController>());
        }
        if(view.IsMine && !usingMixedReality) {
            Debug.Log("Spawned A VR Player");
            
            HandleVRSetup(gameManager.GetMrPlayer().GetComponent<PlayerController>());
        }
        AssignDeck();
    }

    private void Update() {
        
    }

    //------------------------------------------------------
    //                  SETUP FUNCTIONS
    //------------------------------------------------------

    IEnumerator InitialSetup() {
        usingMixedReality = gameManager.GetUsingMixedReality();
        view = GetComponent<PhotonView>();
        yield return new WaitUntil(()=> view != null);
    }

    private void HandleMRSetup(PlayerController vrController) {
        gameManager.SetMrPlayer(gameObject);

        vrController.GetVrCamera().SetActive(false);
    }

    private void HandleVRSetup(PlayerController mrController) {
        gameManager.SetVrPlayer(gameObject);

        mrController.GetMrCamera1().SetActive(false);
        mrController.GetMrCamera2().SetActive(false);
    }

    //------------------------------------------------------
    //                  CUSTOM FUNCTIONS
    //------------------------------------------------------

    IEnumerator HandleOtherPlayers() {
        newPlayerAdded = false;
        yield return new WaitUntil(() => !newPlayerAdded);

        Debug.Log("Running Player Handler");
        if(!(view.IsMine)) {
            if(usingMixedReality) {
                HandleMRSetup(this);
            }
            if(!usingMixedReality) {
                HandleVRSetup(this);
            }
        }
        

    }

    public void AssignDeck() {
        deckController = gameObject.GetComponentInChildren<DeckController>();
    }

    private void AssignParent(GameObject targetParent) {
        this.transform.SetParent(targetParent.transform);
    }

}

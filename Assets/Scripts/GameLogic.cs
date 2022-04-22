using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------

    private GameManager gameManager;

    [SerializeField] private bool readyToStart = false;
    private List<GameObject> players = new List<GameObject>();

    private DeckController playerOneDeckController;
    private DeckController playerTwoDeckController;

    private ItemController playerOneItemController;
    private ItemController playerTwoItemController;


    //------------------------------------------------------
    //                   GETTERS/SETTERS
    //------------------------------------------------------

    public bool GetReadyToStart() {return readyToStart;}
    public void SetReadyToStart(bool changeValue) {readyToStart = changeValue;}


    //------------------------------------------------------
    //              GAME LOGIC FUNCTIONS
    //------------------------------------------------------

    public void StartGame() {
        if(readyToStart) {
            readyToStart = false;
            gameManager = GameManager.Instance;
            players = gameManager.GetCurrentPlayers();
            PlayerOnePrep(players[0]);
            PlayerTwoPrep(players[1]);

        }
    }

    private void PlayerOnePrep(GameObject playerOne) {
        GrowCard(playerOne);
        playerOneDeckController = playerOne.GetComponent<PlayerController>().GetDeckController();
        playerOneItemController = playerOne.GetComponent<PlayerController>().GetItemController();

        Card cardInUse = playerOneDeckController.GetCardInPlay();

        playerOneItemController.HandleItemSpawn(cardInUse.weapon);
    }

    private void PlayerTwoPrep(GameObject playerTwo) {
        GrowCard(playerTwo);
        
        playerTwoDeckController = playerTwo.GetComponent<PlayerController>().GetDeckController();
        playerTwoItemController = playerTwo.GetComponent<PlayerController>().GetItemController();

        Card cardInUse = playerTwoDeckController.GetCardInPlay();

        playerTwoItemController.HandleItemSpawn(cardInUse.weapon);
    }

    private void GrowCard(GameObject currentPlayer) {
        CardController inPlayCard = GetInPlayCard(currentPlayer.GetComponent<PlayerController>());
        inPlayCard.ChangeSize(false);
    }

    private CardController GetInPlayCard(PlayerController playerController) {
        playerController.AssignDeck();
        DeckController playerDeck = playerController.GetDeckController();
        Card mainCardInPlay = playerDeck.GetCardInPlay();
        CardController inPlayCard = mainCardInPlay.GetCardController();
        return inPlayCard;
    }

}

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
    List<GameObject> players = new List<GameObject>();


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
            GrowCard(players[0]);
            GrowCard(players[1]);
        }
    }

    private void GrowCard(GameObject currentPlayer) {
        CardController inPlayCard = GetInPlayCard(currentPlayer.GetComponent<PlayerController>());
        inPlayCard.ChangeSize(false);
    }

    private CardController GetInPlayCard(PlayerController playerController) {
        Debug.Log("Reached Beginning of CardController grab. PlayerController: " + playerController);
        playerController.AssignDeck();
        DeckController playerDeck = playerController.GetDeckController();
        Debug.Log("Grabbed DeckController: " + playerDeck);
        Debug.Log("DeckController's specific cardList size: " + playerDeck.cards.Count);
        Card mainCardInPlay = playerDeck.GetCardInPlay();
        Debug.Log("Grabbed Main Card: " + mainCardInPlay);
        CardController inPlayCard = mainCardInPlay.GetCardController();
        return inPlayCard;
    }

}

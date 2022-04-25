using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

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

    private Card playerOneCard;
    private Card playerTwoCard;

    public List<Card.Weapon> greatSwordWeakness = new List<Card.Weapon> {
        Card.Weapon.Dagger,
        Card.Weapon.Halberd,
        Card.Weapon.Bow
    };
    public List<Card.Weapon> towerShieldWeakness = new List<Card.Weapon> {
        Card.Weapon.Greatsword
    };

    private bool playerOneWin = false;
    private bool playerTwoWin = false;
    private bool playersTied = false;

    private TMP_Text gameOverText;

    [SerializeField] private string winText = "YOU WIN";
    [SerializeField] private string loseText = "GAME OVER";

    [SerializeField] private string tieText = "SAME WEAPON CHOSEN";

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

            StartCoroutine(DetermineWinner(players[0], players[1]));
        }
    }

    private void PlayerOnePrep(GameObject playerOne) {
        GrowCard(playerOne);
        playerOneDeckController = playerOne.GetComponent<PlayerController>().GetDeckController();
        playerOneItemController = playerOne.GetComponent<PlayerController>().GetItemController();

        playerOneCard = playerOneDeckController.GetCardInPlay();

        playerOneItemController.HandleItemSpawn(playerOneCard.weapon);
    }

    private void PlayerTwoPrep(GameObject playerTwo) {
        GrowCard(playerTwo);
        
        playerTwoDeckController = playerTwo.GetComponent<PlayerController>().GetDeckController();
        playerTwoItemController = playerTwo.GetComponent<PlayerController>().GetItemController();

        playerTwoCard = playerTwoDeckController.GetCardInPlay();

        playerTwoItemController.HandleItemSpawn(playerTwoCard.weapon);
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

    IEnumerator DetermineWinner(GameObject playerOne, GameObject playerTwo) {
        PhotonView p1View = playerOne.GetComponent<PhotonView>();
        PhotonView p2View = playerTwo.GetComponent<PhotonView>();
        yield return new WaitForSeconds(10);

        FindWeakness(playerOneCard.weapon, playerTwoCard.weapon);
        gameManager.GetGameOverCanvas().gameObject.SetActive(true);
        AssignGameOverText(playerOne, playerTwo);

        Debug.Log("PLAYER ONE WIN/LOSE: " + playerOneWin);
        Debug.Log("PLAYER TWO WIN/LOSE: " + playerTwoWin);
        Debug.Log("PLAYERS TIED: " + playersTied);
    }

    private void FindWeakness(Card.Weapon playerOneWeapon, Card.Weapon playerTwoWeapon) {
        Debug.Log("Entering FindWeakness");
        if(playerOneWeapon == Card.Weapon.Greatsword) {
            if(playerOneWeapon == playerTwoWeapon) {
                playerOneWin = false;
                playerTwoWin = false;
                playersTied = true;
                return;
            }
            else if(greatSwordWeakness.Contains(playerTwoWeapon)) {
                playerOneWin = false;
                playerTwoWin = true;
                playersTied = false;
                return;
            }
            else {
                playerOneWin = true;
                playerTwoWin = false;
                playersTied = false;
                return;
            }
        }
        if(playerOneWeapon == Card.Weapon.TowerShield) {
            if(playerOneWeapon == playerTwoWeapon) {
                playerOneWin = false;
                playerTwoWin = false;
                playersTied = true;
                return;
            }
            else if(towerShieldWeakness.Contains(playerTwoWeapon)) {
                playerOneWin = false;
                playerTwoWin = true;
                playersTied = false;
                return;
            }
            else {
                playerOneWin = true;
                playerTwoWin = false;
                playersTied = false;
                return;
            }
        }
    }

    private void AssignGameOverText(GameObject playerOne, GameObject playerTwo) {
        
        gameOverText = gameManager.GetGameOverCanvas().GetComponentInChildren<TextMeshProUGUI>();

        Debug.Log("GameOverText Object: " + gameOverText);

        if(playersTied) {
            gameOverText.text = tieText;
            return;
        }

        if(playerOne == gameManager.GetCurrentPlayer()) {
            if(playerOneWin) {
                gameOverText.text = winText;
                return;
            }
            else{
                gameOverText.text = loseText;
                return;
            }
        }
        else {
            if(playerTwoWin) {
                gameOverText.text = winText;
                return;
            }
            else{
                gameOverText.text = loseText;
                return;
            }
        }
    }

}

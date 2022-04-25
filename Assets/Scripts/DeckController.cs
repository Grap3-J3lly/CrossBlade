using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------

    [SerializeField] private Vector3 deckStartLocation = new Vector3(.3f, .51f, -.3f);

    public enum DeckType {
        Default,
        Attack,
        Defense
    }

    public DeckType deckType;

    public List<GameObject> cardOptions = new List<GameObject>();

    public List<Card> cards = new List<Card>();

    //------------------------------------------------------
    //                   STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Start() {
        HandleCardSpawn();
    }

    private void Update() {
        
    }

    //------------------------------------------------------
    //                   SETUP FUNCTIONS
    //------------------------------------------------------
    
    private void HandleCardSpawn() {
        if(deckType == DeckType.Default) {
            SpawnDeckDefault();
        }
        if(deckType == DeckType.Attack) {
            SpawnDeckAttack();
        }
        if(deckType == DeckType.Defense) {
            SpawnDeckDefense();
        }
        
    }

    //------------------------------------------------------
    //              DECK SPAWN FUNCTIONS
    //------------------------------------------------------

    private void SpawnDeckDefault() {
        SpawnCardGreatsword();
        SpawnCardTowerShield();
    }

    private void SpawnDeckAttack() {
        SpawnCardGreatsword();
    }

    private void SpawnDeckDefense() {
        SpawnCardTowerShield();
    }

    //------------------------------------------------------
    //              CARD SPAWN FUNCTIONS
    //------------------------------------------------------

    private void SpawnCardGreatsword() {
        Card card = new Card();
        
        GameObject greatSwordObject = cardOptions.Find(cardToFind => cardToFind.name.ToLower().Contains("greatsword"));

        GameObject newGreatsword = (GameObject)Instantiate(greatSwordObject, transform.position, gameObject.transform.rotation);
        newGreatsword.transform.SetParent(transform);

        card.HandleCardSetup("GreatswordCard", newGreatsword, Card.Weapon.Greatsword, cards);
        
    }

    private void SpawnCardTowerShield() {
        Card card = new Card();
        
        GameObject towerShieldObject = cardOptions.Find(cardToFind => cardToFind.name.ToLower().Contains("towershield"));

        GameObject newTowerShield = (GameObject)Instantiate(towerShieldObject, transform.position, gameObject.transform.rotation);
        newTowerShield.transform.SetParent(transform);
        
        card.HandleCardSetup("TowerShieldCard", newTowerShield, Card.Weapon.TowerShield, cards);

    }

    public Card GetCardInPlay() {

        Card inPlayCard = cards.Find(cardToFind => cardToFind.inPlay == true);
        return inPlayCard;
    }

}

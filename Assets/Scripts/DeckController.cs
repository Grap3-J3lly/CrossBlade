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
        // Debug.Log("CARDS LIST COUNT: " + cards.Count);
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
        card.cardName = "GreatswordCard";
        GameObject greatSwordObject = cardOptions.Find(cardToFind => cardToFind.name.ToLower().Contains("greatsword"));

        GameObject newGreatsword = (GameObject)Instantiate(greatSwordObject, transform.position, greatSwordObject.transform.rotation);
        newGreatsword.transform.SetParent(transform);
        card.cardObject = newGreatsword;
        card.weapon = Card.Weapon.Greatsword;
        card.AssignWeaponObject(card.weapon);

        cards.Add(card);
        Debug.Log("ADDED TO CARDS");
    }

    private void SpawnCardTowerShield() {
        Card card = new Card();
        card.cardName = "TowerShieldCard";
        GameObject towerShieldObject = cardOptions.Find(cardToFind => cardToFind.name.ToLower().Contains("towershield"));

        GameObject newTowerShield = (GameObject)Instantiate(towerShieldObject, transform.position, towerShieldObject.transform.rotation);
        newTowerShield.transform.SetParent(transform);
        card.cardObject = newTowerShield;
        card.weapon = Card.Weapon.TowerShield;
        card.AssignWeaponObject(card.weapon);

        cards.Add(card);
        Debug.Log("ADDED TO CARDS");
    }

    public Card GetCardInPlay() {

        Debug.Log("Card List size: " + cards.Count);

        Card inPlayCard = cards.Find(cardToFind => cardToFind.inPlay == true);
        return inPlayCard;
    }

}

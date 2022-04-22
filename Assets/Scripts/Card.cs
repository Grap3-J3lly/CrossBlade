using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    //------------------------------------------------------
    //                   VARIABLES
    //------------------------------------------------------

    private GameManager gameManager;

    public string cardName;
    public GameObject cardObject;

    private CardController cardController;

    public enum Weapon {
        Greatsword,
        TowerShield
    }

    public Weapon weapon;

    public GameObject weaponObject;

    public bool inPlay = false;

    //------------------------------------------------------
    //                   GETTERS/SETTERS
    //------------------------------------------------------

    public CardController GetCardController() {return cardController;}
    public void SetCardController(CardController newController) {cardController = newController;}

    //------------------------------------------------------
    //                   SPAWN FUNCTIONS
    //------------------------------------------------------

    public void AssignWeaponObject(Weapon weaponType) {
        if(gameManager == null) {
            gameManager = GameManager.Instance;
        }
        List<GameObject> weaponList = gameManager.GetWeaponObjects();
        if(weaponType == Weapon.Greatsword) {
            weaponObject = FindSpecificWeapon(weaponList, "greatsword");
        }

        if(weaponType == Weapon.TowerShield) {
            weaponObject = FindSpecificWeapon(weaponList, "towershield");
        }
    }

    private GameObject FindSpecificWeapon(List<GameObject> weaponList, string weaponName) {
        return weaponList.Find(weaponToFind => weaponToFind.name.ToLower().Contains(weaponName));
    }

    public void UpdateInPlayCard() {
        // Need event in Card Controller to call this method
        inPlay = cardObject.GetComponent<CardController>().GetInPlay();
    }

    public void HandleCardSetup(string newCardName, GameObject newObject, Weapon weaponType, List<Card> cardList) { {}
        cardName = newCardName;
        cardObject = newObject;
        cardController = cardObject.GetComponent<CardController>();
        cardController.onCardInPlay += UpdateInPlayCard;
        weapon = weaponType;
        AssignWeaponObject(weapon);
        cardList.Add(this);
    }

}

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

    public CardController GetCardController() {return cardObject.GetComponent<CardController>();}

    //------------------------------------------------------
    //                   STANDARD FUNCTIONS
    //------------------------------------------------------

    private void Start() {
        gameManager = GameManager.Instance;
    }

    private void Update() {
        CheckController();
    }

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

    private void CheckController() {
        if(cardObject != null) {
            inPlay = cardObject.GetComponent<CardController>().GetInPlay();
        }
    }

}

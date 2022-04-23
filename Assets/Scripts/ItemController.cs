using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    //------------------------------------------------------
    //                  VARIABLES
    //------------------------------------------------------

    [SerializeField] private GameObject deck;
    private DeckController deckController;

    [SerializeField] private GameObject cardSpawn;
    [SerializeField] private GameObject weaponSpawn;

    private WeaponTransform weaponTransform;

    //------------------------------------------------------
    //                  CARD SPAWNS
    //------------------------------------------------------

    private void SpawnLargeCard(Card chosenCard) {
        GameObject largeCard = chosenCard.cardObject;
        largeCard.transform.position = cardSpawn.transform.position;
        largeCard.transform.rotation = cardSpawn.transform.rotation;
    }

    private void SuspendCard(Card chosenCard) {
        GameObject largeCard = chosenCard.cardObject;
        largeCard.GetComponent<Rigidbody>().useGravity = false;
        largeCard.GetComponent<Rigidbody>().isKinematic = true;

    }

    //------------------------------------------------------
    //                  WEAPON SPAWNS
    //------------------------------------------------------

    private void SpawnObjectGreatsword(Card chosenCard) {
        GameObject chosenWeapon = chosenCard.weaponObject;
        List<Vector3> greatSwordTransform = weaponTransform.defaultGreatswordTransform;
        AssignParent(weaponSpawn, chosenWeapon);
        
        chosenWeapon.transform.localPosition = greatSwordTransform[0];
        chosenWeapon.transform.localRotation = Quaternion.Euler(greatSwordTransform[1]);
        chosenWeapon.transform.localScale = greatSwordTransform[2];
    }

    private void SpawnObjectTowerShield(Card chosenCard) {
        GameObject chosenWeapon = chosenCard.weaponObject;
        List<Vector3> towerShieldTransform = weaponTransform.defaultTowerShieldTransform;
        AssignParent(weaponSpawn, chosenWeapon);
        
        Debug.Log("TowerShield Transform Position: " + towerShieldTransform[0]);
        chosenWeapon.transform.localPosition = towerShieldTransform[0];
        chosenWeapon.transform.localRotation = Quaternion.Euler(towerShieldTransform[1]);
        chosenWeapon.transform.localScale = towerShieldTransform[2];
    }

    //------------------------------------------------------
    //                  GENERAL FUNCTIONS
    //------------------------------------------------------

    public void HandleItemSpawn(Card.Weapon itemType) {
        deckController = deck.GetComponent<DeckController>();
        weaponTransform = new WeaponTransform();

        Card chosenCard = deckController.GetCardInPlay();
        SpawnLargeCard(chosenCard);
        SuspendCard(chosenCard);
        if(itemType == Card.Weapon.Greatsword) {
            SpawnObjectGreatsword(chosenCard);
        }

        if(itemType == Card.Weapon.TowerShield) {
            SpawnObjectTowerShield(chosenCard);
        }
    }

    private void AssignParent(GameObject expectedParent, GameObject child) {
        child.transform.SetParent(expectedParent.transform);
    }

}

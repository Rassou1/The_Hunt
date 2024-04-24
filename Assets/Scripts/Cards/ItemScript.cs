using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject equippedCard;
    public GameObject player;
    public CardInventory inv;
    // Start is called before the first frame update
    
    void Start()
    {
        equippedCard.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                //Turns off the gameobject of the card on the floor; can no longer be picked up.
                //equippedCard.SetActive(true);
                inv = player.GetComponent<CardInventory>();
                //references player's inventory, sends the added card into the player's inv. Need to check if this works with the networking.
                inv.AddCard();

            }
        }
    }
    
}

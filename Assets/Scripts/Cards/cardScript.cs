using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public int cardNumber;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Card " + cardNumber + " picked up");
        //Tells us when a card is picked up. For debugging.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            Debug.Log("Card " + cardNumber + "  used");
            Destroy(this);
            //Tells us when a card is used, and destroys the card. Might have to remove from inventory list. Not sure.
            //Currently dumps all cards in inventory and destroys them instead of just the equipped one. Need urgent fix.
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventory : MonoBehaviour
{
    List<Card> cards = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard()
    {
        GameObject newCard = new GameObject();
        Card card = newCard.AddComponent<Card>();
        cards.Add(card);
        newCard.transform.SetParent(this.transform);
        card.cardNumber = cards.Count;
    }
    //Adds a new card to the character's inventory.
}

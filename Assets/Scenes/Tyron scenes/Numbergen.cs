using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numbergen : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Deactivate the button
        gameObject.SetActive(false);

        // Find all GameObjects with the "Player" tag
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Check if there are players
        if (players.Length > 0)
        {
            // Select a random index to make it a Hunter
            int hunterIndex = Random.Range(0, players.Length);

            for (int i = 0; i < players.Length; i++)
            {
                // Set the layer of the player to "Hunter" if it's the selected index
                if (i == hunterIndex)
                {
                    players[i].layer = LayerMask.NameToLayer("Hunter");
                }
                else
                {
                    // Set the layer of other players to "Prey"
                    players[i].layer = LayerMask.NameToLayer("Prey");
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

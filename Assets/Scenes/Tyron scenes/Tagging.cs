using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlayer : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Check if the interacting player is tagged as "hunter"
        if (gameObject.CompareTag("hunter"))
        {
            // Get the tag of the interacted player
            string interactedPlayerTag = gameObject.tag;

            // Check if the interacted player is tagged as "prey"
            if (interactedPlayerTag == "prey")
            {
                // Teleport the prey player to the specified position
                transform.position = new Vector3(63.7f, 10.58f, -17.28f);
            }
        }
    }
}
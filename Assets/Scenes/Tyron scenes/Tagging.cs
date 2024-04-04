using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlayer : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Get the tag of the interacted player
        string interactedPlayerTag = gameObject.tag;

        // Get the tag of the interacting player
        //string interactingPlayerTag = interactor.tag;

        // Check if the interacted player is tagged as "prey" and the interacting player is tagged as "hunter"
        //if (interactedPlayerTag == "prey" && interactingPlayerTag == "hunter")
        //{
        //    // Teleport the prey player to the specified position
        //    transform.position = new Vector3(63.7f, 10.58f, -17.28f);
        //}
    }
}

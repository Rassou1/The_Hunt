using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagging : MonoBehaviour, IInteractable
{
    public string hunterTag = "Hunter";
    public string preyTag = "Prey";

    public void Interact()
    {
        // Check if the interacting player is tagged as "Hunter" and if the interacted player is tagged as "Prey"
        if (gameObject.CompareTag(hunterTag) && transform.CompareTag(preyTag))
        {
            Debug.Log("Hunter interacted with Prey!");
            // Perform actions for when the hunter interacts with prey here
        }
    }
}
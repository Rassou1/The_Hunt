using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class InteractablePlayer : AttributesSync, IInteractable
{
    public bool tagged = false; // Boolean to track if the prey is tagged

    // This field will be synchronized if it's properly configured with your networking framework
    [SynchronizableField]
    public Vector3 syncedPosition = new Vector3(63.7f, 10.58f, -17.28f);

    public void Interact(GameObject interactor)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Prey") && interactor.layer == LayerMask.NameToLayer("Hunter"))
        {
            // Set the synchronized position
            transform.position = syncedPosition;

            // Update the tagged state
            tagged = true;
        }
    }

    public void Update()
    {
        // You can remove this part if you don't need it for other purposes
        if (tagged)
            Debug.Log("Prey has been tagged");
    }
}

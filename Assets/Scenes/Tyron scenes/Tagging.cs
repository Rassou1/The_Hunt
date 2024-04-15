using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class InteractablePlayer : AttributesSync, IInteractable
{
    public bool tagged = false; // Boolean to track if the prey is tagged

    // This field will be synchronized if it's properly configured with your networking framework
    public Vector3 prisonPosition = new Vector3(63.7f, 10.58f, -17.28f);
    public Alteruna.Avatar _avatar;

    public void Start()
    {
        _avatar = gameObject.GetComponent<Alteruna.Avatar>();

    }
    public void Interact(GameObject interactor)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Prey") && interactor.layer == LayerMask.NameToLayer("Hunter"))
        {
            if (!_avatar.IsMe)
                return;

            // Set the synchronized position
            transform.position = prisonPosition;

            // Update the tagged state
            tagged = true;
        }
    }

    public void Update()
    {
        if (tagged)
            Debug.Log("Prey has been tagged");
    }
}

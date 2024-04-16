using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class InteractablePlayer : AttributesSync, IInteractable
{
    public bool tagged = false; // Boolean to track if the prey is tagged

    public Vector3 prisonPosition = new Vector3(63.7f, 10.58f, -17.28f);
    public Alteruna.Avatar _avatar;
    public string myName ;
    public void Start()
    {
        _avatar = gameObject.GetComponent<Alteruna.Avatar>();
    }

    public void Interact(GameObject interactor)
    {
        myName= gameObject.name;
        if (gameObject.layer == LayerMask.NameToLayer("Prey") && interactor.layer == LayerMask.NameToLayer("Hunter"))
        {
            //if (!_avatar.IsMe) 
            //    return;

            //gameObject.transform.position = prisonPosition;

            Debug.Log(gameObject.layer + gameObject.name + " has been tagged by "+ interactor.layer + interactor.name);

        }

        //Debug.Log(gameObject.name+" is "+gameObject.layer);
        //Debug.Log(interactor.name + " is " + interactor.layer);
    }

    public GameObject GiveObject()
    {
        return gameObject;
    }
}

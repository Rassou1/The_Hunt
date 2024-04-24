using Alteruna;
using UnityEngine;

public class InteractablePlayer : AttributesSync, IInteractable
{
    public bool tagged = false; // Boolean to track if the prey is tagged

    public Vector3 prisonPosition = new Vector3(63.7f, 10.58f, -17.28f);
    public Alteruna.Avatar _avatar;
    private string myName;

    public void Start()
    {
        myName = gameObject.name;
    }


    public GameObject GiveObject()
    {
        return gameObject;
    }

    [SynchronizableMethod]
     
    public void Interact(GameObject interactor, Alteruna.Avatar interactorAvatar)
    {
        //if (!_avatar.IsMe)
        //    return;

        // Check if both the interactor and the player are on specific layers
        if (interactor.layer == LayerMask.NameToLayer("Hunter") && gameObject.layer == LayerMask.NameToLayer("Prey"))
        {
            // Teleport the player to the prison position
            GetComponent<TransformSynchronizable>().transform.position = prisonPosition;

        }
        ForceSync();
    }
}

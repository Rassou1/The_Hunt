using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using System;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;
/// <summary>
/// Script written by: ---
/// Fixed for th hunter by Hamdi.
/// </summary>
interface HIInteractable
{
    public void Interact(string hInteractor);

    public void InitInteract(string interactor);

    public GameObject GiveObject();
}
public class Hunter_Interactor : AttributesSync
{
    // Start is called before the first frame update
    private Alteruna.Avatar _avatar;


    public GameObject InteractorCam;

    public Transform InteractorSource;
    public float InteractRange;


    [SerializeField] private GameObject me;
    private float sphereRadius = 0.5f; // Adjust this value as needed
    void Start()
    {
        _avatar = gameObject.GetComponentInParent<Alteruna.Avatar>();
        me = gameObject;
    }

    public void SetAttack()
    {
        InvokeRemoteMethod("AttackRemote", UserId.All);
    }



    [SynchronizableMethod]
    public void AttackRemote()
    {
        Attack();

    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(InteractorCam.transform.position, InteractorCam.transform.forward * InteractRange, Color.magenta);


    }






    [SynchronizableMethod]
    public void Attack()
    {

        if (InteractorCam == null)
        {
            Debug.LogError("InteractorCam is not assigned.");
            return;
        }

        if (_avatar == null)
        {
            Debug.LogError("The avatar initiating the interaction is not assigned.");
            return;
        }

        // Define the radius for the SphereCast
        

        Ray ray = new Ray(InteractorCam.transform.position, InteractorCam.transform.forward);

        // Perform the SphereCast
        if (Physics.SphereCast(InteractorCam.transform.position, sphereRadius, InteractorCam.transform.forward, out RaycastHit hitInfo, InteractRange))
        {

            Debug.Log(hitInfo.transform.gameObject.name);
            IInteractable interactObj = hitInfo.collider.gameObject.GetComponentInParent<IInteractable>();

            if (interactObj != null)
            {
                interactObj.InitInteract(_avatar.name);
                Debug.Log($"{gameObject.name} interacted with {interactObj.GiveObject().name} at position {hitInfo.point}");
            }
            else
            {
                Debug.Log("No interactable object found!");
            }
        }
        else
        {
            Debug.Log("SphereCast did not hit any objects.");
        }
    }


    public void InteractionRay()
    {
        if (InteractorCam == null)
        {
            Debug.LogError("InteractorCam is not assigned.");
            return;
        }

        if (me == null)
        {
            Debug.LogError("The object initiating the interaction is not assigned.");
            return;
        }

        // Define the radius for the SphereCast

        Ray ray = new Ray(InteractorCam.transform.position, InteractorCam.transform.forward);
        Debug.DrawRay(InteractorCam.transform.position, InteractorCam.transform.forward * InteractRange, Color.magenta);
        Debug.Log("Attempting interaction");
        QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Collide; 
        // Perform the SphereCast
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hitInfo, InteractRange,7, queryTriggerInteraction))
        {
            IInteractable interactObj = hitInfo.collider.gameObject.GetComponentInParent<IInteractable>();

            if (interactObj != null)
            {
                interactObj.InitInteract(me.name)
                    ;
                Debug.Log($"{gameObject.name} interacted with {interactObj.GiveObject().name} at position {hitInfo.point}");
            }
            else
            {
                Debug.Log("No interactable object found!");
            }
        }
        else
        {
            Debug.Log("SphereCast did not hit any objects.");
        }
    }

}
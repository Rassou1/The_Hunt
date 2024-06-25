using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using System;
using Unity.VisualScripting;
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
public class Hunter_Interactor : MonoBehaviour
{
    // Start is called before the first frame update
    private Alteruna.Avatar _avatar;


    public GameObject InteractorCam;

    public Transform InteractorSource;
    public float InteractRange;


    [SerializeField] private GameObject me;

    void Start()
    {
        _avatar = gameObject.GetComponentInParent<Alteruna.Avatar>();
        me = gameObject;
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    public void Attack()
    {
        Ray ray = new Ray(InteractorCam.transform.position, InteractorCam.transform.forward);


        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            IInteractable interactObj = hitInfo.collider.gameObject.GetComponentInParent<IInteractable>();


            if (interactObj != null)
            {
                interactObj.InitInteract(_avatar.name);

                Debug.Log(gameObject.name + " interacted with " + interactObj.GiveObject().name);
            }
            else
            {
                Debug.Log("No interactable object found!");
            }
        }
    }

    public void InteractionRay()
    {
        Ray ray = new Ray(InteractorCam.transform.position, InteractorCam.transform.forward);
        Debug.DrawRay(InteractorCam.transform.position, InteractorCam.transform.forward * InteractRange, Color.magenta);
        Debug.Log("Attacking");
        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            IInteractable interactObj = hitInfo.collider.gameObject.GetComponentInParent<IInteractable>();


            if (interactObj != null)
            {
                interactObj.InitInteract(me.name);

                Debug.Log(gameObject.name + " interacted with " + interactObj.GiveObject().name);
            }
            else
            {
                Debug.Log("No interactable object found!");
            }
        }
    }
}
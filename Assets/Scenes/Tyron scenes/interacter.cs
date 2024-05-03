using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
    interface IInteractable
    {
        public void Interact(GameObject interactor, Alteruna.Avatar interactorAvatar);
        
        
        public GameObject GiveObject();
    }

public class interacter : MonoBehaviour
{
    // Start is called before the first frame update
    public Alteruna.Avatar _avatar;


    public GameObject InteractorCam;
    public GameObject roleGiver;

    public Transform InteractorSource;
    public float InteractRange;
    void Start()
    {
        _avatar = gameObject.GetComponent<Alteruna.Avatar>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(InteractorCam.transform.position, InteractorCam.transform.forward * InteractRange, Color.green);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(InteractorCam.transform.position, InteractorCam.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
            {
                IInteractable interactObj = hitInfo.collider.gameObject.GetComponentInParent<IInteractable>();


                if (interactObj != null)//check not me
                {
                    interactObj.Interact(gameObject,_avatar);
                    roleGiver.GetComponentInParent<RoleGiver>().Tag(gameObject, interactObj.GiveObject());

                    Debug.Log(gameObject.name + " interacted with " + interactObj.GiveObject().name);
                }
                else
                {
                    Debug.Log("No interactable object found!");
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    interface IInteractable
    {
        public void Interact(GameObject interactor);
    }
public class interacter : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject InteractorCam;

    public Transform InteractorSource;
    public float InteractRange;
    void Start()
    {
        
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

                if (interactObj!=null)
                {
                    Debug.Log(interactObj+" has been interacted with by "+ gameObject);
                    interactObj.Interact(gameObject);
                }
            }
        }
    }

}

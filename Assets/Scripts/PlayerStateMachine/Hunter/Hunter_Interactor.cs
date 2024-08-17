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

    public GameObject taggingCube;

    public float cubeLifetime = 5f;

    // Method to spawn the taggingCube
    public void SpawnCube()
    {
        // Get the camera's transform
        Transform cameraTransform = InteractorCam.transform;

        // Calculate the position in front of the camera
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * taggingCube.transform.localScale.z;

        // Instantiate the taggingCube at the calculated position and rotation
        GameObject spawnedCube = Instantiate(taggingCube, spawnPosition, cameraTransform.rotation);

        // Attach the CubeLifetime script to handle destruction and collision tracking
        spawnedCube.AddComponent<CubeLifetime>().Initialize(cubeLifetime);
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
        SpawnCube();
        QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Collide;
        // Perform the SphereCast
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hitInfo, InteractRange, 7, queryTriggerInteraction))
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


    //public void InteractionRay()
    //{
    //    if (InteractorCam == null)
    //    {
    //        Debug.LogError("InteractorCam is not assigned.");
    //        return;
    //    }

    //    if (me == null)
    //    {
    //        Debug.LogError("The object initiating the interaction is not assigned.");
    //        return;
    //    }

    //    // Define the radius for the SphereCast

    //    Ray ray = new Ray(InteractorCam.transform.position, InteractorCam.transform.forward);
    //    Debug.DrawRay(InteractorCam.transform.position, InteractorCam.transform.forward * InteractRange, Color.magenta);
    //    Debug.Log("Attempting interaction");
    //    QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Collide; 
    //    // Perform the SphereCast
    //    if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hitInfo, InteractRange,7, queryTriggerInteraction))
    //    {
    //        IInteractable interactObj = hitInfo.collider.gameObject.GetComponentInParent<IInteractable>();

    //        if (interactObj != null)
    //        {
    //            interactObj.InitInteract(me.name)
    //                ;
    //            Debug.Log($"{gameObject.name} interacted with {interactObj.GiveObject().name} at position {hitInfo.point}");
    //        }
    //        else
    //        {
    //            Debug.Log("No interactable object found!");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("SphereCast did not hit any objects.");
    //    }
    //}

}


public class CubeLifetime : MonoBehaviour
{
    private float lifetime;
    private List<GameObject> collidedObjects = new List<GameObject>();

    // Initialize the lifetime of the cube
    public void Initialize(float lifetime)
    {
        this.lifetime = lifetime;
        StartCoroutine(DestroyAfterLifetime());
    }

    // Coroutine to destroy the cube after its lifetime expires
    private IEnumerator<WaitForSeconds> DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        LogCollisions();
        Destroy(gameObject);
    }

    // OnCollisionEnter is called when the cube collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // Add the collided object to the list
        collidedObjects.Add(collision.gameObject);
    }

    // Log the collisions when the cube is destroyed
    private void LogCollisions()
    {
        Debug.Log("The cube collided with the following objects:");
        foreach (GameObject obj in collidedObjects)
        {
            Debug.Log(obj.name);
        }
    }
}
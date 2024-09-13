using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using System;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
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

    public Multiplayer networkManager;
    PlayerStates playerStates;

    [SerializeField] private GameObject me;
    private float sphereRadius = 0.5f; // Adjust this value as needed
    void Start()
    {
        networkManager = FindAnyObjectByType<Multiplayer>();

        _avatar = gameObject.GetComponentInParent<Alteruna.Avatar>();
        me = gameObject;
    }

    public void SetAttack()
    {
        InvokeRemoteMethod("AttackRemote", UserId.All);
    }

    public UnityEngine.SceneManagement.Scene currentscene;

    [SynchronizableMethod]
    public void AttackRemote()
    {
        //if (currentscene.name == "Start" || currentscene.name == "Lobby") 
        //{ 
        //    return;
        //}
            Attack();
        

    }

    // Update is called once per frame
    void Update()
    {

        currentscene = SceneManager.GetActiveScene();
        Debug.Log(currentscene.name);
        networkManager = FindAnyObjectByType<Multiplayer>();


        playerStates = networkManager.GetComponent<PlayerStates>();

        Debug.DrawRay(InteractorCam.transform.position, InteractorCam.transform.forward * InteractRange, Color.magenta);


    }

    public GameObject taggingCube;
    public List<GameObject> taggedPlayers;
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

        // Start a coroutine to wait for 300 milliseconds and then check the list
        StartCoroutine(CheckCollisionsAfterDelay(spawnedCube, 300));
    }

    private IEnumerator CheckCollisionsAfterDelay(GameObject cube, float milliseconds)
    {
        // Convert milliseconds to seconds
        float seconds = milliseconds / 1000f;

        Debug.Log("Timer start");

        // Wait for the specified time
        yield return new WaitForSeconds(seconds);

        Debug.Log("Timer end");

        // Get the TaggingBoxCollisionHandler component from the spawned cube
        TaggingBoxCollisionHandler collisionHandler = cube.GetComponent<TaggingBoxCollisionHandler>();

        if (collisionHandler != null)
        {
            // Check the list of colliding objects after the delay
            foreach (GameObject obj in collisionHandler.objectList)
            {
                taggedPlayers.Add(obj);
            }
        }
        else
        {
            Debug.LogError("TaggingBoxCollisionHandler component not found on the spawned cube.");
        }
                Destroy(cube);
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


        //Ray ray = new Ray(InteractorCam.transform.position, InteractorCam.transform.forward);

        SpawnCube();


        StartCoroutine(ProcessInteractionsWithDelay());
    }

    private IEnumerator ProcessInteractionsWithDelay()
    {
        // Wait for 300 milliseconds
        yield return new WaitForSeconds(300/1000f);

        foreach (GameObject player in taggedPlayers)
        {
            Transform prey = player.transform.Find("PreyComponent");

            IInteractable interactObj = prey.gameObject.GetComponentInParent<IInteractable>();

            if (interactObj != null)
            {
                interactObj.InitInteract(gameObject.transform.root.name);
                Debug.Log($"{gameObject.transform.root.name} tagged {interactObj.GiveObject().name}");
            }
            else
            {
                Debug.Log("No interactable object found!");
            }
        }

        taggedPlayers.Clear();
    }


    //}
    //else
    //{
    //    Debug.Log("SphereCast did not hit any objects.");
    //}



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
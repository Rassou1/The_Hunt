using Alteruna;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractablePlayer : AttributesSync, IInteractable
{
    public bool tagged = false; // Boolean to track if the prey is tagged

    public Vector3 prisonPosition = new Vector3(-43.941452f, 8.03499985f, -49.7112961f);
    public Alteruna.Avatar _avatar;
    //public PlayerManagerBase _playerManager;

    public H_StateManager _hunterManager;
    public P_StateManager _preyManager;

    private string myName;
    public bool movingmap;

    private MapMover mm;

    GameObject startLever;
    public void Awake()
    {
        // Find the startLever in the Awake method
        startLever = GameObject.Find("Objects/StartLever");
        if (startLever != null)
        {
            mm = startLever.GetComponent<MapMover>();
        }
        else
        {
            Debug.LogError("StartLever not found in Objects");
        }
    }
    public void Start()
    {
        myName = gameObject.name;
    }

    private void Update()
    {
        if (movingmap)
        {
            MoveMap();
            movingmap = false;
        }
    }


    public GameObject GiveObject()
    {
        return gameObject;
    }
    public void InitInteract(string interactor)
    {

            BroadcastRemoteMethod("Interact", interactor);
        
    }
    GameObject _interactor;

    [SynchronizableMethod]
    public void Interact(string interactor)
    {
        _interactor = FindAnyObjectByType<GameObject>();

       GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player.name == interactor)
            {
                _interactor = player;
            }
        }
        // Check if both the interactor and the player are on specific layers
        if (_interactor.layer == LayerMask.NameToLayer("Hunter") && gameObject.layer == LayerMask.NameToLayer("Prey"))
        {
            // Teleport the player to the prison position
            GetComponent<TransformSynchronizable>().transform.position = prisonPosition;
            _preyManager.Caught = true;

        }
        Debug.Log("");
    }

    public void SendToPrison()
    {
            // Teleport the player to the prison position
            gameObject.transform.position = new Vector3(-43.941452f, 8.03499985f, -49.7112961f);
           

        
    }

    [SynchronizableMethod]
    public void MoveMap()
    {
        mm.moveMaps(gameObject);
    }
}
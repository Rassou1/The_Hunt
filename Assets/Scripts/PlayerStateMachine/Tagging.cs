using Alteruna;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractablePlayer : AttributesSync, IInteractable
{
    public bool tagged = false; // Boolean to track if the prey is tagged

    public Vector3 prisonPosition = new Vector3(-43.941452f, 8.03499985f, -49.7112961f);
    public Alteruna.Avatar _avatar;
    Multiplayer networkManager;
    PlayerStates playerStates;
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
            //Debug.LogError("StartLever not found in Objects");
        }
    }
    public void Start()
    {
        myName = gameObject.name;
    }

    private void Update()
    {
        networkManager = FindAnyObjectByType<Multiplayer>();
        playerStates = networkManager.GetComponent<PlayerStates>();
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
    public GameObject _interactor;
    List<GameObject> FindObjectsOnLayer(int layer)
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> objectsInLayer = new List<GameObject>();

        foreach (var obj in allPlayers)
        {
            if (obj.layer == layer)
            {
                objectsInLayer.Add(obj);
            }
        }

        return objectsInLayer;
    }
    [SynchronizableMethod]
    public void Interact(string interactor)
    {
        //_interactor = FindAnyObjectByType<GameObject>();

        List<GameObject> players = new List<GameObject>();

        List<GameObject> parents = FindObjectsOnLayer(9);

        foreach (GameObject player in parents)
        {
            Transform parentTransform = player.transform;

            Transform firstChild = parentTransform.Find("PreyComponent");
            Transform secondChild = parentTransform.Find("HunterComponent");

            if (secondChild.gameObject.active)
            {
                players.Add(secondChild.gameObject);
            }

            if (firstChild.gameObject.active)
            {
                players.Add(firstChild.gameObject);
            }
        }



        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponentInParent<Alteruna.Avatar>().name == interactor)
            {
                _interactor = players[i];
            }
        }


        // Check if both the interactor and the player are on specific layers
        if (_interactor.layer == LayerMask.NameToLayer("Hunter") && gameObject.layer == LayerMask.NameToLayer("Prey"))
        {
            // Teleport the player to the prison position
            Transform parentTransform = GetComponent<TransformSynchronizable>().transform;
            Transform firstChild = parentTransform.Find("PlayerAndBody");
            firstChild.position = new Vector3(-43.941452f, 8.03499985f, -49.7112961f);
            _preyManager.Caught = true;
            playerStates.playerTagged(gameObject);

        }
        //Debug.Log("");
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
using Alteruna;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractablePlayer : AttributesSync, IInteractable
{
    public bool tagged = false; // Boolean to track if the prey is tagged

    public Alteruna.Avatar _avatar;
    public Multiplayer networkManager;
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
        networkManager = FindAnyObjectByType<Multiplayer>();
        myName = gameObject.name;
    }

    private void Update()
    {
        networkManager = FindAnyObjectByType<Multiplayer>();
        

        playerStates = networkManager.GetComponent<PlayerStates>();
        //if (movingmap)
        //{
        //    MoveMap();
        //    movingmap = false;
        //}
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
    
    [SynchronizableMethod]
    public void Interact(string interactor)
    {
        //_interactor = FindAnyObjectByType<GameObject>();

        List<GameObject> players = new List<GameObject>();

        List<GameObject> parents = playerStates.FindObjectsOnLayer(9);

        foreach (GameObject player in parents)
        {
            Transform parentTransform = player.transform;

            Transform firstChild = parentTransform.Find("PreyComponent");
            Transform secondChild = parentTransform.Find("HunterComponent");

            if (secondChild.gameObject.activeSelf)
            {
                players.Add(secondChild.gameObject);
            }

            if (firstChild.gameObject.activeSelf)
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
            firstChild.position = new Vector3(29, -104, 47.5f);
            _preyManager.Caught = true;
            playerStates.playerTagged(gameObject);

        }
        //Debug.Log("");
    }

    public void SendToPrison()
    {
            // Teleport the player to the prison position
            gameObject.transform.position = new Vector3(29, -104, 47.5f);

    }

    [SynchronizableMethod]
    public void MoveMap()
    {
        //mm.MoveMaps(gameObject);
    }
}
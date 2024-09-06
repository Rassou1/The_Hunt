using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR;
using UnityEditor;

public class RoleGiver : AttributesSync, IInteractable
{
    public GameObject hunterCanvas;
    public GameObject preyCanvas;

    public GameObject newPrefab;

    public PlayerStates playerStates;
    public Multiplayer networkManager;

    public MapMover mm;

    public Spawner spawner;

    int hunterIndex = 0;

    public GameObject GiveObject()
    {
        return gameObject;
    }

    public void InitInteract(string interactor)
    {
        //Calls interact method, resets all player values. Ensures game restarts don't end due to leftover variables from last game.
        playerStates.StateReset();
        BroadcastRemoteMethod("Interact", interactor);
    }

    [SynchronizableMethod]
    public void Interact(string interactor)
    {
        //On interact with the GameObject that houses this script: - Ibrahim
        if (playerStates.Players.Count > 0)
        {
            
            foreach (GameObject p in playerStates.Players)
            {
                //The players and all networking components move scenes. - Ibrahim
                mm.MoveMaps(p);
            }

            for (int i = 0; i < playerStates.Players.Count; i++)
            {
                Alteruna.Avatar avatar = playerStates.Players[i].GetComponent<Alteruna.Avatar>();
                //The host gets turned into a hunter via the SwitchPrefab script. - Ibrahim
                if (i == hunterIndex)
                {
                    if (!avatar.IsMe)
                        return;
                    SwitchPrefab(hunterIndex);
                    //Hunter UI is turned on. - Ibrahim
                    hunterCanvas.SetActive(true);
                }
                else
                {
                    if (!avatar.IsMe)
                        return;
                    //For all other players, the prey UI is turned on instead. - Ibrahim
                    preyCanvas.SetActive(true);
                }
            }
        }
    }



    [SynchronizableMethod]

    public void SwitchPrefab(int i)
    {
        //Disables the prey and enables the hunter components of the player. - Ibrahim
        Transform parentTransform = playerStates.Players[i].transform;

        // Find the child GameObjects by name - Ibrahim
        Transform firstChild = parentTransform.Find("PreyComponent");
        Transform secondChild = parentTransform.Find("HunterComponent");


        // Transfer the position from the first child to the second child - Ibrahim

        secondChild.position = Vector3.zero;
        secondChild.rotation = firstChild.rotation;

        //Enable the hunter, then disable the prey component. - Ibrahim
        if (firstChild != null && secondChild != null)
        {
            secondChild.gameObject.SetActive(true);
            firstChild.gameObject.SetActive(false);
        }
    }

    

    public void Tag(GameObject tagger, GameObject tagged)
    {
        //If a player is tagged, they are sent to the jail's co-ordinates. - Ibrahim
        tagged.transform.position = new Vector3(63.7f, 10.58f, -17.28f);
    }

    void Start()
    {
        //Both UI canvases are turned off at the beginning to avoid overlap between the two. - Ibrahim
        hunterCanvas.SetActive(false);
        preyCanvas.SetActive(false);
    }

    void Update()
    {   
        networkManager = FindAnyObjectByType<Multiplayer>();
        playerStates = networkManager.GetComponent<PlayerStates>();
    }


    public void ResetAllPrefabs()
    {
        //MOVE THIS TO PLAYERSTATES - Ibrahim
        List<GameObject> _players = playerStates.FindObjectsOnLayer(9);

        foreach (var obj in _players)
        {
            Debug.Log("reseting"+obj.name);
            Transform parentTransform = obj.transform;

            Transform firstChild = parentTransform.Find("PreyComponent");
            Transform secondChild = parentTransform.Find("HunterComponent");

            // Find the child GameObjects' positions by name
            Transform firstChildPosition = parentTransform.Find("PreyComponent").Find("PlayerAndBody");
            Transform secondChildPosition = parentTransform.Find("HunterComponent").Find("PlayerAndBody");

            firstChildPosition.position = Vector3.zero;
            secondChildPosition.position = Vector3.zero;

            // Transfer the position from the first child to the second child
            secondChildPosition.position = firstChildPosition.position;
            secondChildPosition.rotation = firstChildPosition.rotation;
            
            if (!firstChild.gameObject.activeSelf)
            {

                // Turn the first GameObject on
                firstChild.gameObject.SetActive(true);

                // Turn the second GameObject off
                secondChild.gameObject.SetActive(false);
            }

        }
    }

}
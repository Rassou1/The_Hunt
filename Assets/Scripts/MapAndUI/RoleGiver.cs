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
    // Many memebers worked on this script file
    // Thitiwich implented the code to show the hunter/prey canvas but has been rewritten by another member in the group
    public GameObject hunterCanvas;
    public GameObject preyCanvas;

    public GameObject newPrefab;

    public PlayerStates playerStates;
    public Multiplayer networkManager;

    public MapMover mm;

    public Spawner spawner;
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
        //Resets all players to prey. Moves all players to game map. Changes the host to a hunter. - Ibrahim
        if (playerStates.Players.Count > 0)
        {
            ResetAllPrefabs();

            //int hunterIndex = Random.Range(0, players.Count - 1);
            //Legacy code. Doesn't work due to networking errors. - Ibrahim

            int hunterIndex = 0;

            foreach (GameObject p in playerStates.Players)
            {
                //p.GetComponent<InteractablePlayer>().movingmap = true; Don't do this. Don't try to do my work for me please. - Ibrahim
                mm.MoveMaps(p);
            }


            for (int i = 0; i < playerStates.Players.Count; i++)
            {
                Alteruna.Avatar avatar = playerStates.Players[i].GetComponent<Alteruna.Avatar>();

                if (i == hunterIndex)
                {
                    //players[i].layer = LayerMask.NameToLayer("Hunter"); //Don't do this. The layer of the hunterComponent is already on Hunter layer. -Ibrahim

                    if (!avatar.IsMe)
                        return;
                    SwitchPrefab(hunterIndex);
                    hunterCanvas.SetActive(true);
                }
                else
                {

                    //players[i].layer = LayerMask.NameToLayer("Prey"); //Same deal as the hunter. -Ibrahim

                    if (!avatar.IsMe)
                        return;
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

            Transform parentTransform = obj.transform;

            // Find the child GameObjects by name
            Transform firstChild = parentTransform.Find("PreyComponent").Find("PlayerAndBody");
            Transform secondChild = parentTransform.Find("HunterComponent").Find("PlayerAndBody");

            firstChild.position = Vector3.zero;
            secondChild.position = Vector3.zero;

            // Transfer the position from the first child to the second child
            secondChild.position = firstChild.position;
            secondChild.rotation = firstChild.rotation;
            
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
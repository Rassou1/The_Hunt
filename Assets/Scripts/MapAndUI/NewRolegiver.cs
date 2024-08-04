using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR;
using UnityEditor;

public class NewRoleGiver : AttributesSync, IInteractable
{
    // Many memebers worked on this script file
    // Thitiwich implented the code to show the hunter/prey canvas but has been rewritten by another member in the group
    public GameObject hunterCanvas;
    public GameObject preyCanvas;

    public List<GameObject> players;
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
        playerStates.stateReset();
        BroadcastRemoteMethod("Interact", interactor);
        
        

    }

    List<GameObject> FindObjectsOnLayer(int layer)
    {
        //Finds every object on a given layer.
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
        //Resets all players to prey. Moves all players to game map.Changes the host to a hunter.
        if (players.Count > 0)
        {

            resetAllPrefabs();

            //int hunterIndex = Random.Range(0, players.Count - 1);
            //Legacy code. Doesn't work due to networking errors.
            
            int hunterIndex = 0;

            foreach (GameObject p in players)
            {
                p.GetComponent<InteractablePlayer>().movingmap = true;
                mm.moveMaps(p);
                
            }


            for (int i = 0; i < players.Count; i++)
            {

                

                Alteruna.Avatar avatar = players[i].GetComponent<Alteruna.Avatar>();

                if (i == hunterIndex)
                {
                    //players[i].layer = LayerMask.NameToLayer("Hunter"); //Don't do this. The layer of the hunterComponent is already on Hunter layer.

                    if (!avatar.IsMe)
                        return;
                    SwitchPrefab(hunterIndex);
                    hunterCanvas.SetActive(true);
                }
                else
                {

                    //players[i].layer = LayerMask.NameToLayer("Prey");

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
        //Disables the prey and enables the hunter components of the player.
        Transform parentTransform = players[i].transform;

        // Find the child GameObjects by name
        Transform firstChild = parentTransform.Find("PreyComponent");
        Transform secondChild = parentTransform.Find("HunterComponent");


        // Transfer the position from the first child to the second child

        secondChild.position = Vector3.zero;
        secondChild.rotation = firstChild.rotation;


        if (firstChild != null && secondChild != null)
        {

         
            secondChild.gameObject.SetActive(true);
            
            Debug.Log(firstChild.position.ToString() + secondChild.position.ToString());
            
         
            firstChild.gameObject.SetActive(false);




        }
        else
        {
            Debug.LogWarning("One or both child GameObjects not found.");
           
        }

    }

    

    public void Tag(GameObject tagger, GameObject tagged)
    {
        tagged.transform.position = new Vector3(63.7f, 10.58f, -17.28f);

    }

    void Start()
    {
        hunterCanvas.SetActive(false);
        preyCanvas.SetActive(false);
        
    }

    void Update()
    {
        players = FindObjectsOnLayer(9);
       
        
        networkManager = FindAnyObjectByType<Multiplayer>();
        playerStates = networkManager.GetComponent<PlayerStates>();
    }

    public void resetAllPrefabs()
    {
        List<GameObject> _players = FindObjectsOnLayer(9);

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
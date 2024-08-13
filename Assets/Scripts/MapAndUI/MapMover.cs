using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
    using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMover : AttributesSync
{
    Multiplayer networkManager;
    PlayerStates playerStates;

    public GameObject GiveObject()
    {
        return gameObject;
    }

    [SynchronizableMethod]
    public void MoveMaps(GameObject player)
    {
        //Finds the networkmanager and the playerstates global values. Moves players differently based on which map they are on. The gameStarted and gameEnded bools were used
        //to ensure correct placement of players. - Ibrahim
        networkManager = FindAnyObjectByType<Multiplayer>();
        playerStates = networkManager.GetComponent<PlayerStates>();
        Scene scene = SceneManager.GetActiveScene();
        

        if ((scene.name == "Start" || scene.name == "Lobby") && !playerStates.gameStarted)
        {
            foreach (GameObject p in playerStates.Players)
            {
                //Sends each player to a predetermined spawn in the final map. - Ibrahim
                Transform parentTransform = p.transform;

                //Finds the PlayerAndBody component which controls position on both the prey and hunter. - Ibrahim
                Transform firstChild = parentTransform.Find("PreyComponent").Find("PlayerAndBody");
                Transform secondChild = parentTransform.Find("HunterComponent").Find("PlayerAndBody");

                //Centers all 3 objects onto one position to unify movement and avoid desync/animation errors. - Ibrahim
                parentTransform.position = Vector3.zero;
                secondChild.position = Vector3.zero;
                firstChild.position = Vector3.zero;

                //Picks a random spawn for the prey. Initially tried to make these values global through PlayerStates, however that caused sync errors due to the nature of PlayerStates. 
                //It's more efficient to put them here. - Ibrahim
                int spawnLocation = Random.Range(0, 80000);
                
                if (firstChild.gameObject.activeSelf)
                {
                    if (spawnLocation <= 20000)
                    {
                        firstChild.position = new Vector3(5, 1, 28);
                    }
                    else if (spawnLocation <= 40000 && spawnLocation > 20000)
                    {
                        firstChild.position = new Vector3(-11, 1, 27);
                    }
                    else if (spawnLocation <= 60000 && spawnLocation > 40000)
                    {
                        firstChild.position = new Vector3(-14.5f, 1, 4);
                    }
                    else if (spawnLocation <= 80000 && spawnLocation > 60000)
                    {
                        firstChild.position = new Vector3(9, 1, 6);
                    }
                }
            }
            
            //Loads the game scene, and sets the game started boolean to true. - Ibrahim
            networkManager.LoadScene("Game_Map");
            playerStates.gameStarted = true;
        }
        else if (scene.name == "Game_Map" && playerStates.gameStarted && playerStates.gameEnded)
        {
            //Moves players from game map to lobby. Sends them to their place based on their status (tagged, escaped, hunter).
            //NOT WORKING DUE TO PLAYERANDBODY DISCREPANCY. MAKE SURE YOU CHANGE THE RIGHT POSITIONS, SAME WAY AS IN ROLEGIVER AND INITIAL MAPMOVER

            //UPDATE NOTE: Changed the structure of the code to accomodate for playerandbody, basically just stole the code from the initial map movement (which is tested and working).
            //this should theoretically work. literally no guarantee of it since I can't test on my laptop /ibrahim

            //TEST NOTE: It doesnt work not sure of the reason yet, but it seems like firstChild isnt innitialized in some way /Tyron 

            //I have no clue why it isn't working at this point. It's the exact same code, but only works half the time?? I'll refactor it and we'll see. - Ibrahim

            //IT IS ALIVE- Ibrahim


            foreach (GameObject currentplayer in playerStates.Players)
            {
                Transform parentTransform = currentplayer.transform;
                
                Transform firstChild = parentTransform.Find("PreyComponent").Find("PlayerAndBody");
                
                Transform secondChild = parentTransform.Find("HunterComponent").Find("PlayerAndBody");
                
                if (firstChild.GetComponentInChildren<P_StateManager>().Escaped == true)
                {
                    firstChild.position = new Vector3(64.5f, 16.44f, 100);
                }
                else if (firstChild.GetComponentInChildren<P_StateManager>().Escaped == false)
                {
                    firstChild.position = new Vector3(107f, 0.8f, 95);
                }
                secondChild.position = new Vector3(84f, 16.44f, 128);
                
            }
            
            foreach (GameObject hunter in playerStates.Hunters)
            {
                hunter.GetComponentInParent<Transform>();
            }
            
            networkManager = FindAnyObjectByType<Multiplayer>();
            networkManager.LoadScene("Lobby");
            foreach (GameObject prey in playerStates.Prey)
            {
                prey.GetComponentInChildren<P_StateManager>(true).Ghost = false;
                //makes u visible
                prey.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                prey.GetComponent<CapsuleCollider>().enabled = true;
            }
            playerStates.gameStarted = false;
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        networkManager = FindAnyObjectByType<Multiplayer>();
        playerStates = networkManager.GetComponent<PlayerStates>();
    }
}

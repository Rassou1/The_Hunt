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
        //Updated to accomodate for the Alteruna update, now that everything's in the same scene. No longer moves scenes or forces sync; said code can be found in the github. - Ibrahim, Hamdi
        networkManager = FindAnyObjectByType<Multiplayer>();
        playerStates = networkManager.GetComponent<PlayerStates>();
        
        //Scene scene = SceneManager.GetActiveScene();
        

        if (!playerStates.gameStarted && !playerStates.gameEnded)
        {
            foreach (GameObject p in playerStates.Players)
            {
                //Sends each player to a predetermined spawn in the final map. - Ibrahim
                Transform parentTransform = p.transform;

                //Finds the PlayerAndBody component which controls position on both the prey and hunter. - Ibrahim
                Transform preyComponent = parentTransform.Find("PreyComponent").Find("PlayerAndBody");
                Transform hunterComponent = parentTransform.Find("HunterComponent").Find("PlayerAndBody");

                //Centers all 3 objects' local positions to unify movement and avoid desync/animation errors. - Ibrahim
                parentTransform.position = Vector3.zero;
                hunterComponent.position = Vector3.zero;
                preyComponent.position = Vector3.zero;

                //Picks a random spawn for the prey. Initially tried to make these values global through PlayerStates, however that caused sync errors. 
                //It's more efficient to put them here. - Ibrahim
                int spawnLocation = Random.Range(0, 80000);
                hunterComponent.position = new Vector3(73, -111f, 102);


                if (preyComponent.gameObject.activeSelf)
                {
                    if (spawnLocation <= 20000)
                    {
                        preyComponent.position = new Vector3(86, -111, 113);
                    }
                    else if (spawnLocation <= 40000 && spawnLocation > 20000)
                    {
                        preyComponent.position = new Vector3(80, -111, 123);
                    }
                    else if (spawnLocation <= 60000 && spawnLocation > 40000)
                    {
                        preyComponent.position = new Vector3(57, -111, 116);
                    }
                    else if (spawnLocation <= 80000 && spawnLocation > 60000)
                    {
                        preyComponent.position = new Vector3(57, -111, 100);
                    }
                }
            }

            //Loads the game scene, and sets the game started boolean to true. Deprected now.- Ibrahim
            //networkManager.LoadScene("Game_Map");
            
            playerStates.gameStarted = true;
        }
        else if (playerStates.gameStarted && playerStates.gameEnded)
        {
            //Moves players from game map to lobby. Sends them to their place based on their status (tagged, escaped, hunter).

            //UPDATE NOTE: Changed the structure of the code to accomodate for playerandbody, basically just stole the code from the initial map movement (which is tested and working).
            //this should theoretically work. /ibrahim

            //Changed once again to avoid desync. - Ibrahim

            foreach (GameObject currentplayer in playerStates.Players)
            {
                Transform parentTransform = currentplayer.transform;
                
                Transform preyComponent = parentTransform.Find("PreyComponent").Find("PlayerAndBody");
                
                Transform hunterComponent = parentTransform.Find("HunterComponent").Find("PlayerAndBody");
                
                if (preyComponent.GetComponentInChildren<P_StateManager>().Escaped == true)
                {
                    preyComponent.position = new Vector3(67.7900009f, 19.8600006f, 99.1600037f);
                }
                else if (preyComponent.GetComponentInChildren<P_StateManager>().Escaped == false)
                {
                    preyComponent.position = new Vector3(106.099998f, 2.05999994f, 99.1600037f);
                }
                hunterComponent.position = new Vector3(84.9000015f, 16.9899998f, 127.059998f);
                
            }
            
            //foreach (GameObject hunter in playerStates.Hunters)
            //{
            //    hunter.GetComponentInParent<Transform>();
            //}
            
            networkManager = FindAnyObjectByType<Multiplayer>();
            //networkManager.LoadScene("Lobby");
            foreach (GameObject prey in playerStates.Prey)
            {
                prey.GetComponentInChildren<P_StateManager>(true).Ghost = false;
                //Makes you visible. Disables ghost mode.
                Debug.Log("ghost off");
                prey.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                prey.GetComponentInChildren<CapsuleCollider>().enabled = true;
            }
            playerStates.gameStarted = false;
            //playerStates.PlayerForceSync();
            //playerStates.PlayerForceSync();
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

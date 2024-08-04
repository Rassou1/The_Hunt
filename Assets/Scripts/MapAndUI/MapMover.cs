using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
    using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMover : AttributesSync
{
    public List<GameObject> players;
    public List<GameObject> preyList;
    public List<GameObject> hunterList;
    public List<GameObject> parents;

    P_StateManager pManager;

    Multiplayer networkManager;
    Transform spawn;
    PlayerStates playerStates;


    public GameObject GiveObject()
    {
        return gameObject;
    }

   

    public List<GameObject> FindObjectsOnLayer(int layer)
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
    public void moveMaps(GameObject player)
    {
        //Finds the networkmanager and the playerstates global values. Moves players differently based on which map they are on. The gameStarted and gameEnded bools were used 
        //to ensure correct placement of players.
        networkManager = FindAnyObjectByType<Multiplayer>();
        spawn = networkManager.GetComponent<Transform>();
        playerStates = networkManager.GetComponent<PlayerStates>();
        Scene scene = SceneManager.GetActiveScene();
        

        if ((scene.name == "LOOBY" || scene.name == "TEMPSTART") && !playerStates.gameStarted)
        {
            Debug.Log("moving from start");

            //Transform transform = player.GetComponent<Transform>();
            //transform.position = new Vector3(0, 3, -10);




            foreach (GameObject p in players)
            {
                //Sends each player to a predetermined spawn in the final map.
                Transform parentTransform = p.transform;
                Transform firstChild = parentTransform.Find("PreyComponent").Find("PlayerAndBody");
                Transform secondChild = parentTransform.Find("HunterComponent").Find("PlayerAndBody");
                parentTransform.position = Vector3.zero;
                secondChild.position = Vector3.zero;
                firstChild.position = Vector3.zero;
                int spawnLocation = Random.Range(0, 80000);
                if (firstChild.gameObject.activeSelf)
                {
                    if (spawnLocation <= 20000)
                    {
                        firstChild.position = new Vector3(5, 1, 28);//playerStates.spawns[0];
                    }
                    else if (spawnLocation <= 40000 && spawnLocation > 20000)
                    {
                        firstChild.position = new Vector3(-11, 1, 27);//playerStates.spawns[1];
                    }
                    else if (spawnLocation <= 60000 && spawnLocation > 40000)
                    {
                        firstChild.position = new Vector3(-14.5f, 1, 4);//playerStates.spawns[2];
                    }
                    else if (spawnLocation <= 80000 && spawnLocation > 60000)
                    {
                        firstChild.position = new Vector3(9, 1, 6); //playerStates.spawns[3];
                    }
                }

            }
            
            networkManager.LoadScene("Final_Map");
            playerStates.gameStarted = true;
            Debug.Log($"Has game started? {playerStates.gameStarted}");
        }
        else if (scene.name == "Final_Map" && playerStates.gameStarted && playerStates.gameEnded)
        {
            //Moves players from game map to lobby. Sends them to their place based on their status (tagged, escaped, hunter).
            //NOT WORKING DUE TO PLAYERANDBODY DISCREPANCY. MAKE SURE YOU CHANGE THE RIGHT POSITIONS, SAME WAY AS IN NEWROLEGIVER AND INITIAL MAPMOVER

            //UPDATE NOTE: Changed the structure of the code to accomodate for playerandbody, basically just stole the code from the initial map movement (which is tested and working).
            //this should theoretically work. literally no guarantee of it since I can't test on my laptop /ibrahim

            //TEST NOTE: It doesnt work not sure of the reason yet, but it seems like firstChild isnt innitialized in some way /Tyron 

            parents = FindObjectsOnLayer(9);
            foreach (GameObject currentplayer in parents)
            {
                Transform parentTransform = currentplayer.transform;
                Transform firstChild = parentTransform.Find("PreyComponent").Find("PlayerAndBody");
                Transform secondChild = parentTransform.Find("HunterComponent").Find("PlayerAndBody");
                if (firstChild.GetComponentInChildren<P_StateManager>().Escaped == true)
                {
                    /*prey.GetComponentInParent<Transform>()*/firstChild.position = new Vector3(64.5f, 16.44f, 100);
                }
                else if (firstChild.GetComponentInChildren<P_StateManager>().Escaped == false)
                {
                    /*prey.GetComponentInParent<Transform>()*/firstChild.position = new Vector3(107f, 0.8f, 95);
                }
                secondChild.position = new Vector3(84f, 16.44f, 128);
            }
            hunterList = FindObjectsOnLayer(6);
            foreach (GameObject hunter in hunterList)
            {
                /*hunter.GetComponentInParent<Transform>()*/
            }
            networkManager = FindAnyObjectByType<Multiplayer>();
            networkManager.LoadScene("LOOBY");
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
        players = FindObjectsOnLayer(9);
        //Debug.Log(players.Count);
        networkManager = FindAnyObjectByType<Multiplayer>();
        spawn = networkManager.GetComponent<Transform>();
        playerStates = networkManager.GetComponent<PlayerStates>();
        //Debug.Log(playerStates);
    }
}

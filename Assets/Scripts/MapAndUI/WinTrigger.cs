using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    public GameObject youWinText;
    public float delay;
    GameObject player;
    private P_StateManager stateManager;
    public PlayerStates playerStates;
    Multiplayer mp;
    MapMover mapMover;

    

    void Start()
    {
        //On game start, finds the multiplayer component. Makes list of prey and hunter. 
        mp = FindAnyObjectByType<Multiplayer>();
        playerStates = mp.GetComponent<PlayerStates>();

        mapMover = new MapMover();

        playerStates.escapedPlayers.Clear();
        playerStates.taggedPlayers.Clear();
    }

    // Update is called once per frame
    public void Update()
    { 
        if (playerStates.taggedPlayers.Count == playerStates.Prey.Count && playerStates.taggedPlayers.Count > 0)
        {
            playerStates.allPlayersTagged = true;
        }
        if (playerStates.allPlayersTagged)
        {
            playerStates.gameEnded = true;
            foreach (GameObject p in playerStates.Players)
            {
                mapMover.MoveMaps(p);
                
            }
        }
    }

    

    [ExecuteAlways]
    void OnTriggerEnter(Collider other)
    {
        //On entering the escape door, the player turns into ghost, is logged as having escaped, and upon all players escaping, sends everyone back to lobby. - Ibrahim
        mp = FindObjectOfType<Multiplayer>();
        GameObject playerNewPrefab = other.gameObject.transform.parent.parent.parent.gameObject;
        
        if (playerNewPrefab.tag == "Player")
        {
            stateManager = playerNewPrefab.GetComponentInChildren<P_StateManager>(true);
            player = playerNewPrefab;
            if (other.gameObject.layer == 7)
            {
                stateManager.Escaped = true;
                playerStates.playerEscaped(player);
                other.gameObject.GetComponent<P_StateManager>().Ghost = true;
                //makes u invisible
                other.transform.parent.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }

            if (playerStates.escapedPlayers.Count + playerStates.taggedPlayers.Count == playerStates.FindObjectsOnLayer(7).Count && playerStates.escapedPlayers.Count > 0)
            {
                playerStates.gameEnded = true;
               
                foreach (GameObject obj in playerStates.Players)
                {
                    //obj.GetComponent<InteractablePlayer>().movingmap = true;
                    mapMover.MoveMaps(obj);
                }
            }           
        }
    }


    //old code no longer used. - Ibrahim
    //public IEnumerator CountDown()
    //{
    //    //yield return new WaitForSeconds(delay);
    //    yield return new WaitForSeconds(0);
    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

}








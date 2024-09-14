using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    //public GameObject youWinText;
    //public float delay;
    GameObject player;
    private P_StateManager stateManager;
    public PlayerStates playerStates;
    Multiplayer mp;
    public MapMover mapMover;

    

    void Start()
    {
        //On game start, finds the multiplayer component. Makes list of prey and hunter. 
        mp = FindAnyObjectByType<Multiplayer>();
        playerStates = mp.GetComponent<PlayerStates>();

        //mapMover = new MapMover(); deprecated code, now references an existing mapmover object in the map - Ibrahim

        playerStates.escapedPlayers.Clear();
        playerStates.taggedPlayers.Clear();
    }

    // Update is called once per frame
    public void Update()
    { 
        if (playerStates.taggedPlayers.Count == (playerStates.Players.Count -1) && playerStates.taggedPlayers.Count > 0)
            //I would much rather we compare the tagged player count to the prey count, but due to the hunter being turned back into prey on return to lobby we can't do that without using a temporary value.
            //This is far more readable. - Ibrahim
        {
            playerStates.allPlayersTagged = true;
        }
        if (playerStates.allPlayersTagged)
        {
            playerStates.gameEnded = true;
            mapMover.MoveMaps();
        }

        foreach(GameObject p in playerStates.Players)
        {
            P_StateManager state = p.GetComponent<P_StateManager>();
            if (state.Escaped)
            {
                playerStates.playerEscaped(p);
            }
            if(state.Caught)
            {
                playerStates.playerTagged(p);
            }
        }
    }

    

    [ExecuteAlways]
    void OnTriggerEnter(Collider other)
    {
        //On entering the escape door, the player turns into ghost, is logged as having escaped, and upon all players escaping, sends everyone back to lobby. - Ibrahim
        mp = FindObjectOfType<Multiplayer>();
        GameObject playerNewPrefab = other.gameObject.transform.parent.parent.parent.gameObject;
        Debug.Log("Player Escaped");
        if (playerNewPrefab.tag == "Player")
        {
            stateManager = playerNewPrefab.GetComponentInChildren<P_StateManager>(true);
            player = playerNewPrefab;
            if (other.gameObject.layer == 7) 
            {
                stateManager.Escaped = true;
                playerStates.playerEscaped(player);
                other.gameObject.GetComponent<P_StateManager>().Ghost = true;
                //Makes you invisible. Enables ghost mode.
                other.transform.parent.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
                other.gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
            }

            if (playerStates.escapedPlayers.Count + playerStates.taggedPlayers.Count == (playerStates.Players.Count - 1) && playerStates.escapedPlayers.Count > 0)
            {
                playerStates.lastPlayerIndex = playerStates.Players.IndexOf(player);
                playerStates.gameEnded = true;
                mapMover.MoveMaps();
                playerStates.PlayerForceSync();
                
            }           
        }
    }

}








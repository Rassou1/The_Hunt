using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public List<GameObject> escapedPlayers = new List<GameObject>();
    public List<GameObject> taggedPlayers = new List<GameObject>();
    public List<Vector3> spawns = new List<Vector3>()
    {
        new Vector3(0,1,18),
        new Vector3(-8,1.3f, 2),
        new Vector3(6,1, 29),
        new Vector3(16,1, 26) //outside map bounds
        //DOUBLE CHECK THAT SPAWN LOCATIONS WONT CLIP YOU INTO FLOOR
    };

    public List<GameObject> players;

    public bool gameStarted;
    public bool gameEnded;
    public bool allPlayersTagged;

    public void stateReset()
    {
        escapedPlayers.Clear();
        taggedPlayers.Clear();
        allPlayersTagged = false;
        gameStarted = false;
        gameEnded = false;
    }
    

    public void playerEscaped(GameObject player)
    {
        escapedPlayers.Add(player);
    }

    public void playerTagged(GameObject player)
    {
        taggedPlayers.Add(player);
    }

    public void Update()
    {
        
    }

}

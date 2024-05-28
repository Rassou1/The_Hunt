using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public List<GameObject> escapedPlayers = new List<GameObject>();
    public List<GameObject> taggedPlayers = new List<GameObject>();
    public List<Vector3> spawns = new List<Vector3>()
    {
        new Vector3(0,0,18),
        new Vector3(-8,1.3f, 2),
        new Vector3(6,0, 29),
        new Vector3(16,0, 26) //outside map bounds
        //DOUBLE CHECK THAT SPAWN LOCATIONS WONT CLIP YOU INTO FLOOR
    };

    public bool gameStarted;
    public bool gameEnded;

    public void Reset()
    {
        escapedPlayers.Clear();
        taggedPlayers.Clear();
    }
    

    public void playerEscaped(GameObject player)
    {
        escapedPlayers.Add(player);
    }

    public void playerTagged(GameObject player)
    {
        taggedPlayers.Add(player);
    }

}

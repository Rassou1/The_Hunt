using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public List<GameObject> escapedPlayers = new List<GameObject>();
    public List<GameObject> taggedPlayers = new List<GameObject>();

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class PlayerStates : MonoBehaviour
{
    //Global values. Gather number of players based on their states. Holds info for spawn locations. Resets all states on new game start.
    public List<GameObject> escapedPlayers = new List<GameObject>();
    public List<GameObject> taggedPlayers = new List<GameObject>();

    //Legacy code for random  hunter spawns. Removed due to the game becoming unfair.
    //public List<Vector3> spawns = new List<Vector3>()
    //{
    //    new Vector3(5,1,28),
    //    new Vector3(-11,1, 27),
    //    new Vector3(-14.5f,1, 4),
    //    new Vector3(9,1, 6) 
    //};

    //Lists for all the types of players. Used repetitively through the stages of gameplay.
    private List<GameObject> players;
    private List<GameObject> prey;
    private List<GameObject> hunters;

    public RoleGiver roleGiver;
    public EndGameController endGameController;

    public bool gameStarted;
    public bool gameEnded;
    public bool allPlayersTagged;
    bool hasReset = false;
    public void StateReset()
    {
        foreach (GameObject prey in prey)
        {
            P_StateManager state = prey.GetComponentInChildren<P_StateManager>();
            Debug.Log(state);
            Debug.Log(state.Escaped);
            state.Escaped = false;
            state.Caught = false;
        }
        escapedPlayers.Clear();
        taggedPlayers.Clear();        
        roleGiver.ResetAllPrefabs();    

        allPlayersTagged = false;
        gameStarted = false;
        gameEnded = false;
        hasReset = true;
    }

    public void playerEscaped(GameObject player)
    {
        if (!escapedPlayers.Contains(player))
        {
            escapedPlayers.Add(player);
        }
        
    }

    public void playerTagged(GameObject player)
    {
        if(!taggedPlayers.Contains(player)) 
        { 
            taggedPlayers.Add(player); 
        }
    }



    public void Update()
    {
        players = FindObjectsOnLayer(9);
        prey = FindObjectsOnLayer(7);
        hunters = FindObjectsOnLayer(6);
        roleGiver = FindObjectOfType<RoleGiver>();
       
        if (gameStarted && hasReset)
        {
            hasReset = false;

        }
        if (gameEnded && !hasReset)
        {
            if (roleGiver == null)
            {
                Debug.Log("rogiver  null");
            }
            //StateReset();
        }

    }

    public List<GameObject> Players { get { return players; } }
    public List<GameObject> Prey { get { return prey; } }
    public List<GameObject> Hunters { get { return hunters; } }

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
}

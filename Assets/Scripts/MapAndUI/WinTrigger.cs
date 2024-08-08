using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    //not yet synchronized.
    public GameObject youWinText;
    public float delay;
    GameObject player;
    private P_StateManager stateManager;
    public PlayerStates playerStates;
    //public RoleGiver roleGiver;
    Multiplayer mp/* = new Multiplayer()*/; //Commented out the new here since it gets instanciated on start, change back if it broke anything - Love
    MapMover mapMover;

    public List<GameObject> players;
    public List<GameObject> preyList;
    public List<GameObject> hunterList;

    //Commented these out since they were never used and just gave a yellow error in the editor which was annoying me, change back if it broke anything - Love
    //string hoboInteractor = ("lol");
    //string sceneName = "TEMPLOBBY";
    void Start()
    {
        //On game start, finds the multiplayer component. Makes list of prey and hunter. 
        mp = FindAnyObjectByType<Multiplayer>();//new PlayerStates();
        playerStates = mp.GetComponent<PlayerStates>();

        //Commented out this as well since it gave a "can't use new keyword with monobehavior" error in the editor, change back if it broke anything - Love
        mapMover = new MapMover();

        //youWinText.SetActive(false);

        playerStates.escapedPlayers.Clear();
        playerStates.taggedPlayers.Clear();
    }

    // Update is called once per frame
    public void Update()
    {

        //Finds all players. Ends game when all players are tagged. Moves back to lobby.
        players = FindObjectsOnLayer(9);
        preyList = FindObjectsOnLayer(7);
        hunterList = FindObjectsOnLayer(6);

        if (playerStates.taggedPlayers.Count == players.Count - 1 && playerStates.taggedPlayers.Count > 0)
        {
            playerStates.allPlayersTagged = true;
        }
        if (playerStates.allPlayersTagged)
        {
            playerStates.gameEnded = true;
            foreach (GameObject p in players)
            {
                Debug.Log("loseMove");
                p.GetComponent<InteractablePlayer>().movingmap = true;
                mapMover.moveMaps(p);
                
            }
        }

        
    }


    public IEnumerator CountDown()
    {
        //yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public IEnumerator LoadScene(GameObject player)
    //{
    //    Scene currentScene = SceneManager.GetActiveScene();
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WinScene");
    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }
    //    SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("WinScene"));
    //    SceneManager.UnloadSceneAsync(currentScene);
    //}

    [ExecuteAlways]
    void OnTriggerEnter(Collider other)
    {
        //On entering the escape door, the player turns into ghost, is logged as having escaped, and upon all players escaping, sends everyone back to lobby.
        mp = FindObjectOfType<Multiplayer>();
        GameObject playerNewPrefab = other.gameObject.transform.parent.parent.parent.gameObject;
        Debug.Log($"{playerNewPrefab.name} has interacted with winTrigger");
        if (playerNewPrefab.tag == "Player")
        {
            //youWinText.SetActive(true);
            //StartCoroutine(CountDown());
            stateManager = playerNewPrefab.GetComponentInChildren<P_StateManager>(true);
            player = playerNewPrefab;
            if (other.gameObject.layer == 7)
            {
                stateManager.Escaped = true;
                playerStates.playerEscaped(player);
                other.gameObject.GetComponent<P_StateManager>().Ghost = true;
                //makes u invisible
                other.gameObject.transform.parent.gameObject.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }

            if (playerStates.escapedPlayers.Count + playerStates.taggedPlayers.Count == FindObjectsOnLayer(7).Count && playerStates.escapedPlayers.Count > 0)
            {
                playerStates.gameEnded = true;

                foreach (GameObject player in players)
                {

                    other.gameObject.GetComponent<P_StateManager>().Ghost = false;
                    //makes u visible
                    other.gameObject.transform.parent.gameObject.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                    other.gameObject.GetComponent<CapsuleCollider>().enabled = true;
                    player.GetComponent<InteractablePlayer>().movingmap = true;
                    mapMover.moveMaps(player);
                }
            }

        }
    }

    List<GameObject> FindObjectsOnLayer(int layer)
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


    

    



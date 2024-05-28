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
    Multiplayer mp = new Multiplayer();
    MapMover mapMover;
    string hoboInteractor = ("lol");

    public List<GameObject> players;
    public List<GameObject> preyList;
    public List<GameObject> hunterList;

    string sceneName = "TEMPLOBBY";
    void Start()
    {
        mp = FindAnyObjectByType<Multiplayer>();//new PlayerStates();
        playerStates = mp.GetComponent<PlayerStates>();
        players = FindObjectsOnLayer(9);
        preyList = FindObjectsOnLayer(7);
        hunterList = FindObjectsOnLayer(6);
        mapMover = new MapMover();
        //youWinText.SetActive(false);
        Debug.Log(playerStates);
        playerStates.escapedPlayers.Clear();
        playerStates.taggedPlayers.Clear();
    }

    // Update is called once per frame
    public void Update()
    {
        if (playerStates.taggedPlayers.Count == players.Count - 1 && playerStates.taggedPlayers.Count > 0)
        {
            playerStates.allPlayersTagged = true;
        }
        if (playerStates.allPlayersTagged)
        {
            playerStates.gameEnded = true;
            foreach (var obj in players)
            {


                obj.GetComponent<InteractablePlayer>().movingmap = true;

                mapMover.moveMaps(obj);
            }
        }
    }


    public IEnumerator CountDown()
    {
        yield return new WaitForSeconds(delay);
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
        mp = FindObjectOfType<Multiplayer>();

        Debug.Log($"{other} has interacted with winTrigger");
        if (other.gameObject.tag == "Player")
        {
            //youWinText.SetActive(true);
            //StartCoroutine(CountDown());
            stateManager = other.gameObject.GetComponentInChildren<P_StateManager>(true);
            player = other.gameObject;
            if (other.gameObject.layer == 7)
            {
                stateManager.Escaped = true;
                playerStates.playerEscaped(player);
            }

            //untested
            if (playerStates.escapedPlayers.Count + playerStates.taggedPlayers.Count == FindObjectsOnLayer(7).Count && playerStates.escapedPlayers.Count > 0)
            {
                playerStates.gameEnded = true;
                Debug.Log($"{playerStates} winTrigger1");
                Debug.Log($"{mp} wT1");
                foreach (var obj in players)
                {
                   
                    Debug.Log($"{playerStates} winTrigger2");
                    Debug.Log($"{mp} wT2");
                    obj.GetComponent<InteractablePlayer>().movingmap = true;
                    Debug.Log($"{playerStates} winTrigger3");
                    Debug.Log($"{mp} wT3");
                    mapMover.moveMaps(obj);
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


    

    



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

    }

    // Update is called once per frame


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

        Debug.Log(other);
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
            if (playerStates.escapedPlayers.Count <= FindObjectsOnLayer(9).Count)
            {
                //mapMover.moveMaps();
                for (int i = 0; i < players.Count; i++)
                {
                    Transform transform = players[i].GetComponent<Transform>();
                    preyList = FindObjectsOnLayer(7);
                    foreach (GameObject prey in preyList)
                    {
                        if (prey.GetComponentInChildren<P_StateManager>().Escaped == true)
                        {
                            prey.GetComponentInParent<Transform>().position = new Vector3(64.5f, 16.44f, 100);
                        }
                        else if (prey.GetComponentInChildren<P_StateManager>().Escaped == false)
                        {
                            prey.GetComponentInParent<Transform>().position = new Vector3(100f, 0.8f, 100);
                        }
                    }
                    hunterList = FindObjectsOnLayer(6);
                    foreach (GameObject hunter in hunterList)
                    {
                        hunter.GetComponentInParent<Transform>().position = new Vector3(64.5f, 30, 100);
                    }

                    Debug.Log(transform.position);
                    mp.LoadScene("LOBBY");

                    //Debug.Log(other.gameObject);
                    ////StartCoroutine(LoadScene(other.gameObject));
                    //mp.LoadScene("TEMPLOBBY");

                    //if (stateManager.Escaped)
                    //{
                    //    stateManager.Rigidbody.position = new Vector3(64.5f, 16.44f, 100);
                    //}
                    //else if (!stateManager.Escaped)
                    //{
                    //    stateManager.Rigidbody.position = new Vector3(100f, 0.8f, 100);
                    //}
                    //else
                    //{
                    //    stateManager.Rigidbody.position = new Vector3(64.5f, 30, 100);
                    //}

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


    

    



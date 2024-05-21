using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    //not yet synchronized.
    public GameObject youWinText;
    public float delay;
    private P_StateManager stateManager;
    public RoleGiver roleGiver;
    Multiplayer mp = new Multiplayer();

    string sceneName = "WinScene";
    void Start()
    {
      youWinText.SetActive(false);  
    }

    // Update is called once per frame
    

    public IEnumerator CountDown ()
    {
        yield return new WaitForSeconds(delay);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator LoadScene(GameObject player)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WinScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("WinScene"));
        SceneManager.UnloadSceneAsync(currentScene);
    }

    [ExecuteAlways]
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //youWinText.SetActive(true);
            //StartCoroutine(CountDown());
            stateManager = other.gameObject.GetComponentInChildren<P_StateManager>(true);
            if (stateManager != null)
            {
                stateManager.Escaped = true;
                Debug.Log(other.gameObject);
                roleGiver.escapedPlayers.Add(other.gameObject);
                //Debug.Log($"Statemanager: {stateManager}. Escaped status 1: {stateManager.Escaped}");
            }
            else return;
            //untested
            if (roleGiver.escapedPlayers.Count <= roleGiver.players.Length - 1)
            {

                Debug.Log(other.gameObject);
                StartCoroutine(LoadScene(other.gameObject));
                if (stateManager.Escaped)
                {
                    stateManager.Rigidbody.position = new Vector3(64.5f, 16.44f, 100);
                }
                else if (!stateManager.Escaped)
                {
                    stateManager.Rigidbody.position = new Vector3(100f, 0.8f, 100);
                }
                else
                {
                    stateManager.Rigidbody.position = new Vector3(64.5f, 30, 100);
                }
            }
        }
    }
}

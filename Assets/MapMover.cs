using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMover : AttributesSync/*,IInteractable*/
{
    public List<GameObject> players;
    public List<GameObject> preyList;
    public List<GameObject> hunterList;
    P_StateManager pManager;

    

    public GameObject GiveObject()
    {
        return gameObject;
    }

    //NewRoleGover is calling the mapMover script so no interaction needed
    //public void InitInteract(string interactor)
    //{

    //    BroadcastRemoteMethod("Interact", interactor);
            
    //}

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

    //NewRoleGover is calling the mapMover script so no interaction needed

    //[SynchronizableMethod]
    //public void Interact(string interactor)
    //{
    //   // moveMaps();
    //}

    [SynchronizableMethod]
    public void moveMaps(GameObject player)
    {
        Scene scene = SceneManager.GetActiveScene();


        if (scene.name == "LOOBY" || scene.name == "TEMPSTART")
        {
            Debug.Log("moving from start");

            Transform transform = player.GetComponent<Transform>();
                transform.position = new Vector3(0, 3, -10);
                Debug.Log(transform.position);


                Transform parentTransform = player.transform;

                // Find the child GameObjects by name
                Transform firstChild = parentTransform.Find("PreyComponent");
                Transform secondChild = parentTransform.Find("HunterComponent");
                if (firstChild.gameObject.active)
                {

                    firstChild.position = new Vector3(-3, 0, 30);
                    Debug.Log("firstChildMoved" + firstChild.position + firstChild.gameObject.name);
                }
                else
                {
                    secondChild.position = new Vector3(-15, 0, 2.4f);
                    Debug.Log("secondChildMoved" + secondChild.position + secondChild.gameObject.name);
                }

                Multiplayer networkManager = FindAnyObjectByType<Multiplayer>();
                Debug.Log(networkManager.name);
                Debug.Log(networkManager.GetComponent<GameObject>()); //null
                Debug.Log(networkManager.GetComponent<Transform>()); //takes the transform of networkmanager. honestly might just use this to change spawner.
                Transform spawn = networkManager.GetComponent<Transform>();
                    spawn.position = new Vector3(15,5,2.4f);
                

            
            Multiplayer.LoadScene("Final_Map");

        }
        else if (scene.name == "Final_Map")
        {
            Debug.Log("moving from smap");

            Transform transform = player.GetComponent<Transform>();
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
            Multiplayer.LoadScene("LOOBY");
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        players = FindObjectsOnLayer(9);
        
    }
}

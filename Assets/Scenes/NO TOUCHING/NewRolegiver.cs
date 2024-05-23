using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR;
using UnityEditor;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using static UnityEngine.ParticleSystem;

public class NewRoleGiver : AttributesSync, IInteractable
{
    public GameObject hunterCanvas;
    public GameObject preyCanvas;

    public List<GameObject> players;
    public GameObject newPrefab;

    public List<GameObject> escapedPlayers;
    public List<GameObject> caughtPlayers;


    public GameObject GiveObject()
    {
        return gameObject;
    }

    public void InitInteract(string interactor)
    {
        //finds everything on parentPlayer layer.
        players = FindObjectsOnLayer(9);
        BroadcastRemoteMethod("Interact", interactor);
        
        

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

    [SynchronizableMethod]
    public void Interact(string interactor)
    {

        if (players.Count > 0)
        {

            foreach (var obj in players) 
            {

                Transform parentTransform = obj.transform;

                // Find the child GameObjects by name
                Transform firstChild = parentTransform.Find("PreyComponent");
                Transform secondChild = parentTransform.Find("HunterComponent");

                if (!firstChild.gameObject.active)
                {

                    // Turn the first GameObject on
                    firstChild.gameObject.active=true;

                    // Turn the second GameObject off
                    secondChild.gameObject.active = false;
                }

            }

            //int hunterIndex = Random.Range(0, players.Count);
            int hunterIndex = 0;


            for (int i = 0; i < players.Count; i++)
            {
                Alteruna.Avatar avatar = players[i].GetComponent<Alteruna.Avatar>();

                if (i == hunterIndex)
                {
                    players[i].layer = LayerMask.NameToLayer("Hunter");

                    if (!avatar.IsMe)
                        return;
                   SwitchPrefab(hunterIndex);
                    hunterCanvas.SetActive(true);
                }
                else
                {

                    players[i].layer = LayerMask.NameToLayer("Prey");

                    if (!avatar.IsMe)
                        return;
                    preyCanvas.SetActive(true);
                }
            }
        }
        gameObject.SetActive(false);

    }



    [SynchronizableMethod]

    public void SwitchPrefab(int i)
    {
        
        Transform parentTransform = players[i].transform;

        // Find the child GameObjects by name
        Transform firstChild = parentTransform.Find("PreyComponent");
        Transform secondChild = parentTransform.Find("HunterComponent");






        if (firstChild != null && secondChild != null)
        {

            // Turn the second GameObject on
            secondChild.gameObject.active = true;
            // Turn the first GameObject off
            firstChild.gameObject.active = false;




        }
        else
        {
            Debug.LogWarning("One or both child GameObjects not found.");
           
        }

    }

    

    public void Tag(GameObject tagger, GameObject tagged)
    {
        tagged.transform.position = new Vector3(63.7f, 10.58f, -17.28f);

    }

    void Start()
    {
        hunterCanvas.SetActive(false);
        preyCanvas.SetActive(false);
        
    }

    void Update()
    {

    }

    //public GameObject InstantiateWithAvatar()
    //{

    //}

}

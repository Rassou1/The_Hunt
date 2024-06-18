using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XR;
using UnityEditor;

public class RoleGiver: AttributesSync,IInteractable
{
    public GameObject hunterCanvas;
    public GameObject preyCanvas;

    public GameObject[] players;
    public GameObject newPrefab;

    public List<GameObject> escapedPlayers;
    public List<GameObject> caughtPlayers;

    public Spawner spawner;

    public GameObject GiveObject()
    {
        return gameObject;
    }

    public void InitInteract(string interactor)
    {
        
        players = GameObject.FindGameObjectsWithTag("Player");
        BroadcastRemoteMethod("Interact", interactor);

    }
    
    [SynchronizableMethod] 
    public void Interact(string interactor)
    {
        
        if (players.Length > 0)
        {
            int hunterIndex = Random.Range(0, players.Length);

            for (int i = 0; i < players.Length; i++)
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
    
    

    [SynchronizableMethod] public void SwitchPrefab(int i)
    {
        // Get the current player's transform
        Transform currentTransform = transform; //works as intended

        // Save the position and rotation
        Vector3 savedPosition = currentTransform.position; //works as intended
        Quaternion savedRotation = currentTransform.rotation;

        // Destroy the current player prefab
        Destroy(players[i].gameObject);

        //Uses Alteruna spawner to spawn a pre-synchronized gameobject.
        spawner.Spawn(newPrefab.name, savedPosition, savedRotation);


       // // Instantiate the new prefab at the saved position and rotation
       // GameObject newPlayer = Instantiate(newPrefab, savedPosition, savedRotation); //works, no avatar though.
       
       // Alteruna.Avatar newAvatar = players[i].gameObject.GetComponent<Alteruna.Avatar>();
       // User newUser = newAvatar.GetComponent<User>();

        

       // // Destroy the current player prefab
       // Destroy(players[i].gameObject);

       
        

       // Alteruna.Avatar emptyHunterAvatar = newPlayer.gameObject.GetComponent<Alteruna.Avatar>();
       // emptyHunterAvatar.Possessed(newUser); //please work please i beg you please please please pelase

       //// emptyHunterAvatar = newAvatar;


       //players[i] = newPlayer;
       // Debug.Log(players[i]);
    }

    public void Tag(GameObject tagger, GameObject tagged) 
    {
        tagged.transform.position = new Vector3(63.7f, 10.58f, -17.28f);

    }

    void Start()
    {
        hunterCanvas.SetActive(false);
        preyCanvas.SetActive(false);
        spawner = new Spawner();
    }

    void Update()
    {

    }

    //public GameObject InstantiateWithAvatar()
    //{

    //}

}

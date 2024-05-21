using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using JetBrains.Annotations;
using Unity.VisualScripting;
public class RoleGiver: AttributesSync,IInteractable
{
    public GameObject hunterCanvas;
    public GameObject preyCanvas;

    public GameObject[] players;
    public GameObject newPrefab;

    public List<GameObject> escapedPlayers;
    public List<GameObject> caughtPlayers;

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

    [SynchronizableMethod]public void SwitchPrefab(int i)
    {
        // Get the current player's transform
        Transform currentTransform = transform;

        // Save the position and rotation
        Vector3 savedPosition = currentTransform.position;
        Quaternion savedRotation = currentTransform.rotation;

        // Destroy the current player prefab
        Destroy(players[i].gameObject);

        // Instantiate the new prefab at the saved position and rotation
        GameObject newPlayer = Instantiate(newPrefab, savedPosition, savedRotation);
        players[i] = newPlayer;
        
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
}

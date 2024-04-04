using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
public class Numbergen : AttributesSync,IInteractable
{

    [SynchronizableField]
    private bool isActive;

    [SynchronizableField]
    GameObject[] players;
    public void Interact()
    {

        isActive = false;
        gameObject.SetActive(isActive);

         players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            int hunterIndex = Random.Range(0, players.Length);

            for (int i = 0; i < players.Length; i++)
            {
                if (i == hunterIndex)
                {
                    players[i].layer = LayerMask.NameToLayer("Hunter");
                }
                else
                {
                    players[i].layer = LayerMask.NameToLayer("Prey");
                }
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

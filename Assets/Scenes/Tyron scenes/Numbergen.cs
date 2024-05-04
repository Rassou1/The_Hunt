using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
public class RoleGiver: AttributesSync,IInteractable
{

    public GameObject hunterc;
    public GameObject GiveObject()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(GameObject interactor, Alteruna.Avatar interactorAvatar)
    {

        gameObject.SetActive(false);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

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

    public void Tag(GameObject tagger, GameObject tagged) 
    {
        tagged.transform.position = new Vector3(63.7f, 10.58f, -17.28f);

    }

    void Start()
    {

    }

    void Update()
    {

    }
}

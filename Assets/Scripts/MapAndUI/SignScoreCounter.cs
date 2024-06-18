using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Alteruna;
using TMPro;
using UnityEngine;
using Avatar = Alteruna.Avatar;

public class SignScoreCounter : MonoBehaviour
{
    //This script makes a list of all the players and lists them by the most diamonds taken and shows the one who took the most on the sign -Jonathan
    Multiplayer multiplayerManager;
    List<Avatar> avatars = new List<Avatar>();
    List<User> users = new List<User>();
    List<(int,string)> diamondsCollected = new List<(int, string)> ();
    (int,string) amountUser;
    TextMeshPro Text;
    // Start is called before the first frame update
    void Start()
    {
        
        Text = GetComponent<TextMeshPro>();
        multiplayerManager = GameObject.FindWithTag("NetworkManager").GetComponent<Multiplayer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        avatars = multiplayerManager.GetAvatars();
        users = multiplayerManager.GetUsers();

        if (diamondsCollected.Count > 0 && diamondsCollected[0].Item1 > 0)
        {
            diamondsCollected = (List<(int, string)>)diamondsCollected.OrderByDescending(i => i.Item1).ToList();

            amountUser = diamondsCollected.First<(int, string)>();

            Text.text = $"{amountUser.Item2} Got the most diamonds:{amountUser.Item1}";
        }
    }
}

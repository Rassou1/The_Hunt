using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreyCounterUI : MonoBehaviour
{

    PlayerStates playerStates;
    int playerAmount;
    int taggedAmount;
    public TextMeshProUGUI textMeshProUGUI;
    public string output;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerStates = GameObject.Find("NetworkManager").GetComponent<PlayerStates>();
        playerAmount = playerStates.prey.Count;
        taggedAmount = playerStates.taggedPlayers.Count;
        output = "Prey: " + taggedAmount + "/" + playerAmount;
        textMeshProUGUI.text = output;
    }
}

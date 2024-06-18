using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class temptestingshits_ofdoom : MonoBehaviour
{
    public InteractablePlayer taggingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        taggingPlayer = FindAnyObjectByType<InteractablePlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            taggingPlayer.SendToPrison();
        }
    }
}

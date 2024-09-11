using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetOnAwake : MonoBehaviour
{
    PlayerStates playerStates = FindAnyObjectByType<PlayerStates>();
    // Start is called before the first frame update
    void Start()
    {
        playerStates.roleGiver.ResetAllPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

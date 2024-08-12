using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This just resets the level if the prey enters the reset area of the pit in the trial level - Love
public class SP_P_Trial_PitReset : MonoBehaviour
{
    public SP_Prey_GameManager _manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _manager.ResetLevel();
        }
    }
}

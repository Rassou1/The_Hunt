using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This just resets the level if the hunter enters the reset area of the pit in the trial level - Love
public class SP_H_Trial_PitReset : MonoBehaviour
{
    public SP_Hunter_GameManager _manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _manager.ResetLevel();
        }
    }
}

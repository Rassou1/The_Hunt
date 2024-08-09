using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script just notices when you enter the exit zone and call for the level to finish - Love
public class SP_PreyTrialExit : MonoBehaviour
{
    public SP_Prey_GameManager _manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _manager.FinishLevel();
        }
    }
}

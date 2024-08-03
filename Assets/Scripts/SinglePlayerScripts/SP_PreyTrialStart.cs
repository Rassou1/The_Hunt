using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_PreyTrialStart : MonoBehaviour
{
    public SP_Prey_GameManager _manager;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _manager.StartLevel();
            gameObject.SetActive(false);
        }
    }
}

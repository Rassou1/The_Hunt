using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

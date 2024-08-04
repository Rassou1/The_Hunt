using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_HunterTialStart : MonoBehaviour
{
    public SP_Hunter_GameManager _manager;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _manager.StartLevel();
            gameObject.SetActive(false);
        }
    }
}

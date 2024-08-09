using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script just exists to notice when the player exit the bounds of the starting area and call for the level to start - Love
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

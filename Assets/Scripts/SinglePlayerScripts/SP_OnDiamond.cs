using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//This script is placed on the pick-up diamonds in the singleplayer mode - Love
public class SP_OnDiamond : MonoBehaviour
{
    public SP_PickupManager _manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _manager.RemoveDiamond();
            gameObject.SetActive(false);
        }
    }
}

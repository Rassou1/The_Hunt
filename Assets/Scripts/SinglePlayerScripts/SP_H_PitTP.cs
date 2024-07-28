using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_H_PitTP : MonoBehaviour
{
    public Vector3 _respawnPos;
    public SP_H_StateManager _playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerScript.TeleportPlayer(_respawnPos);
        }
    }
}

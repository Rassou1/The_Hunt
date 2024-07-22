using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_PitTP : MonoBehaviour
{
    public Vector3 _respawnPos;
    public SP_P_StateMachine _playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerScript.TeleportPlayer(_respawnPos);
        }
    }
}

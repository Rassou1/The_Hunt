using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLadderCollision : MonoBehaviour
{

    public bool isTouchingLadder;
    private void OnTriggerEnter(Collider other)
    {
        // Burada hem tag hem de layer kontrolü yapılıyor.
        if (other.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            P_StateManager stateManager = other.GetComponent<P_StateManager>();
            Debug.Log("collinision");
            if (stateManager != null)
            {
                isTouchingLadder = true;
                
                //stateManager.SwitchState(stateManager._stateFactory.Climb());
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.layer != LayerMask.NameToLayer("Ladder"))
        {
            P_StateManager stateManager = other.GetComponent<P_StateManager>();
            if (stateManager != null)
            {
                isTouchingLadder = false;
                //stateManager.SwitchState(stateManager._stateFactory.Idle());
            }
        }
    }
}

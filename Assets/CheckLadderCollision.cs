using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CheckLadderCollision : MonoBehaviour
{
    private P_ClimbingState climbingState;
    private P_IdleState idleState;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        // H�mta eller skapa kl�ttringstillst�ndet om det inte finns
    //        if (climbingState == null)
    //        {
    //            climbingState = new P_ClimbingState(P_StateManager.Instance);
    //        }

    //        // Aktivera kl�ttringstillst�ndet
    //        P_StateManager.Instance.SetState(climbingState);
    //    }
    }

    // Om spelaren l�mnar stegen kan du inaktivera kl�ttringstillst�ndet och byta tillbaka till idleState
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        if (idleState == null)
    //        {
    //            // Skapa en instans av P_IdleState h�r
    //            idleState = new P_IdleState(P_StateManager.Instance);
    //        }

    //        // Aktivera idleState
    //        P_StateManager.Instance.SetState(idleState);
    //    }
    //}
}

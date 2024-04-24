using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] private Animator Dooranim;
    
    public IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Coroutine started, attempting to close door.");
        Dooranim.SetBool("Open", false);
        Dooranim.enabled = false;
        Debug.Log("Door should be closed now.");

    }
}


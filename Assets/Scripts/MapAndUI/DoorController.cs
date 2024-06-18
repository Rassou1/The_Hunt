using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class DoorController : MonoBehaviour
//{

//    [SerializeField] private Animator Dooranim;

//    public IEnumerator StopDoor()
//    {
//        yield return new WaitForSeconds(0.5f);
//        Debug.Log("Coroutine started, attempting to close door.");
//        Dooranim.SetBool("Open", false);
//        Dooranim.enabled = false;
//        Debug.Log("Door should be closed now.");

//    }
//}


public class DoorController : MonoBehaviour
{
    public float raiseHeight = 1.5f;  
    public float raiseSpeed = 2f;   
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool doorOpened = false;

    void Start()
    {
        originalPosition = transform.position;  
        targetPosition = new Vector3(originalPosition.x, originalPosition.y + raiseHeight, originalPosition.z);
    }

    void Update()
    {
     
        if (doorOpened)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, raiseSpeed * Time.deltaTime);
        }
    }


    public void OpenDoor()
    {
        doorOpened = true;
    }
}


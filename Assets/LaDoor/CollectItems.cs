using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    [SerializeField] private Animator Dooranim;
    public GameObject Diamonds;
    private static int diamondsCollected = 0;  // diamonds counter
    private int requiredDiamonds = 2;   // Required diamonds to open the door
    private bool isCollected = false;

    void Start()
    {
        Diamonds.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Q) && !isCollected)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;  // do object invisible
            }
            isCollected = true; //Mark the object as collected
            diamondsCollected++;  // Increase diamonds count
            CheckDiamondsCollected();
        }
       
    }

  
    private void CheckDiamondsCollected()
    {
        if (diamondsCollected >= requiredDiamonds)
        {

            // Opens the door if enough diamonds are collected
            Dooranim.SetBool("Open", true);
            DoorController doorController = Dooranim.GetComponent<DoorController>();
            if (doorController != null)
            {
                Debug.Log("DoorController found, starting coroutine.");
                StartCoroutine(doorController.StopDoor());
            }
            else
            {
                Debug.Log("DoorController component not found on Dooranim object.");
            }
            Debug.Log("itemcollet" +  diamondsCollected);
            
        }

        


    }
}

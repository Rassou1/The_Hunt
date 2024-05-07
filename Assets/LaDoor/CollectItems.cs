using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//public class CollectItems : MonoBehaviour
//{
//    [SerializeField] private Animator Dooranim;
//    public GameObject Diamonds;
//    private static int diamondsCollected = 0;  // diamonds counter
//    private int requiredDiamonds = 4;   // Required diamonds to open the door
//    private bool isCollected = false;

//    void Start()
//    {
//        Diamonds.SetActive(true);
//    }

//    private void OnTriggerStay(Collider other)
//    {

//        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Q) && !isCollected)
//        {
//            Renderer renderer = GetComponent<Renderer>();
//            if (renderer != null)
//            {
//                renderer.enabled = false;  // do object invisible
//            }
//            isCollected = true; //Mark the object as collected
//            diamondsCollected++;  // Increase diamonds count
//            CheckDiamondsCollected();
//        }

//    }


//    private void CheckDiamondsCollected()
//    {
//        if (diamondsCollected >= requiredDiamonds)
//        {

//            // Opens the door if enough diamonds are collected
//            Dooranim.SetBool("Open", true);
//            DoorController doorController = Dooranim.GetComponent<DoorController>();
//            if (doorController != null)
//            {
//                Debug.Log("DoorController found, starting coroutine.");
//                StartCoroutine(doorController.StopDoor());
//            }
//            else
//            {
//                Debug.Log("DoorController component not found on Dooranim object.");
//            }
//            Debug.Log("itemcollet" +  diamondsCollected);

//        }




//    }
//}
//public class CollectItems : MonoBehaviour
//{
//    [SerializeField] private Animator Dooranim;
//    public GameObject Diamonds;
//    private static int diamondsCollected = 0;  // diamonds counter
//    private int requiredDiamonds = 1;   // Required diamonds to open the door
//    private bool isCollected = false;

//    void Start()
//    {
//        // Ensure the diamond is visible and interactable at the start
//        Diamonds.SetActive(true);
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        // Check if the player is interacting and the diamond hasn't been collected yet
//        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Q) && !isCollected)
//        {
//            CollectDiamond();
//        }
//    }

//    private void CollectDiamond()
//    {
//        // Disable the visual and physical components of the diamond
//        Renderer renderer = GetComponent<Renderer>();
//        Collider collider = GetComponent<Collider>();
//        if (renderer != null)
//        {
//            renderer.enabled = false;  // Make the object invisible
//        }
//        if (collider != null)
//        {
//            collider.enabled = false;  // Disable the collider
//        }

//        // Mark the diamond as collected and update the count
//        isCollected = true;
//        diamondsCollected++;
//        CheckDiamondsCollected();
//    }

//    private void CheckDiamondsCollected()
//    {
//        // Check if the required number of diamonds has been collected
//        if (diamondsCollected >= requiredDiamonds)
//        {
//            // Open the door if enough diamonds are collected
//            Dooranim.SetBool("Open", true);
//            DoorController doorController = Dooranim.GetComponent<DoorController>();
//            if (doorController != null)
//            {
//                Debug.Log("DoorController found, starting coroutine.");
//                StartCoroutine(doorController.StopDoor());
//            }
//            else
//            {
//                Debug.Log("DoorController component not found on Dooranim object.");
//            }
//            Debug.Log("itemcollected" + diamondsCollected);
//        }
//    }
//}
public class CollectItems : MonoBehaviour
{
    [SerializeField] private Animator Dooranim;
    public GameObject Diamonds;
    private static int diamondsCollected = 0;
    private int requiredDiamonds = 2;
    private bool isCollected = false;
    public Text diamondCountText;
    private Renderer _renderer;
    private Collider _collider;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
        if (Diamonds != null)
        {
            Diamonds.SetActive(true);  // Ensure the diamond is visible and interactable at the start
        }
        UpdateDiamondText();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Q) && !isCollected)
        {
            CollectDiamond();
        }
    }

    private void CollectDiamond()
    {
        //if (_renderer != null)
        //{
        //    _renderer.enabled = false;  // Make the object invisible
        //}
        //if (_collider != null)
        //{
        //    _collider.enabled = false;  // Disable the collider
        //}
        if (!isCollected)
        {

            Destroy(gameObject);
            isCollected = true;  // Mark the diamond as collected
            diamondsCollected++;
            UpdateDiamondText();
            CheckDiamondsCollected();
        }
    }
    private void CheckDiamondsCollected()
    {
        if (diamondsCollected >= requiredDiamonds)
        {
            DoorController doorController = Dooranim.GetComponent<DoorController>();
            if (doorController != null)
            {
                doorController.OpenDoor();  // Kapıyı aç
            }
        }
    }
    void UpdateDiamondText()
    {
        if (diamondCountText != null)
        {
            diamondCountText.text = "Diamonds: " + diamondsCollected;  // Metni güncelle
        }
    }
}
//public class CollectItems : MonoBehaviour
//{
//    [SerializeField] private Animator Dooranim;
//    public GameObject Diamonds;
//    private static int diamondsCollected = 0;
//    private int requiredDiamonds = 2;
//    private bool isCollected = false;
//    public Text diamondCountText;  // Text elementi için bir referans

//    void Start()
//    {
//        if (Diamonds != null)
//        {
//            Diamonds.SetActive(true);
//        }
//        UpdateDiamondText();  // Başlangıçta metni güncelle
//    }

//    private void OnTriggerStay(Collider other)
//    {
//        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.Q) && !isCollected)
//        {
//            CollectDiamond();
//        }
//    }

//    private void CollectDiamond()
//    {
//        Destroy(gameObject);
//        isCollected = true;
//        diamondsCollected++;
//        UpdateDiamondText();  // Her toplamadan sonra metni güncelle
//        CheckDiamondsCollected();
//    }

//    private void CheckDiamondsCollected()
//    {
//        if (diamondsCollected >= requiredDiamonds)
//        {
//            DoorController doorController = Dooranim.GetComponent<DoorController>();
//            if (doorController != null)
//            {
//                doorController.OpenDoor();
//            }
//        }
//    }

    

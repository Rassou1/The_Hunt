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
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float raiseHeight = 1.5f;  // Kapının kalkacağı yükseklik
    public float raiseSpeed = 2f;   // Kapının kalkma hızı (metre/saniye)
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool doorOpened = false;

    void Start()
    {
        originalPosition = transform.position;  // Başlangıç pozisyonunu kaydet
        targetPosition = new Vector3(originalPosition.x, originalPosition.y + raiseHeight, originalPosition.z);
    }

    void Update()
    {
        // Kapıyı yavaşça hedef pozisyona taşı
        if (doorOpened)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, raiseSpeed * Time.deltaTime);
        }
    }

    // Kapıyı açma işlemi için bir method
    public void OpenDoor()
    {
        doorOpened = true;
    }
}


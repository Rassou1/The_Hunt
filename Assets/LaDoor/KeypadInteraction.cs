using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeypadInteraction : MonoBehaviour
{
    //public GameObject Keypad; // Keypad UI objesi Unity Editor'den atanacak

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "keyp") // Karakterinizin tag'ı "Player" ise
    //    {
    //        Keypad.SetActive(true); // Etkileşimde Keypad UI'ı aktifleştir
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.name == "keyp")
    //    {
    //        Keypad.SetActive(false); // Karakter etkileşim alanından çıkınca UI'ı kapat
    //    }
    //}
    public GameObject Keypad; /// Inspector'dan atayacağınız UI elementi

    //void Start()
    //{
    //    Keypad.SetActive(false);
    //}
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) // Karakterinizin tag'ı "Player" ise
        {
            //if (Input.GetMouseButton(4))
            //{ 
            Keypad.SetActive(true); // Etkileşimde Keypad UI'ı aktifleştir
                                    //this.gameObject.SetActive(false);
                                    //}
        }
        else if (!other.CompareTag("Player"))
        {
            Keypad.SetActive(false);
        }
    }

    //private void OnTriggerPad(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //this.gameObject.SetActive(false);
    //        Keypad.SetActive(false); // Karakter etkileşim alanından çıkınca UI'ı kapat
    //    }
    //}
}


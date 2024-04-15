using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckLadderCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu ladder nesnesine temas ettiğinde tırmanma işlevselliğini etkinleştir
            other.GetComponent<PlayerMovement>().climbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu ladder nesnesinden ayrıldığında tırmanma işlevselliğini devre dışı bırak
            other.GetComponent<PlayerMovement>().climbing = false;
        }
    }
}

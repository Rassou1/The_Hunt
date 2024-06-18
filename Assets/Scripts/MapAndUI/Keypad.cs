using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    [SerializeField] private Text Scrb;
    [SerializeField] private Animator Dooranim ;
    private string code = "a";
    public void Alhapet(string alp)
    {
        Scrb.text += alp.ToString();
    }

    public void Enter()
    {
        if (Scrb.text == code)
        {
            Scrb.text = "Correct";
            Dooranim.SetBool("Open", true);
            StartCoroutine("StopDoor");
        }
        else
        { Scrb.text = "Invalid"; }
    }
    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(0.5f);
        Dooranim.SetBool("Open", false);
        Dooranim.enabled = false;
    }
}

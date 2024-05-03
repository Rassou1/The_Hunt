using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject youWinText;
    public float delay;
    void Start()
    {
      youWinText.SetActive(false);  
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            youWinText.SetActive(true);
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown ()
    {
        yield return new WaitForSeconds(delay);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}

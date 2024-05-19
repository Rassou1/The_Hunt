using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject youWinText;
    public float delay;
    public P_StateManager stateManager;
    void Start()
    {
      youWinText.SetActive(false);  
    }

    // Update is called once per frame
    

    public IEnumerator CountDown ()
    {
        yield return new WaitForSeconds(delay);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [ExecuteAlways]
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            youWinText.SetActive(true);
            StartCoroutine(CountDown());
            stateManager = other.gameObject.GetComponentInChildren<P_StateManager>();
            if (stateManager != null)
            {
                stateManager.Escaped = true;
            }
            else return;
        }
    }
}

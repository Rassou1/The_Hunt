using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool isPaused=false;


    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu.SetActive(false);
       
        pauseMenu.active = false;
        Debug.Log(pauseMenu.active);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Moms Spaghetti" +
                      "He's nurvess" +
                      "but on the serfes he looks calm and ready" +
                      "to drop booms" +
                      "but he keeps on forgetting " +
                      "what he wrote down, the whole crowd grows so loud" +
                      "he opens his mouth, but the words wont come out " +
                      "his choking out, everybodys joking now the clocks run out, times over , plow" +
                      "snap back to reality, ow there ");
            if (isPaused) 
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        } 
    }

    public void PauseGame()
    {
        pauseMenu.active = true;
        UnlockMouse();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.active = false;
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}

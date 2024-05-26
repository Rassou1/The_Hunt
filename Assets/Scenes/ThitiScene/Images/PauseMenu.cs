using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public List<GameObject> lobbyMenu;

    public bool isPaused=false;


    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu.SetActive(false);
       
        pauseMenu.active = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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

        foreach (GameObject go in lobbyMenu)
        {
            go.active = true;
        }

        UnlockMouse();
        Time.timeScale = 1f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.active = false;

        foreach (GameObject go in lobbyMenu)
        {
            go.active = false;
        }
        LockMouse();
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}

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
            Debug.Log("mom's spaghetti\r\nHe's nervous, but on the surface he looks calm and ready\r\nto drops bombs, but he keeps on forgetting\r\nwhat he wrote down, the whole crowd goes so loud\r\nHe opens his mouth but the words won't come out\r\nHe's chokin, how? Everybody's jokin now\r\nThe clock's run out, time's up, over - BLAOW!\r\nSnap back to reality, OHH! there goes gravity\r\nOHH! there goes Rabbit, he choked\r\nHe's so mad, but he won't\r\nGive up that easy nope, he won't have it\r\nHe knows, his whole back's to these ropes\r\nIt don't matter, he's dope\r\nHe knows that, but he's broke\r\nHe's so sad that he knows\r\nwhen he goes back to this mobile home, that's when it's\r\nback to the lab again, yo, this whole rap shift\r\nHe better go capture this moment and hope it don't pass him");
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
        Time.timeScale = 1f;
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

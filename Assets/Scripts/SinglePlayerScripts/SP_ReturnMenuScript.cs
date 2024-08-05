using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//This script returns the player to the singleplayer menu, either through entering the exit area or pressing esc. - Love
public class SP_ReturnMenuScript : MonoBehaviour
{
    PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.PreyControls.Escape.started += OnEscape;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ReturnToMenu();
        }
    }

    private void ReturnToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("SP_Menu");
    }

    void OnEscape(InputAction.CallbackContext context)
    {
        ReturnToMenu();
    }

    void OnEnable()
    {
        _playerInput.PreyControls.Enable();
    }

    void OnDisable()
    {
        _playerInput.PreyControls.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
// This is the script that manages most things in the prey trial - Love
public class SP_Prey_GameManager : MonoBehaviour
{
    PlayerInput _playerInput;
    [SerializeField] SP_PickupManager _pickupManager;
    [SerializeField] GameObject _startPoint;
    [SerializeField] GameObject _door1;
    [SerializeField] GameObject _door2;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _timerHolder;
    [SerializeField] TMP_Text _timerDisplay;
    [SerializeField] string _timerFilePath;
    private Vector3 _door1InitPos;
    private Vector3 _door2InitPos;

    private GameObject _startPointStopper;
    IEnumerator _removeStartStopper;

    private float? _timerTime;
    private float? _bestTime;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.PreyControls.ResetSPLevel.started += OnResetLevel;
        _playerInput.PreyControls.Escape.started += OnEscape;
        
        _door1InitPos = _door1.transform.position;
        _door2InitPos = _door2.transform.position;
        _startPointStopper = _startPoint.transform.GetChild(0).gameObject;
        _bestTime = JSON_Handler.Load(_timerFilePath);
        _timerDisplay.text = "Best Time: " + _bestTime?.ToString("0.##") ?? "No saved times";
        _timerDisplay.text += "\n Latest Time: ";
    }

    public void StartLevel()
    {
        //Just in case we somehow missed deleting the last timer due to something unexpected happening we delete it again if it exist - Love
        if (_timerHolder.GetComponent<SP_Timer>())
        {
            Destroy(_timerHolder.GetComponent<SP_Timer>());
        }
        _timerHolder.AddComponent<SP_Timer>();
        Debug.Log("Level started");
    }

    public void FinishLevel()
    {
        //Save time, delete timer, and update the display board
        if (_timerHolder.GetComponent<SP_Timer>())
        {
            _timerTime = _timerHolder.GetComponent<SP_Timer>().GetTime();
            Destroy(_timerHolder.GetComponent<SP_Timer>());
        }
        else _timerTime = null;
        WriteTimer(_timerTime);
        Debug.Log("Level finished");
        ResetLevel();
    }


    void ResetLevel()
    {
        _pickupManager.RespawnDiamonds();
        DoorReset();
        if (_timerHolder.GetComponent<SP_Timer>()) Destroy(_timerHolder.GetComponent<SP_Timer>());
        _startPoint.SetActive(true);
        _startPointStopper.SetActive(true);
        _player.transform.position = _startPoint.transform.position + new Vector3(0, 1.5f, 0);
        _removeStartStopper = RemoveStartStopper();
        StartCoroutine(_removeStartStopper);
    }

    //The "start stopper" is a barrier within the start bubble which exists for a second after restarting a level so you don't accidentally
    //just run straight out of the bubble if you were holding W as you won/reset the level. - Love
    IEnumerator RemoveStartStopper()
    {
        yield return new WaitForSeconds(1f);
        _startPointStopper.SetActive(false);
    }

    private void ReturnToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("SP_Menu");
    }

    void OnEscape(InputAction.CallbackContext context)
    {
        //Commented out so I don't return to another menu whenever i try to use the editor while testing - Love
        //ReturnToMenu();
    }

    void OnResetLevel(InputAction.CallbackContext context)
    {
        ResetLevel();
    }

    void DoorReset()
    {
        if (_door1.GetComponent<SP_DoorOpen>()) Destroy(_door1.GetComponent<SP_DoorOpen>());
        if (_door2.GetComponent<SP_DoorOpen>()) Destroy(_door2.GetComponent<SP_DoorOpen>());
        _door1.transform.position = _door1InitPos;
        _door2.transform.position = _door2InitPos;
        _pickupManager._exitOpened = false;
    }

    public void WriteTimer(float? _inputTime)
    {
        if (_inputTime == null) return;
        _bestTime = JSON_Handler.CheckOverwrite(_inputTime.Value, _timerFilePath);
        _timerDisplay.text = "Best Time: " + _bestTime?.ToString("0.##") ?? "No saved times";
        _timerDisplay.text += "\n Latest Time: " + _inputTime?.ToString("0.##") ?? string.Empty;
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

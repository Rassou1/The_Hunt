using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SP_Prey_GameManager : MonoBehaviour
{
    PlayerInput _playerInput;
    [SerializeField] PickupManager _pickupManager;
    [SerializeField] GameObject _startPoint;
    [SerializeField] GameObject _door1;
    [SerializeField] GameObject _door2;
    [SerializeField] GameObject _player;
    private Vector3 _door1InitPos;
    private Vector3 _door2InitPos;

    private GameObject _startPointStopper;
    IEnumerator _removeStartStopper;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.PreyControls.ResetSPLevel.started += OnResetLevel;
        _door1InitPos = _door1.transform.position;
        _door2InitPos = _door2.transform.position;
        _startPointStopper = _startPoint.transform.GetChild(0).gameObject;
    }

    public void StartLevel()
    {
        //Start timer
        Debug.Log("Level started");
    }

    public void FinishLevel()
    {
        //End timer, save time and write on display
        Debug.Log("Level finished");
        ResetLevel();
    }


    void ResetLevel()
    {
        _pickupManager.RespawnDiamonds();
        DoorReset();
        _startPoint.SetActive(true);
        _startPointStopper.SetActive(true);
        _player.transform.position = _startPoint.transform.position + new Vector3(0, 1.5f, 0);
        _removeStartStopper = RemoveStartStopper();
        StartCoroutine(_removeStartStopper);
    }

    IEnumerator RemoveStartStopper()
    {
        yield return new WaitForSeconds(1.5f);
        _startPointStopper.SetActive(false);
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

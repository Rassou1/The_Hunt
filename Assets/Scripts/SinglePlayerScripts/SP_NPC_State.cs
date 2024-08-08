using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SP_NPC_State : MonoBehaviour
{
    private SP_NPC_StateManager _stateManager;
    private Vector3 _targetPos;
    private float _speed;
    private bool _run;
    private bool _slide;
    public int _id;

    private GameObject _attachedTo;

    private Vector3 _direction;

    public SP_NPC_State(Vector3 targetPos, float speed, bool run, bool slide, int id, SP_NPC_StateManager manager)
    {
        _targetPos = targetPos;
        _speed = speed;
        _run = run;
        _slide = slide;
        _id = id;
        _stateManager = manager;
        _attachedTo = manager.gameObject;
    }

    public void EnterState()
    {
        _stateManager.Animator.SetBool("isRunning", _run);
        _stateManager.Animator.SetBool("isSliding", _slide);
        _attachedTo.transform.rotation = Quaternion.FromToRotation(_attachedTo.transform.position, _targetPos);
        _direction = (_targetPos - _attachedTo.transform.position).normalized;
    }


    public void UpdateState()
    {
        _attachedTo.transform.position += _direction * _speed * Time.deltaTime;
        if(Vector3.Distance(_targetPos, _attachedTo.transform.position) <= 0.1f) _stateManager.SwitchState();
    }



}

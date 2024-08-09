using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
//This is the class which gets created from the path info. It handles one part of the route the prey npc move.
//Options include: Target, speed, what animation it will use, if it just waits (will then look in the direction of the pathinfo ball), and for how long it will wait. - Love
public class SP_NPC_State
{
    private SP_NPC_StateManager _stateManager;
    private Vector3 _targetPos;
    private float _speed;
    private bool _run;
    private bool _slide;
    private bool _wait;
    private float _waitTime;

    private GameObject _attachedTo;

    private Vector3 _direction;
    private float _timePassed;

    public SP_NPC_State(Vector3 targetPos, float speed, bool run, bool slide, bool wait, float waitTime, SP_NPC_StateManager manager)
    {
        _targetPos = targetPos;
        _speed = speed;
        _run = run;
        _slide = slide;
        _wait = wait;
        _waitTime = waitTime;
        _stateManager = manager;
        _attachedTo = manager.gameObject;
    }

    public void EnterState()
    {
        _stateManager.Animator.SetBool("IsRunning", _run);
        _stateManager.Animator.SetBool("IsSliding", _slide);
        _direction = (_targetPos - _attachedTo.transform.position).normalized;
        _attachedTo.transform.rotation = Quaternion.LookRotation(_direction);
        _timePassed = 0;
    }

    public void UpdateState()
    {
        if (_wait)
        {
            _timePassed += Time.deltaTime;
            if (_timePassed > _waitTime) _stateManager.SwitchState();
        }
        else
        {
            _attachedTo.transform.position += _direction * _speed * Time.deltaTime;
            if (Vector3.Distance(_targetPos, _attachedTo.transform.position) <= 0.1f) _stateManager.SwitchState();
        }
    }
}

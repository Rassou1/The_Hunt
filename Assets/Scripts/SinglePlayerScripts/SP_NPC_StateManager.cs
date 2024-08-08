using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SP_NPC_StateManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _pathPointsCollection;
    [SerializeField] private SP_Hunter_GameManager _gameManager;
    public Animator Animator {  get { return _animator; } set { _animator = value; } }

    private SP_NPC_State _activeState;
    private SP_NPC_State _firstState;
    private List<SP_NPC_State> _statesList = new List<SP_NPC_State>();

    private Vector3 _startPoint;

    public bool _activeLevel;

    public SP_NPC_StateManager()
    {
        foreach (Transform child in _pathPointsCollection.transform)
        {
            SP_NPC_PathInfo tempPath = child.GetComponent<SP_NPC_PathInfo>();
            _statesList.Add(new SP_NPC_State(child.transform.position, tempPath._speed, tempPath._run, tempPath._slide, tempPath._id, this));
        }
        _firstState = _statesList.FirstOrDefault(state => state._id == 1);
        _activeState= _firstState;
        _startPoint = gameObject.transform.position;
    }

    public void StartLevel()
    {
        _activeLevel = true;
    }

    public void ResetPrey()
    {
        gameObject.transform.position = _startPoint;
        _activeState = _firstState;
        _activeLevel= false;
    }


    void Update()
    {
        if (_activeLevel) _activeState.UpdateState();
    }

    public void SwitchState()
    {
        _activeState = _statesList.FirstOrDefault(state => state._id == _activeState._id + 1) ?? _firstState;
        _activeState.EnterState();
    }
}

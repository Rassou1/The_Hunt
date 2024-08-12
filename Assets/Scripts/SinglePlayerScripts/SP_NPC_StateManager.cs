using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//This script manages the states of the NPC prey, it also calls for them to be created and puts them in a list
//They get put in the list by their order in the editor window hierarchy, so you can just move them around in there if you want to change the order of the checkpoints on their route.
//Other than that, it's just your standard state manager. Keeps a list of possible states, updates the current state, and finally handles switching between states. - Love
public class SP_NPC_StateManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _pathPointsCollection;
    [SerializeField] private SP_Hunter_GameManager _gameManager;
    public Animator Animator {  get { return _animator; } set { _animator = value; } }

    private SP_NPC_State _activeState;
    private List<SP_NPC_State> _statesList = new List<SP_NPC_State>();

    private Vector3 _startPoint;

    [HideInInspector] public bool _activeLevel;

    public SP_NPC_StateManager()
    {
        
    }

    public void Start()
    {
        foreach (Transform child in _pathPointsCollection.transform)
        {
            if (child.TryGetComponent<SP_NPC_PathInfo>(out SP_NPC_PathInfo tempPath))
            {
                _statesList.Add(tempPath.CreateState(this));
            }
        }
        _activeState = _statesList[0];
        _startPoint = gameObject.transform.position;
    }

    public void StartLevel()
    {
        _activeLevel = true;
        _activeState.EnterState();
    }

    public void ResetPrey()
    {
        gameObject.transform.position = _startPoint;
        _activeState = _statesList[0];
        _activeLevel = false;
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsSliding", false);
    }


    void Update()
    {
        if (_activeLevel) _activeState.UpdateState();
    }

    public void SwitchState()
    {
        //If we ever try to switch to a next state which doesn't exist, aka we're switching from the last state in the list, we catch the exception and set it to the first one again - Love
        _activeState = _statesList.ElementAtOrDefault(_statesList.IndexOf(_activeState) + 1) ?? _statesList[0];
        _activeState.EnterState();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SP_NPC_PreyManager : MonoBehaviour
{
    [SerializeField] GameObject _preyNPCPrefab;
    [SerializeField] GameObject _door1;
    [SerializeField] SP_Hunter_GameManager _gameManager;

    List<PreyNPCTransform> _onStartPrey = new List<PreyNPCTransform>();
    
    int _activePrey;
    int _maxPrey;


    public Text _preyCounter;

    private void Awake()
    {
        GameObject[] tempPreyArray = GameObject.FindGameObjectsWithTag("SP_Prey");
        foreach (GameObject go in tempPreyArray)
        {
            _onStartPrey.Add(new PreyNPCTransform(go.transform.position, go.transform.rotation));
        }
        _activePrey = tempPreyArray.Length;
        _maxPrey = _activePrey;
    }

    public void RemovePrey(GameObject go)
    {
        --_activePrey;
        Destroy(go);
        if (_activePrey <= 0)
        {
            //Don't think this is the best solution, but this allows me to use the same script both for the tutorial and trial
            //As far as I've understood SerializeField is nullable so I'm just leaving either _door1 or _gameManager empty in the editor depending on which scene it's used in - Love
            if (_gameManager != null)
            {
                _preyCounter.text = string.Empty;
                _gameManager.FinishLevel();
                return;
            }
            else  _door1.AddComponent<SP_DoorOpen>();
        }
        _preyCounter.text = (_activePrey).ToString() + " prey left";
    }


    public void RespawnPrey()
    {
        RemoveAllPrey();
        foreach (PreyNPCTransform prey in _onStartPrey)
        {
            Instantiate(_preyNPCPrefab, prey._pos, prey._rot);
        }
        _activePrey = _maxPrey;
        _preyCounter.text = string.Empty;
    }

    public void RemoveAllPrey()
    {
        GameObject[] _tempPreyArray = GameObject.FindGameObjectsWithTag("SP_Prey");
        foreach (GameObject go in _tempPreyArray)
        {
            Destroy(go);
        }
    }
}

public class PreyNPCTransform
{
    public Vector3 _pos;
    public Quaternion _rot;
    public PreyNPCTransform(Vector3 pos, Quaternion rot)
    {
        _pos = pos;
        _rot = rot;
    }
}

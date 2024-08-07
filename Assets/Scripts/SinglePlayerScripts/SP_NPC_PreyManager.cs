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

    List<GameObject> _onStartPrey = new List<GameObject>();
    
    int _activePrey;
    int _maxPrey;


    public Text _preyCounter;

    private void Awake()
    {
        GameObject[] tempPreyArray = GameObject.FindGameObjectsWithTag("SP_Prey");
        foreach (GameObject go in tempPreyArray)
        {
            _onStartPrey.Add(go);
        }
        _activePrey = tempPreyArray.Length;
        _maxPrey = _activePrey;
    }

    public void RemovePrey(GameObject go)
    {
        --_activePrey;
        go.SetActive(false);
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
        foreach (GameObject go in _onStartPrey)
        {
            go.SetActive(true);
        }
        _activePrey = _maxPrey;
        _preyCounter.text = string.Empty;
    }

}

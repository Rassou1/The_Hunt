using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SP_NPC_PreyManager : MonoBehaviour
{
    public GameObject _preyNPC;
    List<GameObject> _onStartPrey = new List<GameObject>();
    public List<GameObject> _activePrey = new List<GameObject>();

    public GameObject _door1;

    public Text _preyCounter;

    private void Awake()
    {
        _onStartPrey = GameObject.FindGameObjectsWithTag("SP_Prey").ToList();
        _activePrey = _onStartPrey;
    }

    public void RemovePrey(GameObject go)
    {
        _activePrey.Remove(go);
        Destroy(go);
        if (_activePrey.Count <= 0)
        {
            _door1.AddComponent<SP_DoorOpen>();
        }
        _preyCounter.text = (_activePrey.Count).ToString() + " prey left";
    }


    public void RespawnPrey()
    {
        RemoveAllPrey();
        _activePrey = _onStartPrey;
        foreach (GameObject go in _activePrey)
        {
            Instantiate(go);
        }
    }

    public void RemoveAllPrey()
    {
        GameObject[] _tempDiamondArray = GameObject.FindGameObjectsWithTag("SP_Prey");
        foreach (GameObject go in _tempDiamondArray)
        {
            Destroy(go);
        }
    }
}

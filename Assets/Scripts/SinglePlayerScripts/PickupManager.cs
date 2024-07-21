using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//This script manages two 
public class PickupManager : MonoBehaviour
{
    public int _neededDiamonds;
    public GameObject _diamondPrefab;
    List<GameObject> _onStartDiamonds = new List<GameObject>();
    public List<GameObject> _activeDiamonds = new List<GameObject>();
    public int _maxDiamonds;

    public GameObject _door1;
    public GameObject _door2;

    public Text _diamondCounter;

    private void Awake()
    {
        _onStartDiamonds = GameObject.FindGameObjectsWithTag("SP_Diamond").ToList();
        _activeDiamonds = _onStartDiamonds;
        _maxDiamonds = _onStartDiamonds.Count;
    }

    public void RemoveDiamond(GameObject go)
    {
        _activeDiamonds.Remove(go);
        if(_maxDiamonds - _activeDiamonds.Count >= _neededDiamonds)
        {
            if(_door1 != null)
            {
                _door1.AddComponent<SP_DoorOpen>();
            }
            if(_door2 != null)
            {
                _door2.AddComponent<SP_DoorOpen>();
            }
        }
        _diamondCounter.text = (_maxDiamonds - _activeDiamonds.Count).ToString() + "/" + _neededDiamonds.ToString() + " diamonds picked up";
    }


    public void RespawnDiamonds()
    {
        RemoveDiamonds();
        _activeDiamonds = _onStartDiamonds;
        foreach(GameObject go in _activeDiamonds)
        {
            Instantiate(go);
        }
    }

    public void RemoveDiamonds()
    {
        GameObject[] _tempDiamondArray = GameObject.FindGameObjectsWithTag("SP_Diamonds");
        foreach (GameObject go in _tempDiamondArray)
        {
            Destroy(go);
        }
    }

}

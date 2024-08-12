using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//This script keeps track of how many diamonds are collected, how many needs collecting, what to do when you collect enough of them as well as updating the diamonds collected text.
//It also handles respawning the diamonds when resetting a scene without reloading it.
//-Love
public class SP_PickupManager : MonoBehaviour
{
    public int _neededDiamonds;
    public GameObject _diamondPrefab;
    List<GameObject> _onStartDiamonds = new List<GameObject>();
    
    int _activeDiamonds;
    int _maxDiamonds;

    public GameObject _door1;
    public GameObject _door2;

    public Text _diamondCounter;

    public bool _exitOpened = false;
    private void Awake()
    {
        GameObject[] tempDiamondArray = GameObject.FindGameObjectsWithTag("SP_Diamond");
        foreach(GameObject go in tempDiamondArray)
        {
            _onStartDiamonds.Add(go);
        }
        _maxDiamonds = tempDiamondArray.Length;
        _activeDiamonds = _maxDiamonds;
    }

    public void RemoveDiamond()
    {
        --_activeDiamonds;
        if(_maxDiamonds - _activeDiamonds >= _neededDiamonds && !_exitOpened)
        {
            _exitOpened = true;
            //I use this script both in the tutorial level which only has one door and in the trial which has 2 doors so I double check
            //that there actually is a door before trying to add any components to avoid any null errors
            //Another solution would have been to just add a second door outside of the map area in the tutorial level, but this felt easier to "set and forget" - Love
            if (_door1 != null)
            {
                _door1.AddComponent<SP_DoorOpen>();
            }
            if(_door2 != null)
            {
                _door2.AddComponent<SP_DoorOpen>();
            }
        }
        _diamondCounter.text = (_maxDiamonds - _activeDiamonds).ToString() + "/" + _neededDiamonds.ToString() + " diamonds picked up";
    }

    public void RespawnDiamonds()
    {
        foreach(GameObject go in _onStartDiamonds)
        {
            go.SetActive(true);
        }
        _activeDiamonds = _maxDiamonds;
        _diamondCounter.text = string.Empty;
    }

}

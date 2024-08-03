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
public class PickupManager : MonoBehaviour
{
    
    public int _neededDiamonds;
    public GameObject _diamondPrefab;
    List<DiamondTransform> _onStartDiamonds = new List<DiamondTransform>();
    
    int _activeDiamonds;
    int _maxDiamonds;

    public GameObject _door1;
    public GameObject _door2;

    public Text _diamondCounter;

    private void Awake()
    {
        GameObject[] tempDiamondArray = GameObject.FindGameObjectsWithTag("SP_Diamond");
        foreach(GameObject go in tempDiamondArray)
        {
            _onStartDiamonds.Add(new DiamondTransform(go.transform.position, go.transform.rotation));
        }
        _maxDiamonds = tempDiamondArray.Length;
        _activeDiamonds = _maxDiamonds;
    }

    public void RemoveDiamond()
    {
        --_activeDiamonds;
        if(_maxDiamonds - _activeDiamonds >= _neededDiamonds)
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
        _diamondCounter.text = (_maxDiamonds - _activeDiamonds).ToString() + "/" + _neededDiamonds.ToString() + " diamonds picked up";
    }


    public void RespawnDiamonds()
    {
        RemoveDiamonds();
        foreach(DiamondTransform diamond in _onStartDiamonds)
        {
            GameObject tempInstantiatedDiamond = Instantiate(_diamondPrefab, diamond._pos, diamond._rot);
            tempInstantiatedDiamond.GetComponent<OnDiamond>()._manager = this;
        }
        _activeDiamonds = _maxDiamonds;
        _diamondCounter.text = "";
    }

    public void RemoveDiamonds()
    {
        GameObject[] _tempDiamondArray = GameObject.FindGameObjectsWithTag("SP_Diamond");
        foreach (GameObject go in _tempDiamondArray)
        {
            Destroy(go);
        }
    }
}

public class DiamondTransform
{
    public Vector3 _pos;
    public Quaternion _rot;
    public DiamondTransform(Vector3 pos, Quaternion rot)
    {
        _pos = pos;
        _rot = rot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{

    [Header("Only Gameplay")]
    public ItemType Type;
    public GameObject Prefab;

    [Header("Only UI")]
    public bool stackable = true;
    

    [Header("Both")]
    public Sprite image;

}

public enum ItemType
{
    Key,
    Valueable,
    Misc
}


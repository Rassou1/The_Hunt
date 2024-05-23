using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerBase : MonoBehaviour
{

    protected bool _caught;
    protected bool _escaped;
    protected int _caughtPrey;
    protected bool _isPrey;

    public bool Escaped { get { return _escaped; } set { _escaped = value; } }
    public bool Caught { get { return _caught; } set { _caught = value; } }
    public int CaughtPrey { get { return _caughtPrey; } }


    public bool IsPrey { get { return _isPrey; } }
}

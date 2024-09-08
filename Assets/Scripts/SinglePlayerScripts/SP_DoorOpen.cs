using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script works the same as the timer in that it is created and destroyed according to need. - Love
public class SP_DoorOpen : MonoBehaviour
{
    private float _moveAmount = 13;
    Vector3 _moveDown = new Vector3(0, 0.005f, 0);
    float _totMoved = 0;
    void Update()
    {
        gameObject.transform.position -= _moveDown;
        _totMoved += _moveDown.y;
        if(_totMoved > _moveAmount)
        {
            Destroy(this);
        }
    }
}

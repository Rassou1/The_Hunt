using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_DoorOpen : MonoBehaviour
{
    Vector3 _moveDown = new Vector3(0, 0.001f, 0);
    float _totMoved = 0;

    

    void Update()
    {
        gameObject.transform.position -= _moveDown;
        _totMoved += _moveDown.y;
        if(_totMoved > 4)
        {
            Destroy(this);
        }
    }
}

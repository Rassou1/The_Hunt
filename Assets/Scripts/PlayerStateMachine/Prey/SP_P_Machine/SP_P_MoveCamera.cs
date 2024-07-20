using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_P_MoveCamera : MonoBehaviour
{
    public Transform _cameraPosition;
    
    void Update()
    {
        transform.position = _cameraPosition.position;
    }
}

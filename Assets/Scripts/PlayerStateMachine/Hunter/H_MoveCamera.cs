using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform cameraBasePosition;
    public Alteruna.Avatar avatar;

    private void Update()
    {
        transform.position = cameraPosition.position;

        if (!avatar.IsMe)
        {

            gameObject.SetActive(false);
        }
    }
}

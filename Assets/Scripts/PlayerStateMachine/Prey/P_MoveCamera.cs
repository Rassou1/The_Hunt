using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
public class P_MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    public Renderer parentRenderer;

    public Alteruna.Avatar avatar;
    void Start()
    {
        
    }

    void Update()
    {
        // Update camera position to match the target position
        if (cameraPosition != null)
        {
            transform.position = cameraPosition.position;
        }

        if (avatar.IsMe)
        {
            parentRenderer.enabled = false;
        }
        else
        {
            parentRenderer.enabled = true;
        }
    }

}

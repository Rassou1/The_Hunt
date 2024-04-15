using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Alteruna;
public class RoleVisuals : MonoBehaviour
{
    private GameObject g;


    public Image roleIndicator;
    // Start is called before the first frame update
    void Start()
    {
        roleIndicator.color = Color.black;
        g = gameObject.transform.root.gameObject;

    }

    // Update is called once per frame
    void Update()
    {

        if ( g.layer == LayerMask.NameToLayer("Prey"))
        {
            Debug.Log("Your prey");

            roleIndicator.color = Color.green;
        }

        if (g.layer == LayerMask.NameToLayer("Hunter"))
        {
            Debug.Log("Your hunter");

            roleIndicator.color = Color.red;
        }
    }

}

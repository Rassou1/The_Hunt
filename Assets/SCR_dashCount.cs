using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SCR_dashCount : MonoBehaviour
{
    public GameObject playerObject;
    private P_StateManager playerScript;

    public TMP_Text dashCount;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = playerObject.GetComponent<P_StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //dashCount.text = $"{playerScript.Pow.AB_dashCharges}X";
    }
}

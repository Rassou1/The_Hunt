using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portraitScript : AttributesSync
{

    public Alteruna.Avatar avatar;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!avatar.IsMe)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

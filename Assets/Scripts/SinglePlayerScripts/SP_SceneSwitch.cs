using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SP_SceneSwitch
{
    public string _sceneName;

    public void SwitchScene()
    {
        SceneManager.LoadScene(_sceneName);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Dash cooldown written by Hamdi.
/// This script links the canvas UI of the dash with its counter for the hunter.
/// </summary>
public class SHunterDashCDUI : MonoBehaviour
{
    public Slider slider;

    [SerializeField]
    GameObject player;

    [SerializeField]
    Outline outline;

    private H_StateManager script;
    private float dash_cooldown;


    // Start is called before the first frame update
    void Start()
    {
        script = player.GetComponent<H_StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        dash_cooldown = script.RemainingDashCooldown;
        slider.value = dash_cooldown;
        if (dash_cooldown == 0)
        {
            outline.enabled = true;
            outline.effectColor = Color.Lerp(outline.effectColor, Color.red, 0.12f);
        }
        else
        {
            outline.enabled = false;
            outline.effectColor = Color.black;
        }
    }
}
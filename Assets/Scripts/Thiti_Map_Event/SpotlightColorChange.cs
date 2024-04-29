using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightColorChange : MonoBehaviour
{

    // Reference to the spotlight component
    private Light spotlight;

    // The color to change to upon collision
    public Color collisionColor = Color.red;

    // Start is called before the first frame update


    void Start()
    {
        spotlight = GetComponent<Light>();
    }

    void OnTriggerEnter(Collider Player)
    {
        // Check if the collider's GameObject has the tag you want to interact with
        if (Player.CompareTag("baseGround"))
        {
            // Change the color of the spotlight to the collision color
            spotlight.color = collisionColor;
        }
    }
}

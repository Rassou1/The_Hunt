using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code should be ignore because it was Thitiwich´s attempt to make a "pit"
// By changing the spotlight´s color to indicate that players have been fallen into the pit.

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

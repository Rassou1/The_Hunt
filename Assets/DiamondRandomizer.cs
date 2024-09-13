using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondRandomizer : MonoBehaviour
{
    private List<GameObject> diamondVariants = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        // Find all children with names that start with "Diamonds Variant"
        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("Diamonds Variant"))
            {
                diamondVariants.Add(child.gameObject);
            }
        }

        // Randomize the list and activate 60% of them
        ActivateRandomSet(0.6f);
    }

    // Function to activate a random set of objects based on the percentage
    void ActivateRandomSet(float percentage)
    {
        // Shuffle the list of diamond variants
        ShuffleList(diamondVariants);

        // Calculate the number of diamonds to activate (60%)
        int numberToActivate = Mathf.FloorToInt(diamondVariants.Count * percentage);

        // Activate only the random 60% and deactivate the rest
        for (int i = 0; i < diamondVariants.Count; i++)
        {
            if (i < numberToActivate)
            {
                diamondVariants[i].SetActive(true); // Activate
            }
            else
            {
                diamondVariants[i].SetActive(false); // Deactivate
            }
        }
    }

    // Fisher-Yates shuffle to randomize the list
    void ShuffleList(List<GameObject> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

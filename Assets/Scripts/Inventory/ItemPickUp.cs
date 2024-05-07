using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item ItemType;
    bool playerExists = false;
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") && inventoryManager != null)
        {
            playerExists = true;
        }

        if (playerExists && Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position) < 1.75)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                bool result = inventoryManager.AddItem(ItemType);
                if (result == true)
                {
                    Debug.Log("Item Added");
                    Object.Destroy(gameObject);

                }
                else
                {
                    Debug.Log("Inventory Full");
                }
            }
        }
    }
}

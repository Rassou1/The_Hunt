using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxStackedItems = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 9)
            {
                ChangeSelectedSlot(number - 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && inventorySlots[selectedSlot] != null)
        {
            InventoryItem temp = inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();
            Instantiate(temp.item.Prefab, GameObject.FindWithTag("Player").transform.position, GameObject.FindWithTag("Player").transform.rotation);

        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        //Check if item has space for stacking
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && item.stackable == true && itemInSlot.item == item && itemInSlot.count < MaxStackedItems)
            {

                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        //Finds an empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item,InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    void RemoveItem(Item item)
    {
        //Add so item gets removed when dropped
    }
}

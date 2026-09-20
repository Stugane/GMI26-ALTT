using UnityEngine;
using System.Collections.Generic;

public class AInventory : MonoBehaviour
{
    public AItemSO loot1;
    public AItemSO loot2;

    public GameObject hotbarObj;
    public GameObject inventorySlotParent;

    private List<ASlot> inventorySlots = new List<ASlot>();
    private List<ASlot> hotbarSlots = new List<ASlot>();
    private List<ASlot> allSlots = new List<ASlot>();

    private void Awake()
    {
        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<ASlot>());
        hotbarSlots.AddRange(hotbarObj.GetComponentsInChildren<ASlot>());

        allSlots.AddRange(inventorySlots);
        allSlots.AddRange(hotbarSlots);

        Debug.Log("Inventory slots: " + inventorySlots.Count);
        Debug.Log("Hotbar slots: " + hotbarSlots.Count);
        Debug.Log("All slots: " + allSlots.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddItem(loot1, 3);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            AddItem(loot2, 1);
        }
    }

    public void AddItem(AItemSO itemToAdd, int amount)
    {
        int remaining = amount;

        foreach(ASlot slot in allSlots)
        {
            if(slot.HasItem() && slot.GetItem() == itemToAdd)
            {
                int currentAmount = slot.GetAmount();
                int maxStack = itemToAdd.maxStackSize;

                if(currentAmount < maxStack)
                {
                    int spaceLeft = maxStack - currentAmount;
                    int amountToAdd = Mathf.Min(spaceLeft, remaining);

                    slot.SetItem(itemToAdd, currentAmount + amountToAdd);
                    remaining -= amountToAdd;

                    if(remaining <= 0)
                        return;
                }
            }
        }

        foreach(ASlot slot in allSlots)
        {
            if (!slot.HasItem())
            {
                int amountToPlace = Mathf.Min(itemToAdd.maxStackSize, remaining);
                slot.SetItem(itemToAdd, amountToPlace);
                remaining -= amountToPlace;

                if (remaining <= 0)
                    return;
            }
        }

        if(remaining > 0)
        {
            Debug.Log("Inventory is full, couldn't add " + remaining + " of " + itemToAdd.itemName);
        }
    }
}

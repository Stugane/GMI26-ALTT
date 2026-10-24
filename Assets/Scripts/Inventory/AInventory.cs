using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class AInventory : MonoBehaviour
{
    public AItemSO loot1;
    public AItemSO loot2;

    public GameObject hotbarObj;
    public GameObject inventorySlotParent;
    public GameObject container;

    public Image dragIcon;

    public float pickupRange = 4;
    private AItem lookedAtItem = null;
    public Material highlightMaterial;
    private Material originalMaterial;
    private Renderer lookedAtRenderer = null;

    private List<ASlot> inventorySlots = new List<ASlot>();
    private List<ASlot> hotbarSlots = new List<ASlot>();
    private List<ASlot> allSlots = new List<ASlot>();

    private ASlot draggedSlot = null;
    private bool isDragging = false;

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
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddItem(loot1, 3);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            AddItem(loot2, 1);
        }
        */

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            container.SetActive(!container.activeInHierarchy);
            //Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
            //Cursor.visible = !Cursor.visible;
        }

        DetectLookedAtItem();
        Pickup();

        StartDrag();
        UpdateDragItemDestination();
        EndDrag();
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

    private void StartDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ASlot hovered = GetHoveredSlot();

            if(hovered != null && hovered.HasItem())
            {
                draggedSlot = hovered;
                isDragging = true;

                //Show drag item
                dragIcon.sprite = hovered.GetItem().icon;
                dragIcon.color = new Color(1, 1, 1, 0.5f);
                dragIcon.enabled = true;
            }
        }
    }

    private void EndDrag()
    {
        if(Input.GetMouseButtonUp(0) && isDragging)
        {
            ASlot hovered = GetHoveredSlot();

            if(hovered != null)
            {
                HandleDrop(draggedSlot, hovered);

                dragIcon.enabled = false;

                draggedSlot = null;
                isDragging = false;
            }
        }
    }

    private ASlot GetHoveredSlot()
    {
        foreach(ASlot s in allSlots)
        {
            if (s.hovering)
                return s;
        }

        return null;
    }

    private void HandleDrop(ASlot from, ASlot to)
    {
        if(from == to) return;


        //Stacking
        if(to.HasItem() && to.GetItem() == from.GetItem())
        {
            int max = to.GetItem().maxStackSize;
            int space = max - to.GetAmount();

            if(space > 0)
            {
                int move = Mathf.Min(space, from.GetAmount());

                to.SetItem(to.GetItem(), to.GetAmount() + move);
                from.SetItem (from.GetItem(), from.GetAmount() - move);

                if(from.GetAmount() <= 0)
                    from.ClearSlot();

                return;
            }
        }

        //Different Item
        if (to.HasItem())
        {
            AItemSO tempItem = to.GetItem();
            int tempAmount = to.GetAmount();

            to.SetItem(from.GetItem(), from.GetAmount());
            from.SetItem(tempItem, tempAmount);
            return;
        }

        //Empty Slot
        to.SetItem(from.GetItem(), from.GetAmount());
        from.ClearSlot();
    }

    private void UpdateDragItemDestination()
    {
        if (isDragging)
        {
            dragIcon.transform.position = Input.mousePosition;
        }
    }

    private void Pickup()
    {
        if(lookedAtRenderer != null && Input.GetKeyDown(KeyCode.E))
        {
            AItem item = lookedAtRenderer.GetComponent<AItem>();
            if (item != null)
            {
                AddItem(item.item, item.amount);
                Destroy(item.gameObject);
            }
        }
    }

    private void DetectLookedAtItem()
    {
        if(lookedAtRenderer != null)
        {
            lookedAtRenderer.material = originalMaterial;
            lookedAtRenderer = null;
            originalMaterial = null;
        }

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            AItem item = hit.collider.GetComponent<AItem>();
            if (item != null)
            {
                Renderer rend = item.GetComponent<Renderer>();
                if(rend != null)
                {
                    originalMaterial = rend.material;
                    rend.material = highlightMaterial;
                    lookedAtRenderer = rend;
                }
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int maxSlots = 30;
    public List<InventorySlot> slots = new();

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedCallback;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Multiple inventories found!");
            return;
        }
        Instance = this;
    }

    public bool AddItem(Item item, int quantity = 1)
    {
        if (item == null) return false;

        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.quantity += quantity;
                onInventoryChangedCallback?.Invoke();
                return true;
            }
        }

        if (slots.Count < maxSlots)
        {
            slots.Add(new InventorySlot(item, quantity));
            onInventoryChangedCallback?.Invoke();
            return true;
        }

        Debug.Log("Inventory is full!");
        return false;
    }

    public void RemoveItem(Item item, int quantity = 1)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].quantity -= quantity;
                if (slots[i].quantity <= 0)
                    slots.RemoveAt(i);

                onInventoryChangedCallback?.Invoke();
                return;
            }
        }
    }

    public bool HasItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
                return true;
        }
        return false;
    }
}

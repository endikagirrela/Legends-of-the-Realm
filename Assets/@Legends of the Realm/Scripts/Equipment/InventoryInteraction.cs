using UnityEngine;

public class InventoryInteraction : MonoBehaviour
{
    public void TryEquip(Item item)
    {
        if (item is EquipmentItem equipmentItem)
        {
            EquipmentManager manager = FindObjectOfType<EquipmentManager>();
            manager.EquipItem(equipmentItem);
            Inventory.Instance.RemoveItem(item);
        }
    }
}

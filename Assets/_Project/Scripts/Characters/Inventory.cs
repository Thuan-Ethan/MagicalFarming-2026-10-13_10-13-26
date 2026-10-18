using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public event System.Action OnChanged;

    [System.Serializable]
    public class Slot
    {
        public CollectableType type;
        public int count;
        public int maxAllowed;
        public Sprite itemIcon;

        public Slot()
        {
            type = CollectableType.NONE;
            count = 0;
            maxAllowed = 99;
        }

        public bool CanAddItem()
        {
            if (count < maxAllowed)
            {
                return true;
            }
            return false;
        }

        public void AddItem(Collectable item)
        {
            this.type = item.type;
            this.itemIcon = item.itemIcon;
            count++;
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public bool Add(Collectable itemToAdd)
    {
        // Ưu tiên thêm vào slot đang chứa cùng loại vật phẩm.
        foreach (Slot slot in slots)
        {
            if (slot.type == itemToAdd.type && slot.CanAddItem())
            {
                slot.AddItem(itemToAdd);
                OnChanged?.Invoke();
                return true;
            }
        }

        // Nếu không có slot cùng loại, tìm slot trống.
        foreach (Slot slot in slots)
        {
            if (slot.type == CollectableType.NONE)
            {
                slot.AddItem(itemToAdd);
                OnChanged?.Invoke();
                return true;
            }
        }

        // Inventory đầy, không thêm được vật phẩm.
        return false;
    }
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot  // Made Slot public if you need to access it elsewhere
    {
        [SerializeField] private ItemData _itemData; // Changed to ItemData for clarity
        [SerializeField] private Item _item;
        public ItemData itemData
        {
            get { return _itemData; } // Property to access item data
            private set { _itemData = value; } // Private setter to prevent external modification
        }
        [SerializeField]private int _quantity;
        public int quantity
        {
            get { return _quantity; } // Property to access quantity
            private set { _quantity = value; } // Private setter to prevent external modification
        }
        private int _max;

        public Slot()
        {
            _quantity = 0;
            _max = 99;
            _itemData = null;
        }

        public bool isEmpty()
        {
            return _itemData == null;
        }

        public bool CanAdd(ItemData addedItemData)
        {
            if (_itemData!=null && _itemData.itemName == addedItemData.itemName && quantity > 0)
            {
                return true;
            }
            return false;
        }

        public void AddItem(Item item)
        {
            _itemData = item.data; // Assuming Item has a data property of type ItemData
            _quantity++;
        }

        public void AddItem(Slot slot)
        {

            _itemData = slot.itemData; // Assuming Item has a data property of type ItemData
            _quantity = slot.quantity;
            _max = slot._max;
        }

        public void removeItem()
        {
            if (quantity > 0)
            {
                quantity--;

                if (quantity == 0)
                {
                    _itemData = null;
                }
            }

        }

    }

    [SerializeField] private List<Slot> _slots = new List<Slot>(); // Added SerializeField for Inspector visibility

    public List<Slot> slots
    {
        get { return _slots; } // Property to access slots
        private set { _slots = value; } // Private setter to prevent external modification
    }

    public Inventory(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _slots.Add(new Slot());
        }
    }

    // Inventory.cs
    public void Add(Item addedItem)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.itemData == addedItem.data && slot.CanAdd(addedItem.data))
            {
                slot.AddItem(addedItem);
                Debug.Log($"Added {addedItem.data.itemName} to existing stack. Quantity: {slot.quantity}");
                Debug.Log(_slots);
                return;
            }
        }

        foreach (Slot slot in _slots)
        {
            if (slot.itemData == null)
            {
                slot.AddItem(addedItem);
                Debug.Log($"Added {addedItem.data.itemName} to new slot. Quantity: {slot.quantity}");
                return;
            }
        }

        Debug.LogWarning("Inventory is full!");
    }


    public ItemData remove(int index)
    {
        ItemData item = _slots[index].itemData;

        Debug.Log($"Removing item {item.itemName} from slot {index}. Quantity before removal: {_slots[index].quantity}");
        _slots[index].removeItem();
        Debug.Log($"Quantity after removal: {_slots[index].quantity}");

        //_slots[index].removeItem();
        return item;
    }

    public void moveSlot(int draggingSlotIndex, int destinationSlotIndex, Inventory draggingSlotInventory)
    {
        Debug.Log("Test");
        Slot draggingSlot = _slots[draggingSlotIndex];
        Slot destinationSlot = draggingSlotInventory._slots[destinationSlotIndex];
        Debug.Log(draggingSlot.isEmpty());
        Debug.Log(destinationSlot.CanAdd(draggingSlot.itemData));

        if (destinationSlot.isEmpty() || destinationSlot.CanAdd(draggingSlot.itemData))
        {
            Debug.Log("Test2");
            destinationSlot.AddItem(draggingSlot);
            int quantity = draggingSlot.quantity;
            for (int i = 0; i < quantity; i++)
            {
                draggingSlot.removeItem();
            }
        }
    }
}
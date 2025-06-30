using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot  // Made Slot public if you need to access it elsewhere
    {
        [SerializeField] private ItemData _itemData; // Changed to ItemData for clarity
        public ItemData itemData
        {
            get { return _itemData; } // Property to access item data
            private set { _itemData = value; } // Private setter to prevent external modification
        }
        private int _quantity;
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

        public bool CanAdd() => _quantity < _max; // Simplified with expression body

        public void AddItem(Item item)
        {

            _itemData = item.data; // Assuming Item has a data property of type ItemData
            _quantity++;
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
            if (slot.itemData == addedItem.data && slot.CanAdd())
            {
                slot.AddItem(addedItem);
                Debug.Log($"Added {addedItem.data.itemName} to existing stack. Quantity: {slot.quantity}");
                Debug.Log(_slots);
                return;
            }
        }

        foreach (Slot slot in _slots)
        {
            if (slot.itemData==null)
            {
                slot.AddItem(addedItem);
                Debug.Log($"Added {addedItem.data.itemName} to new slot. Quantity: {slot.quantity}");
                return;
            }
        }

        Debug.LogWarning("Inventory is full!");
    }


    public void remove(int index)
    {
        _slots[index].removeItem();


    }
}
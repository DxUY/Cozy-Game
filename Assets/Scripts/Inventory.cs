using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot  // Made Slot public if you need to access it elsewhere
    {
        private string _itemName; // Renamed to follow C# conventions
        public string itemName
        {
            get { return _itemName; } // Property to access item name
            private set { _itemName = value; } // Private setter to prevent external modification
        }
        private Sprite _icon; // Added icon for UI representation
        public Sprite icon
        {
            get { return _icon; } // Property to access icon
            private set { _icon = value; } // Private setter to prevent external modification
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
            _itemName = "";
        }

        public bool CanAdd() => _quantity < _max; // Simplified with expression body

        public void AddItem(Item item)
        {
            
            _itemName = item.data.itemName; // Set item name from the item data
            _icon = item.data.itemIcon; // Set icon from the item
            _quantity++;
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
            if (slot.itemName == addedItem.data.itemName && slot.CanAdd())
            {
                slot.AddItem(addedItem);
                Debug.Log($"Added {addedItem.data.itemName} to existing stack. Quantity: {slot.quantity}");
                Debug.Log(_slots);
                return;
            }
        }

        foreach (Slot slot in _slots)
        {
            if (slot.itemName == "")
            {
                slot.AddItem(addedItem);
                Debug.Log($"Added {addedItem.data.itemName} to new slot. Quantity: {slot.quantity}");
                return;
            }
        }

        Debug.LogWarning("Inventory is full!");
    }

    public List<Slot> GetSlots() => _slots;
}
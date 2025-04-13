using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot  // Made Slot public if you need to access it elsewhere
    {
        public CollectableType _type; // Renamed to follow C# conventions
        private int _quantity;
        private int _max;

        public Slot()
        {
            _quantity = 0;
            _max = 99;
            _type = CollectableType.NONE;
        }

        public bool CanAdd() => _quantity < _max; // Simplified with expression body

        public void AddItem(CollectableType type)
        {
            _type = type;
            _quantity++;
        }

        // Optional: expose quantity for debugging/inspection
        public int Quantity => _quantity;
    }

    [SerializeField] private List<Slot> _slots = new List<Slot>(); // Added SerializeField for Inspector visibility

    public Inventory(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _slots.Add(new Slot());
        }
    }

    // Inventory.cs
    public void Add(CollectableType addedType)
    {
        foreach (Slot slot in _slots)
        {
            if (slot._type == addedType && slot.CanAdd())
            {
                slot.AddItem(addedType);
                Debug.Log($"Added {addedType} to existing stack. Quantity: {slot.Quantity}");
                Debug.Log(_slots);
                return;
            }
        }

        foreach (Slot slot in _slots)
        {
            if (slot._type == CollectableType.NONE)
            {
                slot.AddItem(addedType);
                Debug.Log($"Added {addedType} to new slot. Quantity: {slot.Quantity}");
                return;
            }
        }

        Debug.LogWarning("Inventory is full!");
    }

    public List<Slot> GetSlots() => _slots;
}
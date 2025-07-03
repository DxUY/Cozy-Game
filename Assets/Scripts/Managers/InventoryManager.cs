using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, Inventory> _inventoryDict = new Dictionary<string, Inventory>();

    [SerializeField] private Inventory _backpack;
    [SerializeField] private int _backpackSlots;
    [SerializeField] private Inventory _toolbar;
    [SerializeField] private int _toolBarSlot;

    public Inventory backpack
    {
        get { return _backpack; }
        set{ _backpack = value; }
    }

    public Inventory toolBar
    {
        get { return _toolbar; }
        set{ _toolbar = value; }
    }

    void Awake()
    {
        _backpack = new Inventory(_backpackSlots);
        _toolbar = new Inventory(_toolBarSlot);

        _inventoryDict.Add("Backpack", _backpack);
        _inventoryDict.Add("ToolBar", _toolbar);

    }

    public Inventory getByName(string inventoryName)
    {
        Debug.Log(inventoryName);

        

        if (_inventoryDict.ContainsKey(inventoryName))
        {

            return _inventoryDict[inventoryName];
        }
        return null;
    }
}

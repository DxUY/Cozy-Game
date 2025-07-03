using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory_UI : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    [SerializeField] private List<Slot_UI> slots = new List<Slot_UI>();
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private string _inventoryName;
    [SerializeField] private UI_Manager _uiManager;


    public string inventoryName
    {
        get { return _inventoryName; }
        set { _inventoryName = value; }
    }

    public Inventory inventory
    {
        get { return _inventory; }
        set { _inventory = value; }
    }

    public List<Slot_UI> Slots
    {

        get { return slots; } 
    }

    void Start()
    {
        

        if (player.inventoryManager == null)
        {
            Debug.LogError("inventoryManager is not assigned in the PlayerScript component.");
            return;
        }

        if (string.IsNullOrEmpty(_inventoryName))
        {
            Debug.LogError("_inventoryName is not set in the Inspector for Inventory_UI. It should be 'Backpack' or 'ToolBar'.");
            return;
        }

        Debug.Log($"_inventoryName: {_inventoryName}");
        _inventory = player.inventoryManager.getByName(_inventoryName);
        Debug.Log(_inventory);
        if (_inventory == null)
        {
            Debug.LogError($"No inventory found for name: {_inventoryName}. Ensure it matches 'Backpack' or 'ToolBar'.");
        }

        setUp();
    }

    public void setUp()
    {
        if (slots.Count == _inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].slotId = i; // Ensure each slot has a unique ID
                slots[i].inventory = _inventory;
                if (_inventory.slots[i].itemData != null)
                {
                    slots[i].SetItem(_inventory.slots[i]);
                }
                else
                {
                    slots[i].setEmpty();
                }
            }
        }

    }

    public void removeItem()
    {
        int quantity= UI_Manager._draggingSlot.quantityText.text != null ? int.Parse(UI_Manager._draggingSlot.quantityText.text) : 0;
        Vector3 playerPosition = player.transform.position;
        float randX = Random.Range(-4f, 4f);
        float randY = Random.Range(-4f, 4f);

        for (int i = 0; i < quantity; i++)
        {
            ItemData droppedItem = _inventory.remove(UI_Manager._draggingSlot.slotId);
            Instantiate(droppedItem.itemPrefab, playerPosition + new Vector3(randX, randY, 0), Quaternion.identity);
        }
        
        //Debug.Log("Removed item: " + droppedItem.itemName + " from slot: " + _draggingSlot.slotId);
        setUp();

        //Debug.Log("Dropped item: " + droppedItem.itemName + " at position: " + (playerPosition + new Vector3(randX, randY, 0)));
    }

    public void slotBeginDrag(Slot_UI slot)
    {
        UI_Manager._draggingSlot = slot;
        UI_Manager._draggingIcon = Instantiate(slot.icon);
        UI_Manager._draggingIcon.transform.SetParent(_canvas.transform);
        UI_Manager._draggingIcon.raycastTarget = false; // Disable raycast to allow clicks to pass through
        UI_Manager._draggingIcon.rectTransform.sizeDelta = new Vector2(70, 70); // Set size for visibility
        moveToMousePosition(UI_Manager._draggingIcon.gameObject);
        Debug.Log("Begin Drag Slot: " + slot.name );
    }

    public void slotDrag(Slot_UI slot)
    {
        moveToMousePosition(UI_Manager._draggingIcon.gameObject);

        //Debug.Log("Dragging Slot: " + slot.name);
    }


    public void slotEndDrag(Slot_UI slot)
    {
        Destroy(UI_Manager._draggingIcon.gameObject);
        UI_Manager._draggingIcon = null;
        Debug.Log("End Drag Slot: " + slot.name);

    }

    public void slotDrop(Slot_UI slot)
    {
        Debug.Log(UI_Manager._draggingSlot);
        Debug.Log("Testing: "+ ( slot.inventory));
        UI_Manager._draggingSlot.inventory.moveSlot(UI_Manager._draggingSlot.slotId, slot.slotId, slot.inventory);
        _uiManager.setUpAllInventory();
        Debug.Log("Dropped Slot: " + slot.name);
    }

    public void moveToMousePosition(GameObject toMove)
    {
        if (_canvas != null)
        {
            Vector2 mousePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                null,
                out mousePosition
            );

            toMove.transform.position = _canvas.transform.TransformPoint(mousePosition);
        }
    }
}

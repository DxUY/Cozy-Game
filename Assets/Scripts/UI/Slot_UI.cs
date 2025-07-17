using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_UI : MonoBehaviour
{
    [SerializeField] private int _slotId;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private string _itemName;
    [SerializeField] private GameObject _highlight;

    public int slotId
    {
        get { return _slotId; }
        set { _slotId = value; }
    }

    public Inventory inventory
    {
        get { return _inventory; }
        set { _inventory = value; }
    }

    public string itemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }

    public Image icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    public TextMeshProUGUI quantityText
    {
        get { return _quantityText; }
        set { _quantityText = value; }
    }




    public void SetItem(Inventory.Slot slot)
    {
        _icon.sprite = slot.itemData.itemIcon; // Assuming itemData has a property itemIcon of type Sprite
        _itemName = slot.itemData.itemName;
        _icon.color = new Color(1, 1, 1, 1); // Set icon color to white for visibility
        _quantityText.text = slot.quantity.ToString();
    }

    public void setEmpty()
    {
        _icon.sprite = null; // Clear the icon
        _icon.color = new Color(1, 1, 1, 0); // Set icon color to transparent
        _quantityText.text = "0"; // Reset quantity text
    }
    
    public void setHighLight(bool isActive)
    {
        _highlight.SetActive(isActive);
    }
}

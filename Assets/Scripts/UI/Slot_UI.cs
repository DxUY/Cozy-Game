using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_UI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quantityText;

    [SerializeField] private GameObject _highlight;

    public void SetItem(Inventory.Slot slot)
    {
        _icon.sprite = slot.icon;
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

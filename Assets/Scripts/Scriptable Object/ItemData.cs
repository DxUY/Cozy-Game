using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField]private string _itemName;
    public string itemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }
    [SerializeField] private Sprite _itemIcon;
    public Sprite itemIcon
    {
        get { return _itemIcon; }
        set { _itemIcon = value; }
    }
}

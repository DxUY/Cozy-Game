using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _itemIcon;

    [SerializeField] private GameObject _itemPrefab;

    public string itemName
    {
        get { return _itemName; }
        set { _itemName = value; }
    }
    public Sprite itemIcon
    {
        get { return _itemIcon; }
        set { _itemIcon = value; }
    }
    
    public GameObject itemPrefab
    {
        get { return _itemPrefab; }
        set { _itemPrefab = value; }
    }
}

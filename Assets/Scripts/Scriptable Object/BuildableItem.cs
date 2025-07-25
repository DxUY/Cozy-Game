using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "BuildableItem", menuName = "Scriptable Objects/BuildableItem")]
public class BuildableItem : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private TileBase _tile;
    [SerializeField] private Vector3 _tileOffset;
    [SerializeField] private Sprite _previewSprite;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private GameObject _gameObject;

    public string Name => _name;
    public TileBase Tile => _tile;
    public Vector3 TileOffset => _tileOffset;
    public Sprite PreviewSprite => _previewSprite;
    public Sprite UiIcon => _uiIcon;
    public GameObject GameObject => _gameObject;


}

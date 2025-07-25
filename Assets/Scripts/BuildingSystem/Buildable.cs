using UnityEngine;
using UnityEngine.Tilemaps;

public class Buildable
{
    [SerializeField] private Tilemap _parentTileMap;
    [SerializeField] private BuildableItem _buildableItem;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Vector3 _coords;

    public Buildable(Tilemap parentTileMap, BuildableItem buildableItem, Vector3 coords, GameObject gameObject=null)
    {
        _parentTileMap = parentTileMap;
        _buildableItem = buildableItem;
        _gameObject = gameObject;
        _coords = coords;
    }
}

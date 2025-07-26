using UnityEngine;
using UnityEngine.Tilemaps;
using Extensions;

public class Buildable
{
    [SerializeField] private Tilemap _parentTileMap;
    [SerializeField] private BuildableItem _buildableItem;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Vector3Int _coords;

    public BuildableItem BuildableItem => _buildableItem;

    public Buildable(Tilemap parentTileMap, BuildableItem buildableItem, Vector3Int coords, GameObject gameObject = null)
    {
        _parentTileMap = parentTileMap;
        _buildableItem = buildableItem;
        _gameObject = gameObject;
        _coords = coords;
    }

    public void Destroy()
    {
        if (_gameObject != null)
        {
            Object.Destroy(_gameObject);
        }

        _parentTileMap.SetTile(_coords, null);
    }

    public void IterateCollisionSpace(RectIntExtension.RectAction action)
    {
        _buildableItem.CollisionSpace.Iterate(_coords, action);
    }

    public bool IterateCollisionSpace(RectIntExtension.RectActionBool action)
    {
        return _buildableItem.CollisionSpace.Iterate(_coords, action);
    }
}

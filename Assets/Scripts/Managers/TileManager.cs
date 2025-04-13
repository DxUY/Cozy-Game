using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap _groundTileMap;
    [SerializeField] private Tile _plowedTile;

    void Start()
    {
        GameObject groundObject = GameObject.FindWithTag("Ground");
        if (groundObject != null)
        {
            _groundTileMap = groundObject.GetComponent<Tilemap>();
            if (_groundTileMap == null)
            {
                Debug.LogError("No Tilemap component found on GameObject with tag 'Ground'!");
            }
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Ground' found in the scene!");
        }

        if (_plowedTile == null)
        {
            Debug.LogError("Plowed Tile not assigned in Inspector!");
        }
    }

    private void OnEnable()
    {
        EventBus.GetTileAvailable += isAvailable;
        EventBus.Plowed += setPlowed;
    }

    private void OnDisable()
    {
        EventBus.GetTileAvailable -= isAvailable;
        EventBus.Plowed -= setPlowed;
    }

    public bool isAvailable(Vector3 worldPosition) // Changed to Vector3
    {
        if (_groundTileMap == null) return false;

        Vector3Int tilePosition = _groundTileMap.WorldToCell(worldPosition);
        TileBase tile = _groundTileMap.GetTile(tilePosition);
        Debug.Log($"Player World Pos: {worldPosition}, Tile Pos: {tilePosition}");

        if (tile != null)
        {
            if (tile.name == "grass_tileset_16x16_24")
            {
                Debug.Log("true");
                return true;
                
            }
        }
        Debug.Log(tile.name);
        Debug.Log("false");
        return false;
    }

    public void setPlowed(Vector3 worldPosition) // Changed to Vector3
    {
        if (_groundTileMap == null || _plowedTile == null) return;

        Vector3Int tilePosition = _groundTileMap.WorldToCell(worldPosition);
        Debug.Log($"Plowed tile at: {tilePosition}");
        _groundTileMap.SetTile(tilePosition, _plowedTile);
    }

    // Optional: Expose Tilemap for PlayerScript if needed
    public Tilemap GetGroundTilemap() => _groundTileMap;
}
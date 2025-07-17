using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ChestTile", menuName = "Tiles/ChestTile")]
public class ChestTile : InteractableTile
{
    [SerializeField] private Vector3Int position; // Lưu vị trí của tile trong lưới Tilemap

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        this.position = position; // Lưu vị trí của tile
        tileData.sprite = this.sprite;
    }
    public override void Interact(GameObject interactingObject)
    {
        Debug.Log($"Clicked on {TileName}: {position}"); // Hành vi khi nhấp vào rương
        // Ví dụ: Mở giao diện rương
    }
}
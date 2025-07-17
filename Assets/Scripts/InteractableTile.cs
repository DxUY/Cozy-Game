using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class InteractableTile : Tile, IInteractables
{
    public string TileName;
    public string Description;

    public abstract void Interact(GameObject interactingObject); // Phương thức trừu tượng

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.sprite = this.sprite;
    }
}

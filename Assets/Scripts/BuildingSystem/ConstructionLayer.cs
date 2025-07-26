using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConstructionLayer : MonoBehaviour
{

    [SerializeField] private Tilemap _constructionTileMap;
    [SerializeField] private Dictionary<Vector3Int, Buildable> _builadbles = new Dictionary<Vector3Int, Buildable>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnEnable()
    {

        EventBus.Build += build;
        EventBus.IsEmpty += isEmpty;
        EventBus.Destroy += destroy;
    }

    private void OnDisable()
    {

        EventBus.Build -= build;
        EventBus.IsEmpty -= isEmpty;
        EventBus.Destroy -= destroy;
    }


    public void build(Vector3 worldPosition, BuildableItem buildableItem)
    {
        Debug.Log("Building");
        var coords = _constructionTileMap.WorldToCell(worldPosition);
        GameObject itemObject = null;
        if (buildableItem.Tile != null && isEmpty(worldPosition))
        {
            _constructionTileMap.SetTile(coords, buildableItem.Tile);

        }
        else if (buildableItem.GameObject != null && isEmpty(worldPosition))
        {
            itemObject = Instantiate(buildableItem.GameObject, _constructionTileMap.GetCellCenterWorld(coords), Quaternion.identity);
        }
        var buildable = new Buildable(_constructionTileMap, buildableItem, coords, itemObject);
        if (buildableItem.UseCustomCollisionSpace)
        {
            EventBus.SetBuilableCollision?.Invoke(buildable, true);
            registerBuildableCollisionSpace(buildable);
        }
        else
        {
            _builadbles[coords] = buildable;
        }

    }

    public void destroy(Vector3 worldPosition)
    {
        var coords = _constructionTileMap.WorldToCell(worldPosition);
        if (!_builadbles.ContainsKey(coords)) return;

        var buildable = _builadbles[coords];
        if (buildable.BuildableItem.UseCustomCollisionSpace)
        {
            EventBus.SetBuilableCollision?.Invoke(buildable, false);
            unregisterBuildableCollisionSpace(buildable);
        }
        _builadbles.Remove(coords);
        buildable.Destroy();
    }

    public bool isEmpty(Vector3 worldPosition, RectInt rect = default)
    {
        var coords = _constructionTileMap.WorldToCell(worldPosition);
        if (!rect.Equals(default))
        {
            return !isRectOccipied(coords, rect);
        }
        return !_builadbles.ContainsKey(_constructionTileMap.WorldToCell(worldPosition)) && _constructionTileMap.GetTile(coords) == null;
    }

    public bool isRectOccipied(Vector3Int coords, RectInt rect)
    {
        return rect.Iterate(coords, (tileCoords) => _builadbles.ContainsKey(tileCoords));
    }

    public void registerBuildableCollisionSpace(Buildable buildable)
    {
        buildable.IterateCollisionSpace((coords) => _builadbles[coords] = buildable);
    }

    public void unregisterBuildableCollisionSpace(Buildable buildable)
    {
        buildable.IterateCollisionSpace((coords) =>
        {
            _builadbles.Remove(coords);
        });
    }
    

}

using System.Collections.Generic;
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

    }

    private void OnDisable()
    {

        EventBus.Build -= build;
        EventBus.IsEmpty -= isEmpty;
    }


    public void build(Vector3 worldPosition, BuildableItem buildableItem)
    {
        Debug.Log("Building");
        var coords = _constructionTileMap.WorldToCell(worldPosition);
        if (buildableItem.Tile != null)
        {
            _constructionTileMap.SetTile(coords, buildableItem.Tile);
            var buildable = new Buildable(_constructionTileMap, buildableItem, coords);
            _builadbles[coords] = buildable;
        }
    }

    public bool isEmpty(Vector3 worldPosition)
    {
        return !_builadbles.ContainsKey(_constructionTileMap.WorldToCell(worldPosition));
    }
}

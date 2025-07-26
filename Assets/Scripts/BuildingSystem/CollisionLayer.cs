using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollisionLayer : MonoBehaviour
{
    [SerializeField] private Tilemap _collisionTileMap;
    [SerializeField] private TileBase _collsionTile;

     private void OnEnable()
    {

        EventBus.SetBuilableCollision += setCollision;
       
    }

    private void OnDisable()
    {

        EventBus.SetBuilableCollision -= setCollision;
        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCollision(Buildable buildable, bool value)
    {
        var tile = value ? _collsionTile : null;
        buildable.IterateCollisionSpace((coords)=>
        {
            _collisionTileMap.SetTile(coords, tile);
        });
    }
}

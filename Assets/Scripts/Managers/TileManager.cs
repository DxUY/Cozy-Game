using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap _groundTileMap;
    [SerializeField] private Tile _plowedTile;
    [SerializeField] private Tile _soiledTile;
    [SerializeField] private Tilemap _secondLayer;
    [SerializeField] private Tilemap _constructionTileMap;

    [SerializeField] private Dictionary<Vector3Int, PlantedCrop> _plantedTiles = new Dictionary<Vector3Int, PlantedCrop>();
    [SerializeField] private Dictionary<Vector3Int, IInteractables> _interactables = new Dictionary<Vector3Int, IInteractables>();


    void Start()
    {
        GameObject[] interactableObjects = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (var obj in interactableObjects)
        {
            if (obj.TryGetComponent<IInteractables>(out var interactable))
            {
                Vector3Int pos = _constructionTileMap.WorldToCell(obj.transform.position);
                _interactables[pos] = interactable;
                Debug.Log($"Added interactable at {pos} with type {interactable.GetType().Name}");
                interactable.tilePosition = pos; // Set the tile position for the interactable
            }
        }


        foreach (KeyValuePair<Vector3Int, IInteractables> pair in _interactables)
        {
            Debug.Log($"Interactable at {pair.Key} with type {pair.Value.GetType().Name}");
        }

    }

    private void OnEnable()
    {
        EventBus.GetTileAvailable += isAvailable;
        EventBus.Plowed += setPlowed;
        EventBus.PlantSeed += plantSeed;
        EventBus.UpdateAllCrops += updateAllCrops;
        EventBus.WaterPlant += waterCrop;

    }

    private void OnDisable()
    {
        EventBus.GetTileAvailable -= isAvailable;
        EventBus.Plowed -= setPlowed;
        EventBus.PlantSeed -= plantSeed;
        EventBus.UpdateAllCrops -= updateAllCrops;
        EventBus.WaterPlant -= waterCrop;

    }

    public bool isAvailable(Vector3 worldPosition) // Changed to Vector3
    {
        if (_groundTileMap == null) return false;

        Vector3Int tilePosition = _groundTileMap.WorldToCell(worldPosition);
        TileBase tile = _groundTileMap.GetTile(tilePosition);
        Debug.Log($"Player World Pos: {worldPosition}, Tile Pos: {tilePosition}");

        if (tile != null)
        {
            if (tile.name == "Grass_11")
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

    public bool isPlowed(Vector3Int tilePosition)
    {
        TileBase tile = _groundTileMap.GetTile(tilePosition);
        return String.Equals(tile.name, _plowedTile.name);
    }

    // Optional: Expose Tilemap for PlayerScript if needed
    public Tilemap GetGroundTilemap() => _groundTileMap;

    public void plantSeed(Vector3 worldPosition, string seedName)
    {
        CropData plantCropData = EventBus.GetCropDataBySeedName?.Invoke(seedName);
        if (plantCropData != null)
        {
            Vector3Int tilePosition = _groundTileMap.WorldToCell(worldPosition);
            if (isPlowed(tilePosition))
            {

                PlantedCrop plantedCrop = new PlantedCrop(plantCropData, EventBus.GetCurrentDate.Invoke(), 0, 0);

                _secondLayer.SetTile(tilePosition, plantedCrop.growthStageTiles[0]);
                _plantedTiles.Add(tilePosition, plantedCrop);
                Debug.Log("Planted " + seedName + " at " + tilePosition);
            }

        }
        else Debug.Log("No CropData with seed name: " + seedName);

    }

    public void waterCrop(Vector3 worldPosition)
    {
        Vector3Int tilePosition = _groundTileMap.WorldToCell(worldPosition);
        if (!isWatered(tilePosition) && _plantedTiles.ContainsKey(tilePosition))
        {
            PlantedCrop plantCrop = _plantedTiles[tilePosition];
            _groundTileMap.SetTile(tilePosition, _soiledTile);
            plantCrop.daysWatered += 1;
            Debug.Log("waterCrop Function:" + plantCrop.daysWatered);
        }

    }

    public bool isWatered(Vector3Int tilePosition)
    {
        TileBase tile = _groundTileMap.GetTile(tilePosition);
        if (tile.name == _soiledTile.name) return true;

        return false;
    }

    public void updateAllCrops(DateTime currentDate)
    {
        Debug.Log("Testing updateCrop");
        foreach (KeyValuePair<Vector3Int, PlantedCrop> pair in _plantedTiles)
        {
            PlantedCrop crop = pair.Value;
            if (crop.currentGrowthStage < crop.growthStageTiles.Count - 1 && crop.daysWatered >= crop.daysTillNextStage)
            {
                Debug.Log("updateCrop Function, crop.currentGrowthStage < crop.growthStageTiles.Count - 1: " + (crop.currentGrowthStage < crop.growthStageTiles.Count - 1));
                Debug.Log("updateCrop Function, daysWatered: " + crop.daysWatered);
                Debug.Log("updateCrop Function, daysTillNextStage: " + crop.daysTillNextStage);
                _secondLayer.SetTile(pair.Key, crop.nextStageTile);
                Debug.Log("updateCrop Function, crop.nextStageTile: " + crop.nextStageTile);
                crop.currentGrowthStage += 1;
                crop.daysWatered = 0;
            }
            // if (isWatered(pair.Key))
            // {
            //     crop.daysWatered += 1;
            // }
            _groundTileMap.SetTile(pair.Key, _plowedTile);

        }
    }

    }
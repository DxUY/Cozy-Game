using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantedCrop
{
    public CropData _cropData;         // Tham chiếu đến CropData
    public int _daysWatered;        // Số ngày tưới nước
    public int _currentGrowthStage;    // Giai đoạn phát triển
    public DateTime _plantedDate;      // Ngày trồng

    public PlantedCrop()
    {

    }
    public PlantedCrop(CropData cropData, DateTime plantedDate, int currentGrowthStage = 0, int daysWatered = 0)
    {
        _cropData = cropData;
        _plantedDate = plantedDate;
        _currentGrowthStage = currentGrowthStage;
        _daysWatered = daysWatered;
    }
    public string seedName
    {
        get { return _cropData.seedName; }
    }

    public List<Tile> growthStageTiles
    {
        get { return _cropData.growthStageTiles; }
    }

    public DateTime plantedDate
    {
        get { return _plantedDate; }
        set { _plantedDate = value; }
    }

    public int currentGrowthStage
    {
        get { return _currentGrowthStage; }
        set { _currentGrowthStage = value; }
    }

    public int daysWatered
    {
        get { return _daysWatered; }
        set { _daysWatered = value; }
    }

    public int daysTillNextStage
    {
        get { return _cropData.growthStageDays[_currentGrowthStage]; }
    }

    public TileBase nextStageTile
    {        get
    {
        if (_currentGrowthStage + 1 < _cropData.growthStageTiles.Count)
            return _cropData.growthStageTiles[_currentGrowthStage + 1];
        return null;
    }
    }
}



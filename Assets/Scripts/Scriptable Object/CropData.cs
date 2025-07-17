using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/CropData")]
public class CropData : ScriptableObject
{
    [SerializeField] private List<Tile> _growthStageTiles = new List<Tile>();
    [SerializeField] private List<int> _growthStageDays = new List<int>();
    [SerializeField] private string _seedName;
   
    public string seedName
    {
        get { return _seedName; }
    }

    public List<Tile> growthStageTiles
    {
        get { return _growthStageTiles; }
    }

   public List<int> growthStageDays
    {
        get { return _growthStageDays; }
    }
}

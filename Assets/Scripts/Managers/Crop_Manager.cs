using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Lumin;

public class Crop_Manager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, CropData> _cropDict = new Dictionary<string, CropData>();

    [SerializeField] private List<CropData> _cropDatas;

     private void OnEnable()
    {
        EventBus.GetCropDataBySeedName += getCropDataBySeedName;
    }

    private void OnDisable()
    {
        EventBus.GetCropDataBySeedName -= getCropDataBySeedName;
    }


    public CropData getCropDataBySeedName(string seedName)
    {
        if (_cropDict.ContainsKey(seedName))
        {
            return _cropDict[seedName];
        }
        else return null;
    }

    private void initialized()
    {
        foreach(CropData data in _cropDatas)
        {
            if (!_cropDict.ContainsKey(data.seedName))
            {
                _cropDict.Add(data.seedName, data);
            }
        }
            

    }

    void Awake()
    {
        initialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

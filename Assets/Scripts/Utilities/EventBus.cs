using System;
using UnityEngine;

public static class EventBus
{
    public static Action<string> OnSceneChangeRequest;
    public static Action<string> OnSceneLoaded;

    public static Action<string> OnPlaySound;
    public static Action<string> OnPlayMusic;
    public static Action OnStopMusic;

    public static Action<string> OnAreaMusicChange;

    public static Func<DateTime> GetCurrentDate;

    public static Func<Slot_UI> GetCurrentSlot;
    public static Action SetUpAllInventory;

    public static Func<string, CropData> GetCropDataBySeedName;

    public static Func<Vector3, bool> GetTileAvailable;
    public static Action<Vector3> Plowed;
    public static Action<Vector3, string> PlantSeed;
    public static Action<Vector3> WaterPlant;
    public static Action<DateTime> UpdateAllCrops;

    public static Action FishingUI;
    public static Action Fishing;

    public static Action<Vector3, BuildableItem> Build;
    public static Action<Vector3> Destroy;
    public static Action HidePreview;
    public static Action<BuildableItem, Vector3, bool> ShowPreview;
    public static Func<Vector3, RectInt, bool> IsEmpty;
    public static Func<BuildableItem> GetNextBuildableItem;
    public static Action<Sprite> SetUIActiveBuildable;
    public static Action<Buildable, bool> SetBuilableCollision;


    public static Action<string, string, Sprite> SetDialogue;
}

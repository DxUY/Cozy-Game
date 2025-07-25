using System;
using Mono.Cecil.Cil;
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

    public static Action<Vector3> InteractableInteract;

    public static Action<string, string, Sprite> SetDialogue;

    public static Action<bool> Pause;
}

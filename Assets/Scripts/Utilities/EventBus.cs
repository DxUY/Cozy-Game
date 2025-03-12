using System;

public static class EventBus
{
    public static Action<string> OnSceneChangeRequest;
    public static Action<string> OnSceneLoaded;

    public static Action<string> OnPlaySound;
    public static Action<string> OnPlayMusic;
    public static Action OnStopMusic;
    
    public static Action<string> OnAreaMusicChange;
}

using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Scriptable Objects/MusicData")]
public class MusicData : ScriptableObject
{
    [System.Serializable]
    public class MusicEntry
    {
        public string identifier;
        public string track;
    }

    public MusicEntry[] sceneMusicEntries;
    public MusicEntry[] areaMusicEntries;
}

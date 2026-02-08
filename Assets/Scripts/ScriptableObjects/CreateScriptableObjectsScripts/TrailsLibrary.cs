using UnityEngine;
[System.Serializable]
public class TrailEntry {
    public TrailType type; public GameObject prefab;
    }

[CreateAssetMenu(menuName = "FX/Trails")]
public class TrailsLibrary : ScriptableObject
{
    public TrailEntry[] entries;
}


public enum TrailType
{
    DefaultBullet
}


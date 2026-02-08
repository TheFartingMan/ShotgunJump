using System;
using UnityEngine;
[System.Serializable]
public class TrailEntry {
    public TrailType type;
    public GameObject objectPrefab;
    public TrailRenderer GetTrailRenderer()
    {
        return objectPrefab.GetComponent<TrailRenderer>();
    }
    
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


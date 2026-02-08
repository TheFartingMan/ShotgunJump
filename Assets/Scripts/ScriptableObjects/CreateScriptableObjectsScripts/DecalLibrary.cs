using UnityEngine;
[System.Serializable]
public class DecalEntry {
     public DecalType type; public GameObject prefab; 
     }

[CreateAssetMenu(menuName = "FX/Decal Library")]
public class DecalLibrary : ScriptableObject
{
    public DecalEntry[] entries;
}

// Enum for particle types used by the ParticleLibrary and emitters
public enum DecalType
{
    BulletHole
}


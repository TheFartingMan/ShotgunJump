using UnityEngine;
[System.Serializable]
public class ParticleEntry {
     public ParticleType type; public GameObject prefab; 
     }

[CreateAssetMenu(menuName = "FX/Particle Library")]
public class ParticleLibrary : ScriptableObject
{
    public ParticleEntry[] entries;
}

// Enum for particle types used by the ParticleLibrary and emitters
public enum ParticleType
{
    MuzzleFlash,
    ShellEject,
    Impact,
    Blood,
    Spark
}

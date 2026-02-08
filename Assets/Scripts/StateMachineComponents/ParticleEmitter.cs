using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    private ParticleLibrary Library;
    public void initialize(ParticleLibrary Library)
    {
        this.Library = Library;
    }


    public void emitPS(ParticleType type, Vector3 position, Quaternion rotation)
    {
        if (Library == null || Library.entries == null) return;
        foreach (var e in Library.entries)
        {
            if (e.type == type && e.prefab != null)
            {
                Instantiate(e.prefab, position, rotation);
                return;
            }
        }
    }
}

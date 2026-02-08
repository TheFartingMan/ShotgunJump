using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class DecalCreator : MonoBehaviour
{
    private DecalLibrary Library;
    public void initialize(DecalLibrary Library)
    {
        this.Library = Library;
    }

    void Awake()
    {
        //pool = new ObjectPool<DecalPrefab>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, false, defaultCapacity, maxSize);
    }

    /// <summary>
    /// Spawns a decal at a point and doesn't get moved or destroyed. 
    /// 
    /// <para></para>
    /// 
    /// Needs a decal type to know what decal to use, needs a spawn point
    /// to figure out where it should go, and it needs a normal so that it can face the correct direction
    /// </summary>
    /// <param name="decalType">Type of decal listed in the decal library scriptable object</param>
    /// <param name="spawnPoint">Where the the decal will be trying to project to</param>
    /// <param name="spawnNormal">Normal data that moves the decal back a little bit so that it can project onto the object</param>
    public void spawnDecal(DecalType decalType, Vector3 spawnPoint, Vector3 spawnNormal)
    {
        if (Library == null || Library.entries == null) return;

        foreach (var e in Library.entries)
        {
            if (e.type == decalType && e.prefab != null)
            {
                GameObject spawnedDecal = Instantiate(Library.entries[0].prefab, spawnPoint - spawnNormal * 0.0015f, Quaternion.LookRotation(spawnNormal));
                return;
            }
        }

    }



    /// <summary>
    /// Spawns a decal at a point and doesn't get moved and is object pooled. 
    /// 
    /// <para></para>
    /// 
    /// Needs a decal type to know what decal to use, needs a spawn point
    /// to figure out where it should go, and it needs a normal so that it can face the correct direction.
    /// 
    /// <para></para>
    /// 
    /// <b>Also needs an int number of objects in the pool</b>
    /// </summary>
    /// <param name="decalType">Type of decal listed in the decal library scriptable object</param>
    /// <param name="spawnPoint">Where the the decal will be trying to project to</param>
    /// <param name="spawnNormal">Normal data that moves the decal back a little bit so that it can project onto the object</param>
    public void spawnDecalPool(DecalType decalType, Vector3 spawnPoint, Vector3 spawnNormal)
    {
        if (Library == null || Library.entries == null) return;

        foreach (var e in Library.entries)
        {
            if (e.type == decalType && e.prefab != null)
            {

                ObjectPoolManager.spawnObject(Library.entries[0].prefab, spawnPoint - spawnNormal * 0.0015f, Quaternion.LookRotation(spawnNormal), ObjectPoolManager.PoolType.BulletHoles);
                return;
            }
        }

    }

}

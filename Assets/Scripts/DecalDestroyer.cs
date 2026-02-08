using UnityEngine;
public class DecalDestroyer : MonoBehaviour
{
    [SerializeField] private ShotgunStats shotgunStats;

    void OnEnable()
    {
        // Unsubscribe first to prevent duplicate subscriptions
        ObjectPoolManager.OnObjectSpawned -= HandleSpawn;
        ObjectPoolManager.OnObjectSpawned += HandleSpawn;
    }

    void OnDisable()
    {
        ObjectPoolManager.OnObjectSpawned -= HandleSpawn;
    }
    
    /// <summary>
    /// Kind of badly named, but recycles old object pool members when over a certain amount.
    /// </summary>
    /// <param name="pool"></param>
    void HandleSpawn(PooledObjectInfo pool)
    {
        // Only manage bullet hole decals
        if (pool.lookupString.Contains("Bullet Hole Decal") && pool.ActiveObjects.Count > shotgunStats.maxAmountOfBulletDecals)
        {
            // Clean up excess decals all at once instead of one at a time
            int excessCount = pool.ActiveObjects.Count - shotgunStats.maxAmountOfBulletDecals;
            
            for (int i = 0; i < excessCount; i++)
            {
                if (pool.ActiveObjects.Count > 0 && pool.ActiveObjects[0] != null)
                {
                    ObjectPoolManager.returnObjectToPool(pool.ActiveObjects[0]);
                }
            }
        }
    }

}

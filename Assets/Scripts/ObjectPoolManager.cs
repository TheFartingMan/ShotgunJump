using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();
    private GameObject objectPools;

    #region Game objects used to hold object pools
    private static GameObject bulletHoleDecals;
    private static GameObject defaultBulletTrailDecals;
    #endregion
    public static event System.Action<PooledObjectInfo> OnObjectSpawned;
    public enum PoolType
    {
        None,
        BulletHoles,
        DefaultBulletTrails
    }

    void Awake()
    {
        instance = this;
        SetupEmpties();
        CleanupNullReferences();
    }

    /// <summary>
    /// Clean up null references from previous runs when domain doesn't reload
    /// </summary>
    private static void CleanupNullReferences()
    {

        foreach (var pool in ObjectPools)
        {
            pool.ActiveObjects.RemoveAll(obj => obj == null);
            pool.InactiveObjects.RemoveAll(obj => obj == null);
        }
    }

    private void SetupEmpties()
    {
        objectPools = new GameObject("Object Pools");

        bulletHoleDecals = new GameObject("Bullet Hole Decals");
        bulletHoleDecals.transform.SetParent(objectPools.transform);

        defaultBulletTrailDecals = new GameObject("Default bullets");
        defaultBulletTrailDecals.transform.SetParent(objectPools.transform);
    }

    /// <summary>
    /// Adds an object to an object pool or creates a new object if there is no space for one
    /// </summary>
    /// <param name="objectToSpawn">Game object that will be added to the pool</param>
    /// <param name="spawnPosition">The spawn position of the object</param>
    /// <param name="spawnRotation">The spawn rotation of the object</param>
    /// <param name="poolType">The object pool that the object will go to. (Enum PoolType.(...)) </param>
    /// <returns>The game object that was just made or moved</returns>
    public static GameObject spawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        //Same as a foreach method looking for the correct lookup string
        PooledObjectInfo pool = ObjectPools.Find(p => p.lookupString == objectToSpawn.name);

        if (pool == null)
        {
            pool = new PooledObjectInfo() { lookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // Clean up any null references before spawning
        pool.ActiveObjects.RemoveAll(obj => obj == null);
        pool.InactiveObjects.RemoveAll(obj => obj == null);

        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        //Same code as above listed here in a easier to understand way
        // GameObject spawnableObj = null;
        // foreach (GameObject obj in pool.InactiveObjects)
        // {
        //     if (obj != null)
        //     {
        //         spawnableObj = obj;
        //         break;
        //     }
        // }

        if (spawnableObj == null)
        {
            GameObject parentObject = setParentObject(poolType);

            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }

            pool.ActiveObjects.Add(spawnableObj);
        }

        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            pool.ActiveObjects.Add(spawnableObj);
            spawnableObj.SetActive(true);
        }

        OnObjectSpawned?.Invoke(pool);

        return spawnableObj;
    }

    /// <summary>
    /// Returns the GameObject argument back to the pool. 
    /// 
    /// <para></para>
    /// 
    /// (I would recommend using the pool.ActiveObjects[] array to do this
    /// 
    /// <para></para> 
    /// 
    /// ex. myAwesomePool.ActiveObjects[0])
    /// </summary>
    /// <param name="obj">The object that needs to be returned back to its pool</param>
    public static void returnObjectToPool(GameObject obj, float delaySeconds = 0f)
    {
        #region return after delay
        if (delaySeconds > 0f)
        {
            if (instance == null)
            {
                Debug.LogWarning("ObjectPoolManager instance missing; returning immediately.");
            }
            else
            {
                instance.StartCoroutine(ReturnAfterDelay(obj, delaySeconds));
                return;
            }
        }
        #endregion

        if (obj == null)
        {
            Debug.LogWarning("Trying to return a null/destroyed object to the pool");
            return;
        }

        //In the editor there is (Clone) after each name, so remove it by taking off the last 7 letters
        string removeCloneName = obj.name.Substring(0, obj.name.Length - 7);

        PooledObjectInfo pool = ObjectPools.Find(p => p.lookupString == removeCloneName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to rerlease an object that is not pooled: " + obj.name);
        }

        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
            pool.ActiveObjects.Remove(obj);
        }
    }

    private static IEnumerator ReturnAfterDelay(GameObject obj, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        returnObjectToPool(obj, 0f);
    }
    
    /// <summary>
    /// Used in the spawn object method, it sets the parent object for the object pool members
    /// </summary>
    /// <param name="poolType">poolType determines which parent to put the pool members under</param>
    /// <returns>Returns the parent game object</returns>
    private static GameObject setParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.BulletHoles:
                return bulletHoleDecals;

            case PoolType.DefaultBulletTrails:
                return defaultBulletTrailDecals;

            case PoolType.None:
                return null;

            //Default is nessesary to keep the method running
            default:
                return null;
        }
    }
    
}



/// <summary>
/// Small helper class for object pool manager
/// </summary>
public class PooledObjectInfo
{
    public string lookupString;

    public List<GameObject> ActiveObjects = new List<GameObject>();
    public List<GameObject> InactiveObjects = new List<GameObject>();
}

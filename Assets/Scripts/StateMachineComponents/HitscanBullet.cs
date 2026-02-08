using System.Collections;
using UnityEngine;

public class HitscanBullet : MonoBehaviour
{
    private TrailsLibrary trailsLibrary;
    private ParticleEmitter emitter;
    private DecalCreator decalCreator;
    private DecalLibrary decalLibrary;

/// <summary>
/// 
/// </summary>
/// <param name="trailsLibrary"></param>
/// <param name="emitter"></param>
/// <param name="decalLibrary"></param>
/// <param name="decalCreator"></param>
    public void initialize(TrailsLibrary trailsLibrary, ParticleEmitter emitter, DecalLibrary decalLibrary, DecalCreator decalCreator)
    {
        this.trailsLibrary = trailsLibrary;
        this.emitter = emitter;
        this.decalLibrary = decalLibrary;
        this.decalCreator = decalCreator;
    }
    public void shootBullet(Transform spawnPoint, TrailType bulletTrailType, DecalType impactDecalType, float bulletSpeed, Vector3 bulletSpreadVarience)
    {
        Vector3 direction = GetDirection(bulletSpreadVarience, spawnPoint);

        if (Physics.Raycast(spawnPoint.position, direction, out RaycastHit hit, float.MaxValue))
        {
            GameObject trail = ObjectPoolManager.spawnObject(getTrail(bulletTrailType), spawnPoint.position, Quaternion.identity, ObjectPoolManager.PoolType.DefaultBulletTrails);

            StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true, bulletSpeed, impactDecalType));
        }

        // this has been updated to fix a commonly reported problem that you cannot fire if you would not hit anything

        else
        {
            GameObject trail = ObjectPoolManager.spawnObject(getTrail(bulletTrailType), spawnPoint.position, Quaternion.identity, ObjectPoolManager.PoolType.DefaultBulletTrails);

            StartCoroutine(SpawnTrail(trail, spawnPoint.position + GetDirection(bulletSpreadVarience, spawnPoint) * 100, Vector3.zero, false, bulletSpeed, impactDecalType));
        }
    }

    private Vector3 GetDirection(Vector3 bulletSpreadVarience, Transform spawnPoint)
    {
        Vector3 direction;

        direction = spawnPoint.forward + 
            spawnPoint.right * Random.Range(-bulletSpreadVarience.x, bulletSpreadVarience.x) +
            spawnPoint.up * Random.Range(-bulletSpreadVarience.y, bulletSpreadVarience.y) +
            spawnPoint.forward * Random.Range(-bulletSpreadVarience.z, bulletSpreadVarience.z);

        direction.Normalize();

        return direction;
    }


    private IEnumerator SpawnTrail(GameObject trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact, float bulletSpeed, DecalType impactDecalType)
    {
        // This has been updated from the video implementation to fix a commonly raised issue about the bullet trails
        // moving slowly when hitting something close, and not
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= bulletSpeed * Time.deltaTime;

            yield return null;
        }

        trail.transform.position = HitPoint;

        if (MadeImpact)
        {
            decalCreator.spawnDecalPool(impactDecalType, HitPoint, HitNormal);
        }

        ObjectPoolManager.returnObjectToPool(trail.gameObject, trail.GetComponent<TrailRenderer>().time);
    }

    private GameObject getTrail(TrailType type)
    {
        foreach (var e in trailsLibrary.entries)
        {
            if (e.type == type && e.prefab != null)
            {
                return e.prefab;
            }
        }
        return null;
    }
}

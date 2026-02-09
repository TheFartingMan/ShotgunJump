using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private AmmoManager ammoManager;
    [SerializeField] private GameObject emptyBullet;
    [SerializeField] private GameObject fullBullet;

    private void OnEnable()
    {
        ammoManager.maxAmmoChanged += maxAmmoChanged;
    }

    private void OnDisable()
    {
        ammoManager.maxAmmoChanged -= maxAmmoChanged;
    }

    private void maxAmmoChanged(int maxAmmo)
    {
        createEmptyBulletSlots(maxAmmo);
    }
    
    private void createEmptyBulletSlots(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(emptyBullet, transform.position + new Vector3(200 * i, 0, 0), Quaternion.identity, transform);
        }
    }
}

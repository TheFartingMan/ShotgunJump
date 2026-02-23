using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private AmmoManager ammoManager;
    [SerializeField] private GameObject fullBullet;
    [SerializeField] private float spacing;
    private float imageWidth;
    private List<GameObject> bulletsList;

    void Awake()
    {
        imageWidth = fullBullet.GetComponent<RectTransform>().rect.width;
        bulletsList = new List<GameObject>();
    }

    private void OnEnable()
    {
        ammoManager.maxAmmoChanged += maxAmmoChanged;
        ammoManager.ammoChanged += ammoChanged;
    }

    private void OnDisable()
    {
        ammoManager.maxAmmoChanged -= maxAmmoChanged;
        ammoManager.ammoChanged -= ammoChanged;
    }

    private void maxAmmoChanged(int maxAmmo)
    {
        bulletsList.Clear();
        createBulletSlots(maxAmmo);
    }

    private void ammoChanged(int ammoCount)
    {
        

        if (bulletsList.Count == 0)
        {
            Debug.LogWarning("Something is wrong with the maximum ammo count");
            return;
        }

        if (ammoCount == bulletsList.Count)
        {
            foreach (GameObject gameObject in bulletsList)
            {
                Image img1 = gameObject.GetComponent<Image>();
                img1.color = Color.white;
            }
            return;
        }

        if (ammoCount < 0 || ammoCount >= bulletsList.Count)
        {
            Debug.LogWarning($"Ammo index out of range: {ammoCount} out of {bulletsList.Count}");
            return;
        }

        Image img = bulletsList[ammoCount].GetComponent<Image>();
        img.color = Color.black;
    }
    
    private void createBulletSlots(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(fullBullet, new Vector3((imageWidth / 2) + (spacing * i), transform.position.y, transform.position.z), Quaternion.identity, transform);
            bulletsList.Add(bullet);
        }
    }
}

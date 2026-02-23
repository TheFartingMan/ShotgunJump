using System;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public int ammoCount { get; set; }
    public int maxAmmo { get; private set; }
    public event Action<int> maxAmmoChanged;
    public event Action<int> ammoChanged;

    public void setMaxAmmo(int maxAmmo)
    {
        this.maxAmmo = maxAmmo;
        maxAmmoChanged?.Invoke(maxAmmo);
    }
    
    public void addAmmo(int count = 1)
    {
        if (maxAmmo < ammoCount + count)
        {
            ammoCount = maxAmmo;
        }
        else
        {
           ammoCount += count; 
        }
        ammoChanged?.Invoke(ammoCount);
    }

    public void subtractAmmo(int count = 1)
    {
        ammoCount -= count;
        ammoChanged?.Invoke(ammoCount);
    }
}

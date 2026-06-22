using System;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public bool isHit { get; private set; } = false;
    public static event System.Action<Target> targetHit; 

    private MeshRenderer meshRenderer;
    [SerializeField] private Material startmaterial;
    [SerializeField] private Material hitMaterial;


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void takeDamage(float damageAmount)
    {
        if (isHit) return;

        isHit = true;
        meshRenderer.material = hitMaterial;
        targetHit?.Invoke(this);
        
    }
}

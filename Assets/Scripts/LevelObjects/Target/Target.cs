using System;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
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

    /// <summary>
    /// When this method is called it lets the target know that it has been hit and changes material. 
    /// It immidiately returns nothing if hit again.
    /// </summary>
    /// <param name="damageAmount">Completely irrelevant for a target</param>
    public void takeDamage(float damageAmount)
    {
        if (isHit) return;

        isHit = true;
        meshRenderer.material = hitMaterial;
        targetHit?.Invoke(this);
    }
}

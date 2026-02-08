using UnityEngine;
[CreateAssetMenu(menuName = "Player/ShotgunStats")]
public class ShotgunStats : ScriptableObject
{
    public float shotgunPushForce;
    public float bulletSpeed;
    public float bulletSpreadY;
    public float bulletSpreadX;
    public float amountOfBullets;
    public int maxAmountOfBulletDecals;
}
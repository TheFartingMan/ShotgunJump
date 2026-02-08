using UnityEngine;
[CreateAssetMenu(menuName = "Player/Stats")]
public class PlayerStats : ScriptableObject
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 15f;
    public float jumpBufferTime = 0.1f;
    public float acceleration = 5;
    [Tooltip("Deceleration under max speed")]
    [Range(0f, 1f)]
    public float deceleration = .95f;
    [Tooltip("Deceleration from the extra movement speed from a shotgun back to the max movement speed")]
    [Range(0f, 1f)]
    public float extraDeceleration = .95f;
    [Range(0f, 1f)]
    public float airDeceleration = .99f;
    public float airAcceleration = 2.5f;
}

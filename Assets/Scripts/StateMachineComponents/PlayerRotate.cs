using System;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private Rigidbody rb;
    private float localPitch = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void rotateYaw(float amount)
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, amount, 0f));
    }
    public void rotatePitch(float amount)
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(amount, 0f, 0f));
    }

    public void rotatePitchLocal(float amount)
    {
        localPitch += amount;
        transform.localRotation = Quaternion.Euler(localPitch, 0f, 0f);
    }

    public void rotatePitchLocal(float amount, Transform childTransform)
    {
        localPitch = Mathf.Clamp(localPitch + amount, -89.9f, 89.9f);
        childTransform.localRotation = Quaternion.Euler(localPitch, 0f, 0f);

    }
    
}

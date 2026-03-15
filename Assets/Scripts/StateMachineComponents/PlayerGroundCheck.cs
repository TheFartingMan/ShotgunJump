using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public bool isGrounded { get; private set; }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = false;
        foreach (ContactPoint c in collision.contacts)
        {
            if (collisionAngle(c) < 30)
            {
                isGrounded = true;
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }


    private float collisionAngle(ContactPoint contact)
    {
        Vector3 surfaceNormal = contact.normal;
        float slopeAngle = Vector3.Angle(surfaceNormal, Vector3.up);
        return slopeAngle;
    }
}

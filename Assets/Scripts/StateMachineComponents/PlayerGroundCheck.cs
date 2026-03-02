using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public bool isGrounded { get; private set; }

    void OnCollisionStay(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        int groundContacts = collision.contactCount;

            foreach (ContactPoint c in collision.contacts)
            {
                if (collisionAngle(c) < 30)
                {
                    isGrounded = true;
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

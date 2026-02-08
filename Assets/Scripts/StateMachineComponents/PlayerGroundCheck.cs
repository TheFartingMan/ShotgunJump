using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public bool isGrounded { get; private set; }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}

using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerStats Stats;
    public void initialize(PlayerStats Stats)
    {
        this.Stats = Stats;
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    #region Movement and Acceleration
    /// <summary>
    /// Move
    /// </summary>
    /// <param name="velocity"></param>
    public void move(Vector3 velocity)
    {
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
    /// <summary>
    /// Move with acceleration dictated by the player stats acceleration stat.
    /// </summary>
    /// <param name="binaryVelocity"></param>
    public void accelerateMove(Vector3 binaryVelocity)
    {
        float maxSpeed;
        Vector3 clampedSpeed;
        addClampedAccleration(binaryVelocity, Stats.acceleration, out maxSpeed, out clampedSpeed);

        if (Mathf.Sqrt(Mathf.Pow(rb.linearVelocity.x, 2) + Mathf.Pow(rb.linearVelocity.z, 2)) > maxSpeed)
        {
            decelerateMove(Stats.extraDeceleration);
        }
        else
        {
            move(clampedSpeed);
        }

        decelerateIfNoInput(binaryVelocity);
    }
    /// <summary>
    /// custom deceleration. deceleration is under max speed deceleration and extraDeceleration is above max speed. These numbers should be between 0 and 1.
    /// 0 being instant deceleration and 1 being no deceleration.
    /// </summary>
    /// <param name="binaryVelocity"></param>
    /// <param name="deceleration"></param>
    /// <param name="extraDeceleration"></param>
    public void accelerateMove(Vector3 binaryVelocity, float acceleration, float deceleration, float extraDeceleration)
    {
        float maxSpeed;
        Vector3 clampedSpeed;
        addClampedAccleration(binaryVelocity, acceleration, out maxSpeed, out clampedSpeed);

        if (Mathf.Sqrt(Mathf.Pow(rb.linearVelocity.x, 2) + Mathf.Pow(rb.linearVelocity.z, 2)) > maxSpeed)
        {
            decelerateMove(extraDeceleration);
        }
        else
        {
            move(clampedSpeed);
        }

        decelerateIfNoInput(binaryVelocity, deceleration);
    }

    #region Old accelerateMoveNoDeceleration
    /// <summary>
    /// Same thing as accelerateMove, but there is no deceleration on the x and z axis.
    /// </summary>
    /// <param name="binaryVelocity"></param>
    public void accelerateMoveNoDeceleration(Vector3 binaryVelocity)
    {
        Vector3 worldBinaryVelocity = transform.TransformDirection(binaryVelocity);
        Vector3 delta = new Vector3(
            worldBinaryVelocity.x * Stats.acceleration * Time.fixedDeltaTime,
            0f,
            worldBinaryVelocity.z * Stats.acceleration * Time.fixedDeltaTime);
        Vector3 newVelocity = rb.linearVelocity + delta;
        float maxSpeed = Stats.moveSpeed;

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            Vector3 finalVelocity = newVelocity.normalized * maxSpeed;
            move(finalVelocity);
        }
        else
        {
            move(newVelocity);
        }

    }
    #endregion
    
    #region Private movement and acceleration methods
    private Vector3 clampPlanarUnlessAlreadyOverspeed(Vector3 currentSpeed, Vector3 newSpeed, float maxSpeed)
    {
        // if we're already over the max, leave the overspeed alone
        if (currentSpeed.magnitude > maxSpeed) return newSpeed;
        // otherwise clamp the result to maxSpeed
        if (newSpeed.magnitude > maxSpeed) return newSpeed.normalized * maxSpeed;
        return newSpeed;
    }

    private void addClampedAccleration(Vector3 binaryVelocity, float acceleration, out float maxSpeed, out Vector3 clampedSpeed)
    {
        Vector3 worldBinaryVelocity = transform.TransformDirection(binaryVelocity);
        Vector3 delta = new Vector3(
            worldBinaryVelocity.x * acceleration * Time.fixedDeltaTime,
            0f,
            worldBinaryVelocity.z * acceleration * Time.fixedDeltaTime);
        Vector3 newVelocity = rb.linearVelocity + delta;
        maxSpeed = Stats.moveSpeed;
        clampedSpeed = clampPlanarUnlessAlreadyOverspeed(rb.linearVelocity, newVelocity, maxSpeed);
    }

    private void decelerateIfNoInput(Vector3 binaryVelocity)
    {
        if (binaryVelocity == Vector3.zero)
        {
            decelerateMove();
        }
    }

    private void decelerateIfNoInput(Vector3 binaryVelocity, float decelerationSpeed)
    {
        if (binaryVelocity == Vector3.zero)
        {
            decelerateMove(decelerationSpeed);
        }
    }
    #endregion

    #endregion

    #region Deceleration
    /// <summary>
    /// Decelerates only linear velocity x and z by the deceleration value in the player stats scriptable object
    /// </summary>
    public void decelerateMove()
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x * Stats.deceleration,
            rb.linearVelocity.y,
            rb.linearVelocity.z * Stats.deceleration);
    }

    /// <summary>
    /// Same as normal decelerate move but takes in a deceleration parameter
    /// <para>
    /// Deceleration should be between 0 and 1
    /// </para>
    /// </summary>
    /// <param name="deceleration"></param>
    public void decelerateMove(float deceleration)
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x * deceleration,
            rb.linearVelocity.y,
            rb.linearVelocity.z * deceleration);
    }
    #endregion
    
    #region Jump and Shotgun Jump
    public void addImpulse(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }
    
    public void shotgunJump(Vector3 force)
    {
        if (force.y > 0 && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        }
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void jump(float force)
    {
        Vector3 v = rb.linearVelocity;
        v.y = 0f;
        rb.linearVelocity = v;

        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
    #endregion
}

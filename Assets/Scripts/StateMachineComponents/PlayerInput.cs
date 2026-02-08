using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Public get variables
    public Vector2 move { get; private set; }
    public float horizontalMouseDelta { get; private set; }
    public float verticalMouseDelta { get; private set; }
    public bool jumpPressed { get; private set; }
    public float jumpBufferTimer { get; private set; }
    public bool mousePressed{ get; private set; }
    #endregion
    private PlayerStats Stats;
    private bool jumpQueued;

    public void initialize(PlayerStats Stats)
    {
        this.Stats = Stats;
    }
    void Update()
    {
        updateMovement();
        checkMouseX();
        checkMouseY();
        checkJump();
        checkMouseClick();
    }
    #region Check Methods
    private void checkMouseX()
    {
        horizontalMouseDelta = Input.GetAxis("Mouse X");
    }
    private void checkMouseY()
    {
        verticalMouseDelta = Input.GetAxis("Mouse Y");
    }

    private void updateMovement()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void checkJump()
    {
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        if (jumpPressed)
        {
            jumpQueued = true;
            jumpBufferTimer = Stats.jumpBufferTime;
        }

        if (jumpBufferTimer > 0f)
        {
            jumpBufferTimer -= Time.deltaTime;
        }
    }
    public void checkMouseClick()
    {
        mousePressed = Input.GetMouseButtonDown(0);
    }
    #endregion
    
    #region Logic Methods

    /// <summary>
    /// Normal jump method that helps with detecting input on Update and using that input on FixedUpdate. Does run code, so it's best to put it at the end of short circut if statements.
    /// </summary>
    /// <returns>Allows you to jump as soon as possible after jump is pressed in the PlayerInput class</returns>
    public bool consumeJump()
    {
        if (jumpQueued)
        {
            jumpQueued = false;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Very similar to consumeJump method, but allows for a buffer before landing. The buffer timer is set in the PlayerStats scriptable object.
    /// </summary>
    /// <returns>Does a normal jump or buffers a jump</returns>
    public bool bufferConsumeJump(){
        if (jumpBufferTimer > 0f)
        {
            jumpBufferTimer = 0f;
            return true;
        }
        return false;
    }
    #endregion

}

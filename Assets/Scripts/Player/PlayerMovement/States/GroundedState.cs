using UnityEngine;

public class GroundedState : PlayerMovementState
{
    public GroundedState(PlayerMovementStateMachine machine) : base(machine) { }

    public override void FixedUpdate()
    {
        changeStateIfNotGrounded();
        checkInputAndMove();

        if (machine.GroundCheck.isGrounded && machine.Input.bufferConsumeJump())
        {
            machine.Motor.jump(machine.Stats.jumpForce);
        }
    }
    public override void Update()
    {
        RotatePlayerAndCamera();
    }

    private void changeStateIfNotGrounded()
    {
        if (!machine.GroundCheck.isGrounded)
        {
            machine.changeState(new AirState(machine));
        }
    }

    private void checkInputAndMove()
    {
        Vector2 input = machine.Input.move;
        Vector3 velocity = new Vector3(input.x, 0, input.y);
        machine.Motor.accelerateMove(velocity);
    }
    
    private void RotatePlayerAndCamera()
    {
        float mouseDelta = machine.Input.horizontalMouseDelta;
        machine.Rotate.rotateYaw(mouseDelta * machine.Stats.mouseSensitivity);
        machine.Rotate.rotatePitchLocal(-machine.Input.verticalMouseDelta * machine.Stats.mouseSensitivity, machine.shoulderTransform);
    }
}

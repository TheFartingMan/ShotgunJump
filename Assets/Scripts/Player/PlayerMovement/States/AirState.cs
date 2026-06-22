using UnityEngine;

public class AirState : PlayerMovementState
{
    public AirState(PlayerMovementStateMachine machine) : base(machine) { }

    public override void FixedUpdate()
    {
        changeStateIfGrounded();
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

    private void checkInputAndMove()
    {
        Vector2 input = machine.Input.move;
        Vector3 velocity = new Vector3(input.x * machine.Stats.moveSpeed, 0, input.y * machine.Stats.moveSpeed);
        machine.Motor.accelerateMove(velocity, machine.Stats.airAcceleration, machine.Stats.airDeceleration, machine.Stats.extraDeceleration);
    }

    private void changeStateIfGrounded()
    {
        if (machine.GroundCheck.isGrounded)
        {
            machine.changeState(new GroundedState(machine));
        }
    }

    private void RotatePlayerAndCamera()
    {
        float mouseDelta = machine.Input.horizontalMouseDelta;
        machine.Rotate.rotateYaw(mouseDelta * machine.Stats.mouseSensitivity);
        machine.Rotate.rotatePitchLocal(-machine.Input.verticalMouseDelta * machine.Stats.mouseSensitivity, machine.shoulderTransform);
    }
}

using UnityEngine;

public abstract class PlayerMovementState
{
    protected PlayerMovementStateMachine machine;

    protected PlayerMovementState(PlayerMovementStateMachine machine)
    {
        this.machine = machine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}

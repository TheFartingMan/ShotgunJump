public abstract class ShotgunState
{
    protected ShotgunStateMachine machine;

    protected ShotgunState(ShotgunStateMachine machine)
    {
        this.machine = machine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}

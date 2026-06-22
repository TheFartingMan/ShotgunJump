using UnityEngine;

public class PlayerMovementStateMachine : MonoBehaviour
{
    [Header("Refrences")]
    public PlayerStats Stats;
    public PlayerMovementState currentState { get; private set; }
    public Transform shoulderTransform;

    #region Connected Scripts

    public PlayerInput Input { get; private set; }
    public PlayerMotor Motor { get; private set; }
    public PlayerRotate Rotate { get; private set; }
    public PlayerGroundCheck GroundCheck { get; private set; }

    #endregion

    void Awake()
    {
        Input = GetComponent<PlayerInput>();
        Input.initialize(Stats);
        Motor = GetComponent<PlayerMotor>();
        Motor.initialize(Stats);
        Rotate = GetComponent<PlayerRotate>();
        GroundCheck = GetComponent<PlayerGroundCheck>();
    }

    void Start()
    {
        changeState(new GroundedState(this));
    }

    void Update() => currentState?.Update();
    void FixedUpdate() => currentState?.FixedUpdate();

    public void changeState(PlayerMovementState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}

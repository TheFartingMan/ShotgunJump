using UnityEngine;

public class ShotgunStateMachine : MonoBehaviour
{
    public ShotgunState currentState { get; private set; }

    [Header("Refrences")]
    public ShotgunStats[] shotgunStats;
    public PlayerStats Stats;
    public ParticleLibrary ParticleLibrary;
    public TrailsLibrary trailsLibrary;
    public DecalLibrary decalLibrary;
    public Transform ShotgunTip;
    public Transform shoulderTransform;
    public PlayerMotor playerMotor;
    public Animator anim;

    #region Connected Scripts
    public PlayerInput Input { get; private set; }
    public ParticleEmitter Emitter { get; private set; }
    public PlayerRotate Rotate { get; private set; }
    public HitscanBullet hitscanBullet { get; private set; }
    public DecalCreator decalCreator { get; private set; }
    public ObjectPoolManager objectPoolManager { get; private set; }
    #endregion
    //declare states attatched
    void Awake()
    {
        Input = GetComponent<PlayerInput>();
        Input.initialize(Stats);
        Emitter = GetComponent<ParticleEmitter>();
        Emitter.initialize(ParticleLibrary);
        Rotate = GetComponent<PlayerRotate>();
        decalCreator = GetComponent<DecalCreator>();
        decalCreator.initialize(decalLibrary);
        hitscanBullet = GetComponent<HitscanBullet>();
        hitscanBullet.initialize(trailsLibrary, Emitter, decalLibrary, decalCreator);
        objectPoolManager = GetComponent<ObjectPoolManager>();
        
    }

    void Start()
    {
        changeState(new DefaultShotgunState(this));
    }
    void Update() => currentState?.Update();
    void FixedUpdate() => currentState?.FixedUpdate();

    public void changeState(ShotgunState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    
}

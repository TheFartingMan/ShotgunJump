using UnityEngine;

[RequireComponent(typeof(TimerManager))]
public class ShotgunStateMachine : MonoBehaviour
{
    public ShotgunState currentState { get; private set; }

    #region Refrences
    [Header("Refrences")]
    /*
        This technically could be a spot to use an enum instead of an array, but I can't think of how to do that right now
        index 0: default shotgun
    */
    public ShotgunStats[] shotgunStats;
    public GameObject[] shotgunPrefabs;
    public PlayerStats Stats;
    public ParticleLibrary ParticleLibrary;
    public TrailsLibrary trailsLibrary;
    public DecalLibrary decalLibrary;
    public Transform shoulderTransform;
    public Animator anim;
    #endregion

    #region Connected Scripts
    [Header("Connected scripts from player")]
    public PlayerMotor playerMotor;
    public PlayerGroundCheck playerGroundCheck;
    #endregion

    #region Connected Scripts
    public PlayerInput Input { get; private set; }
    public ParticleEmitter Emitter { get; private set; }
    public PlayerRotate Rotate { get; private set; }
    public HitscanBullet hitscanBullet { get; private set; }
    public DecalCreator decalCreator { get; private set; }
    public ObjectPoolManager objectPoolManager { get; private set; }
    public AmmoManager ammoManager { get; private set; }
    public TimerManager timerManager { get; private set; }
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

        ammoManager = GetComponent<AmmoManager>();

        timerManager = GetComponent<TimerManager>();
        
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

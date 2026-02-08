using UnityEngine;

public class RotateCameraY : MonoBehaviour
{
    private PlayerInput Input;
    private PlayerRotate Rotate;
    public PlayerStats Stats;

    void Awake()
    {
        Input = GetComponent<PlayerInput>();
        Input.initialize(Stats);
        Rotate = GetComponent<PlayerRotate>();
    }

    void Update()
    {
        Rotate.rotatePitchLocal(Input.verticalMouseDelta* Stats.mouseSensitivity * Time.deltaTime);
    }
}

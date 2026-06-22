using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private int totalTargets;
    private int hitTargets;
    [Tooltip("Level number starts with 0, so the tutorial is level 0, the next level is level 1, etc.")]
    [SerializeField] private int level;

    private void OnEnable()
    {
        Target.targetHit += OnTargetHit;
    }

    private void OnDisable()
    {
        Target.targetHit -= OnTargetHit;
    }

    private void OnTargetHit(Target target)
    {
        hitTargets++;

        if (hitTargets >= totalTargets && level == OneWayTeleporter.levelNumber)
        {
            AllTargetsHit();
        }
    }

    private void AllTargetsHit()
    {
        Debug.Log("All targets hit");
    }
}
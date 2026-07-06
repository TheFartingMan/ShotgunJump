using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private int totalTargets;
    private int hitTargets;
    [Tooltip("Level number starts with 0, so the tutorial is level 0, the next level is level 1, etc.")]
    [SerializeField] private int level;
    [Tooltip("The GameObject that is going to be set inactive once all targets are hit")]
    [SerializeField] private GameObject wall;
    private void OnEnable()
    {
        Target.targetHit += OnTargetHit;
    }

    private void OnDisable()
    {
        Target.targetHit -= OnTargetHit;
    }

    //  I am passing in the Target script in case I need to get/set some info from the target that just got hit.
    //As of now it is unessesary
    private void OnTargetHit(Target target)
    {
        if (level == OneWayTeleporter.levelNumber)
        {
            hitTargets++;

            if (hitTargets >= totalTargets)
            {
                AllTargetsHit();
            }
        }
    }

    private void AllTargetsHit()
    {
        Debug.Log("All targets hit");

        wall.SetActive(false);
    }
}
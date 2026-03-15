using UnityEngine;

public class DefaultShotgunState : ShotgunState
{
    private const string reloadTimerId = "default_shotgun_reload";

    public DefaultShotgunState(ShotgunStateMachine machine) : base(machine) { }

    public override void Enter()
    {
        machine.shotgunPrefabs[0].SetActive(true);
        machine.ammoManager.setMaxAmmo(machine.shotgunStats[0].magSize);
    }

    public override void Update()
    {

        if (machine.Input.mousePressed && machine.ammoManager.ammoCount > 0)
        {
            //machine.Emitter.emitPS(ParticleType.MuzzleFlash, machine.ShotgunTip.position, machine.ShotgunTip.rotation);


            for (int i = 0; i < machine.shotgunStats[0].amountOfBullets; i++)
            {
                machine.hitscanBullet.shootBullet(
                    machine.shotgunPrefabs[0].transform.GetChild(0),
                    TrailType.DefaultBullet,
                    DecalType.BulletHole,
                    machine.shotgunStats[0].bulletSpeed,
                    new Vector3(machine.shotgunStats[0].bulletSpreadX, machine.shotgunStats[0].bulletSpreadY, 0)
                    );
            }
            machine.anim.Play("Shotgun Shoot", 0, 0);
            machine.playerMotor.shotgunJump(-machine.shoulderTransform.forward.normalized * machine.shotgunStats[0].shotgunPushForce);

            machine.ammoManager.subtractAmmo();
        }

        if (machine.playerGroundCheck.isGrounded)
        {
            if (machine.ammoManager.ammoCount < machine.ammoManager.maxAmmo)
            {
                machine.timerManager.startTimerIfNotRunning(reloadTimerId, 0.1f, refillAmmo);
            }
        }
        else
        {
            machine.timerManager.cancelTimer(reloadTimerId);
        }
    }

    private void refillAmmo()
    {
        machine.ammoManager.addAmmo(machine.shotgunStats[0].magSize);
    }

    public override void Exit()
    {
        machine.timerManager.cancelTimer(reloadTimerId);
        machine.shotgunPrefabs[0].SetActive(false);
    }

}

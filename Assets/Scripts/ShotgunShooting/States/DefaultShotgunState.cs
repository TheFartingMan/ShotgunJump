using UnityEngine;

public class DefaultShotgunState : ShotgunState
{
    public DefaultShotgunState(ShotgunStateMachine machine) : base(machine) { }

    public override void Update()
    {
        if (machine.Input.mousePressed)
        {
            //machine.Emitter.emitPS(ParticleType.MuzzleFlash, machine.ShotgunTip.position, machine.ShotgunTip.rotation);
            
            
            for (int i = 0; i < machine.shotgunStats[0].amountOfBullets; i++)
            {
                machine.hitscanBullet.shootBullet(
                    machine.ShotgunTip,
                    TrailType.DefaultBullet,
                    DecalType.BulletHole,
                    machine.shotgunStats[0].bulletSpeed,
                    new Vector3(machine.shotgunStats[0].bulletSpreadX, machine.shotgunStats[0].bulletSpreadY, 0)
                    );
            }
            machine.anim.Play("Shotgun Shoot", 0, 0);
            machine.playerMotor.shotgunJump(-machine.shoulderTransform.forward.normalized * machine.shotgunStats[0].shotgunPushForce);
        }
    }

}

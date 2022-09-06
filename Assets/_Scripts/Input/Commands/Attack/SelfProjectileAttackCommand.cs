using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfProjectileAttack", menuName = "DadJokes/Input/SelfProjectile")]
public class SelfProjectileAttackCommand : AttackCommand
{
    [Header("Stats")]
    public FloatVariable range;
    public FloatVariable radius;
    public FloatVariable damage;

    [Header("VFX")]
    public GameObjectVariable attackVFX;
    public GameObjectVariable impactVFX;

    public override float Damage => damage;

    public override void EndExecute(CombatController t)
    {

    }

    public override void StartExecute(CombatController t)
    {
        VFXManager.Spawn(attackVFX, 3f, t.transform);
        t.AttackLinearProjectileSelfLocalSpace(Vector3.zero, t.transform.forward, radius, range, damage, (RayHit<Health> h) =>
        {
            VFXManager.Spawn(impactVFX, 2f, h.transform);
        });
        StopCommand(t);
    }

    public override void WhileExecute(CombatController t)
    {

    }

}

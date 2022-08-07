using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;


[CreateAssetMenu(fileName = "RangedChargedAttack", menuName = "DadJokes/Input/RangedCharged")]
public class RangedChargedAttackCommand : ChargedAttackCommand
{
    [Header("Stats")]
    public FloatVariable range;
    public FloatVariable radius;


    public override float Damage => chargeDamage;

    [Header("VFX")]
    public GameObjectVariable attackVFX;
    public GameObjectVariable impactVFX;
    public GameObjectVariable chargeVFX;

    VFXInstance chargeInstance;


    public override void BeginCharge(CombatController t, float chargeAmount)
    {
        //throw new System.NotImplementedException();
        chargeInstance = VFXManager.Spawn(chargeVFX, maxChargeTime, t.transform, Vector3.one, null, (instance) =>
        {
            //if(instance.gameObject != null)
            //instance.gameObject.transform.localScale *= instance.timeElapsed / instance.lifespan;
        });
    }

    public override void ReleaseCharge(CombatController t, float chargeAmount)
    {
        VFXInstance attack = VFXManager.Spawn(attackVFX, 3f, null, t.transform.position, t.transform.rotation);
        attack.gameObject.transform.localScale *= chargeAmount < 0.25f ? 0.25f : chargeAmount;
        t.AttackAtDirectionLocalSpace(Vector3.zero, t.transform.forward, radius, range, Damage, (RayHit<Health> h) =>
        {
            VFXInstance impact = VFXManager.Spawn(impactVFX, 2f, h.transform);
        });
        VFXManager.Remove(chargeInstance);
    }

    public override void WhileCharge(CombatController t, float chargeAmount)
    {
        //throw new System.NotImplementedException();
    }
}

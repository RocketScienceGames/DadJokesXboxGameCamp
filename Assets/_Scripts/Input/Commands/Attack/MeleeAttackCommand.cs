using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;


[CreateAssetMenu(fileName = "MeleeAttack", menuName = "DadJokes/Input/Melee")]
public class MeleeAttackCommand : AttackCommand
{
    [Header("Stats")]
    public FloatVariable range;
    public FloatVariable damage;

    [Header("VFX")]
    public FloatVariable delayImpact;
    public GameObjectVariable attackVFX;
    public GameObjectVariable impactVFX;

    public override float Damage => damage;

    public override void EndExecute(CombatController t)
    {
        
    }

    public override void StartExecute(CombatController t)
    {
        VFXManager.Spawn(attackVFX, 1f, t.transform, Vector3.forward * range);
        t.StartCoroutine(DelayImpactCR(t));
    }

    IEnumerator DelayImpactCR(CombatController cc)
    {
        yield return new WaitForSeconds(delayImpact);
        AttackImpact(cc);
    }

    public virtual void AttackImpact(CombatController t)
    {
        t.AttackAtPointLocalSpace(Vector3.zero, range, damage, (Health h) =>
        {
            VFXManager.Spawn(impactVFX, 1f, h.transform);
        });
        StopCommand(t);
    }

    public override void WhileExecute(CombatController t)
    {
        
    }
}

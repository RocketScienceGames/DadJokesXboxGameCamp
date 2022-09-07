using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;


[CreateAssetMenu(fileName = "RangedAttack", menuName = "DadJokes/Input/Ranged")]
public class RangedAttackCommand : AttackCommand
{
    [Header("Stats")]
    public FloatVariable range;
    public FloatVariable radius;
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
        VFXManager.Spawn(attackVFX, 3f, null, t.transform.position, t.transform.rotation);
        t.StartCoroutine(DelayImpactCR(t));
    }

    IEnumerator DelayImpactCR(CombatController cc)
    {
        yield return new WaitForSeconds(delayImpact);
        AttackImpact(cc);
    }

    public virtual void AttackImpact(CombatController t)
    {
        t.AttackAtDirectionLocalSpace(Vector3.zero, t.transform.forward, radius, range, damage, (RayHit<Health> h)=>
        {
            VFXManager.Spawn(impactVFX, 2f, h.transform);
        });
        StopCommand(t);
    }

    public override void WhileExecute(CombatController t)
    {
        
    }
}

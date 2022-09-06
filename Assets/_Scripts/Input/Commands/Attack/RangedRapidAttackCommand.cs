using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedRapidAttack", menuName = "DadJokes/Input/RangedRapidAttack")]
public class RangedRapidAttackCommand : TimedAttackCommand
{

    [Header("Stats")]
    public float range;
    public float radius;
    public float totalDamage;
    public float angleDeviation;

    [Header("VFX")]
    public GameObjectVariable attackVFX;
    public GameObjectVariable impactVFX;



    public override float Damage => totalDamage;

    public override void BeginAttack(CombatController t, float timeCompletion)
    {
        VFXManager.Spawn(attackVFX, duration, t.transform , Vector3.forward + Vector3.up);
    }

    public override void EndAttack(CombatController t, float timeCompletion)
    {
        
    }

    public override void WhileAttack(CombatController t, float timeCompletion)
    {
        Vector3 direction = RotateVector3(Vector3.forward, Random.Range(-angleDeviation, angleDeviation));
        t.AttackAtDirectionLocalSpace(Vector3.zero, direction, radius, range, damagePerTimestep, (hit)=>
        {
            VFXManager.Spawn(impactVFX, timeStep, hit.transform);
        });
    }

    
    public Vector3 RotateVector3(Vector3 vector, float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.up) * vector;
    }

}

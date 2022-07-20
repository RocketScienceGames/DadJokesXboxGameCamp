using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;


[CreateAssetMenu(fileName = "MeleeAttack", menuName = "DadJokes/Input/Melee")]
public class MeleeAttackCommand : CorruptedCommand<CombatController>
{

    public FloatVariable range;
    public FloatVariable damage;

    public override void EndExecute(CombatController t)
    {
        
    }

    public override void StartExecute(CombatController t)
    {
        t.AttackAtPointLocalSpace(Vector3.forward * range, range, damage);
    }

    public override void WhileExecute(CombatController t)
    {
        
    }
}

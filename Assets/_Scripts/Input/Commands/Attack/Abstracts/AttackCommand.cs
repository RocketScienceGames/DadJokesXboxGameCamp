using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCommand : CorruptedCommand<CombatController>
{

    [Header("UX Settings")]
    public FloatVariable cooldown;
    public AttackAbilityUI uiElement;

    public abstract float Damage { get; }

    public void StopCommand(CombatController t)
    {
        EndExecute(t);
    }

}

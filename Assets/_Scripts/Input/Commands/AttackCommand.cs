using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCommand : CorruptedCommand<CombatController>
{

    public FloatVariable cooldown;

}

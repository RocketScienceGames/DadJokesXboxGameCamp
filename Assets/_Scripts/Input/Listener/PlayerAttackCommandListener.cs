using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

[RequireComponent(typeof(CombatController))]
public class PlayerAttackCommandListener : CorruptedCommandListener<CombatController>
{
    public override CombatController GetReceiver()
    {
        return GetComponent<CombatController>();
    }


    private void FixedUpdate()
    {
        foreach (CommandListener cl in buttons)
        {
            if (cl.command is IFixedUpdateListener<CombatController>)
                (cl.command as IFixedUpdateListener<CombatController>).OnFixedUpdate(receiver);
        }
        foreach (CommandAxisListener cl in axes)
        {
            if (cl.command is IFixedUpdateListener<CombatController>)
                (cl.command as IFixedUpdateListener<CombatController>).OnFixedUpdate(receiver);
        }
    }
}

    


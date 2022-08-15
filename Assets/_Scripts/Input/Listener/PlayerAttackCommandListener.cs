using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;
using System;

[RequireComponent(typeof(CombatController))]
public class PlayerAttackCommandListener : CorruptedCommandListener<CombatController>
{



    protected override void Start()
    {
        base.Start();
        OnCommandEnd += StartCooldown;
    }

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

    private void StartCooldown(CorruptedCommand<CombatController> obj)
    {
        CommandListener listener = GetCommandListener(obj);
        AttackCommand command = obj as AttackCommand;
        if (listener.isValid)
            StartCoroutine(CooldownCR(listener, command.cooldown));

    }

    IEnumerator CooldownCR(CommandListener listener, float cooldown) {
        listener.isValid = false;
        yield return new WaitForSeconds(cooldown);
        listener.isValid = true;
    }
}

    


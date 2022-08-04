using Corrupted;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CombatController))]
public class NPCAttackCommand : NPCBehaviour
{

    public NPCAttack[] commands;

    CombatController combat;

    bool attackInProgress = false;

    private void Awake()
    {
        combat = GetComponent<CombatController>();
    }


    public override void OnEnd()
    {
        StopAllCoroutines();
        attackInProgress = false;
    }

    public override void OnStart()
    {
        foreach(NPCAttack attack in commands)
        {
            if(attack.isValid)
            StartNPCAttack(attack);
        }
    }

    public override void OnUpdate()
    {
        LookAtATarget();
    }

    public void StartNPCAttack(NPCAttack attack)
    {
        StartCoroutine(NPCAttackCR(attack));
    }

    IEnumerator NPCAttackCR(NPCAttack attack)
    {
        ExecuteCommand(attack.command, attack.duration.GetRandom());
        yield return new WaitForSeconds(attack.frequency.GetRandom());
        yield return NPCAttackCR(attack);
    }

    public void ExecuteCommand(AttackCommand command, float duration = 0.1f)
    {
        StartCoroutine(CommandCR(command, duration));
    }

    IEnumerator CommandCR(AttackCommand command, float duration)
    {
        yield return WaitForAttack();
        attackInProgress = true;
        command.StartExecute(combat);
        while(duration > 0)
        {
            command.WhileExecute(combat);
            duration -= Time.deltaTime;
            yield return null;
        }
        command.EndExecute(combat);
        yield return null;
        attackInProgress = false;
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitWhile(()=>attackInProgress);
    }

    void LookAtATarget()
    {
        if (agent.target == null)
            return;

        Vector3 lookAt = agent.target.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }

}

[System.Serializable]
public class NPCAttack
{
    public string name;
    public AttackCommand command;

    [MinMaxRange(0,180)]
    public RangedFloat frequency;

    [MinMaxRange(0,60)]
    public RangedFloat duration;


    public bool isValid = true;
}

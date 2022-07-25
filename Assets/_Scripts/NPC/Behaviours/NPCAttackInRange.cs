using Corrupted;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CombatController))]
public class NPCAttackInRange : NPCBehaviour
{

    public NPCState attackState;


    //public LayerMask layer;
    public FloatVariable radius;
    public FloatVariable frequency;

    CombatController combat;
    Coroutine coroutine;

    private void Awake()
    {
        combat = GetComponent<CombatController>();
    }


    public override void OnEnd()
    {
        if(coroutine!=null)StopCoroutine(coroutine);
    }

    public override void OnStart()
    {
        coroutine = StartCoroutine(CheckForEnemy());
    }

    public override void OnUpdate()
    {
    }

    IEnumerator CheckForEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(frequency);
            combat.SurveyPointLocalSpace(Vector3.zero, radius, (Health h) =>
            {
                if(h.team != agent.team)
                    agent.SetState(attackState);
            });
        }
    }
}

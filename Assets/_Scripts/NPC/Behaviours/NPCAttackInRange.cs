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

    bool isOnScreen = false;
    bool isActive = false;

    private void Awake()
    {
        combat = GetComponent<CombatController>();
    }


    public override void OnEnd()
    {
        StopSearch();
        isActive = false;
    }

    public override void OnStart()
    {
        if(isOnScreen)
            StartSearch();
        isActive = true;
    }

    public override void OnUpdate()
    {
    }

    void StartSearch()
    {
        if (coroutine != null) return;
        coroutine = StartCoroutine(CheckForEnemy());
    }

    void StopSearch()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
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

    private void OnBecameVisible()
    {
        isOnScreen = true;
        if (isActive)
            StartSearch();

    }

    private void OnBecameInvisible()
    {
        isOnScreen = false;
        StopSearch();
    }
}

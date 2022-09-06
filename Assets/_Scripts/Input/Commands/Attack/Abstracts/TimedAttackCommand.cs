using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedAttackCommand : AttackCommand
{

    public System.Action<CombatController, float> OnTimedBegin, OnWhileTimed, OnTimedEnd;


    [Header("Timed Settings")]
    public FloatVariable duration;
    public FloatVariable timeStep;

    protected float currentTime;

    public bool isTiming
    {
        get; protected set;
    }


    public float timeCompletion => currentTime / duration;

    public virtual float damagePerSecond => Damage / duration;

    public virtual float damagePerTimestep => damagePerSecond * timeStep;

    public override void StartExecute(CombatController t)
    {
        if (isTiming)
            return;
        currentTime = 0;
        t.StartCoroutine(RunTimedAttack(t));
    }

    IEnumerator RunTimedAttack(CombatController t)
    {
        BeginAttack(t, timeCompletion);
        isTiming = true;
        OnTimedBegin?.Invoke(t, timeCompletion);
        while (currentTime < duration)
        {
            currentTime += timeStep;
            WhileAttack(t, timeCompletion);
            OnWhileTimed?.Invoke(t, timeCompletion);
            yield return new WaitForSeconds(timeStep);
        }
        EndAttack(t, timeCompletion);
        OnTimedEnd?.Invoke(t, timeCompletion);
        currentTime = 0;
        isTiming = false;
    }


    public override void WhileExecute(CombatController t)
    {

    }

    public override void EndExecute(CombatController t)
    {

    }


    public abstract void BeginAttack(CombatController t, float timeCompletion);

    public abstract void EndAttack(CombatController t, float timeCompletion);

    public abstract void WhileAttack(CombatController t, float timeCompletion);

}


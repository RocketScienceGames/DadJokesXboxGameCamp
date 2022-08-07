using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChargedAttackCommand : AttackCommand
{

    public System.Action<CombatController, float> OnChargeBegin, OnWhileCharge, OnChargeRelease;


    [Header("Charged Settings")]
    public FloatVariable minChargeTime;
    public FloatVariable maxChargeTime;
    public FloatVariable minDamage, maxDamage;
    public BoolVariable attackOnFullCharge;

    protected float currentCharge;

    public bool isCharging
    {
        get; protected set;
    }
    bool hasFired;


    public float chargeAmount => currentCharge / maxChargeTime;

    public float chargeDamage => GetChargeDamage(chargeAmount);

    public override void StartExecute(CombatController t)
    {
        currentCharge = 0;
        hasFired = false;
    }

    public override void WhileExecute(CombatController t)
    {
        if(currentCharge > minChargeTime && isCharging == false)
        {
            BeginCharge(t, chargeAmount);
            isCharging = true;
            OnChargeBegin?.Invoke(t, chargeAmount);
        }
        currentCharge += Time.deltaTime;
        if (isCharging)
        {
            WhileCharge(t, chargeAmount);
            OnWhileCharge?.Invoke(t, chargeAmount);
        }
        if (attackOnFullCharge == false)
            return;
        if (currentCharge > maxChargeTime && isCharging && hasFired == false)
        {
            ReleaseCharge(t, 1);
            OnChargeRelease?.Invoke(t, chargeAmount);
            isCharging = false;
            hasFired = true;
        }
    }

    public override void EndExecute(CombatController t)
    {
        if (currentCharge > minChargeTime && isCharging && hasFired == false)
        {
            ReleaseCharge(t, chargeAmount);
            OnChargeRelease?.Invoke(t, chargeAmount);
        }
        currentCharge = 0;
        isCharging = false;
        hasFired = false;
    }

    public float GetChargeDamage(float chargeAmount)
    {
        return ((maxDamage - minDamage) * chargeAmount) + minDamage;
    }

    public abstract void BeginCharge(CombatController t, float chargeAmount);

    public abstract void ReleaseCharge(CombatController t, float chargeAmount);

    public abstract void WhileCharge(CombatController t, float chargeAmount);

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedAttackAbilityUI : AttackAbilityUIListener
{
    // Start is called before the first frame update
    void OnEnable()
    {
        attack.OnChargeBegin += OnChargeBegin;
        attack.OnChargeRelease += OnChargeRelease;
        attack.OnWhileCharge += OnWhileCharge;
    }

    private void OnDisable()
    {
        attack.OnChargeBegin -= OnChargeBegin;
        attack.OnChargeRelease -= OnChargeRelease;
        attack.OnWhileCharge -= OnWhileCharge;
    }

    private void OnWhileCharge(CombatController arg1, float arg2)
    {
        SetFillAmount(arg2);
    }

    private void OnChargeRelease(CombatController arg1, float arg2)
    {
        view.view.fillClockwise = !view.view.fillClockwise;
        SetFillAmount(arg2);
    }

    private void OnChargeBegin(CombatController arg1, float arg2)
    {
        view.view.fillClockwise = !view.view.fillClockwise;
        SetFillAmount(0);
    }
}

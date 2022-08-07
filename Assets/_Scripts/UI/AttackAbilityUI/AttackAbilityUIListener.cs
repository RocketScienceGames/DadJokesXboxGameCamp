using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AttackAbilityUI))]
public abstract class AttackAbilityUIListener : MonoBehaviour
{

    protected AttackAbilityUI view;

    protected PlayerAttackCommandListener listener => view.listener;

    protected ChargedAttackCommand attack => view.attack as ChargedAttackCommand;

    protected void Awake()
    {
        view = GetComponent<AttackAbilityUI>();
    }

    public void SetFillAmount(float fillAmount)
    {
        view.SetFillAmount(fillAmount);
    }
}

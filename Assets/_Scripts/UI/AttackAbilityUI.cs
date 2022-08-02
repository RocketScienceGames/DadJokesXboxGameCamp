using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackAbilityUI : MonoBehaviour
{

    public PlayerAttackCommandListener listener;
    public CorruptedCommand<CombatController> attack;

    public Image view;

    public bool inCooldown = false;


    // Start is called before the first frame update
    void Start()
    {
        listener.OnCommandStart += OnAttackStart;
        listener.OnCommandEnd += OnAttackEnd;

    }

    private void OnDestroy()
    {
        listener.OnCommandStart -= OnAttackStart;
        listener.OnCommandEnd -= OnAttackEnd;
    }

    private void OnAttackEnd(CorruptedCommand<CombatController> obj)
    {
        //throw new NotImplementedException();
    }

    private void OnAttackStart(CorruptedCommand<CombatController> obj)
    {
        if (inCooldown)
            return;
        if(obj == attack)
            StartCoroutine(UICooldown(1f, 0.1f));
    }

    IEnumerator UICooldown(float cooldownTime, float stepRate)
    {
        inCooldown = true;
        float timer = cooldownTime;
        while(timer > 0)
        {
            timer -= stepRate;
            view.fillAmount = cooldownTime - (timer / cooldownTime);
            yield return new WaitForSeconds(stepRate);
        }
        inCooldown = false;
    }

    
}

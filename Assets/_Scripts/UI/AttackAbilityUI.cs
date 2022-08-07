using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackAbilityUI : MonoBehaviour
{

    public PlayerAttackCommandListener listener;
    public AttackCommand attack;

    public Image view;

    [Range(0f,1f)]
    public float fillAmount;

    public bool inCooldown = false;


    // Start is called before the first frame update
    void Start()
    {
        //listener.OnCommandStart += OnAttackStart;
        listener.OnCommandEnd += OnAttackEnd;

    }

    private void OnDestroy()
    {
        //listener.OnCommandStart -= OnAttackStart;
        listener.OnCommandEnd -= OnAttackEnd;
    }

    private void OnAttackEnd(CorruptedCommand<CombatController> obj)
    {
        if (inCooldown)
            return;
        AttackCommand ac = obj as AttackCommand;
        if (obj == attack)
            StartCoroutine(UICooldown(ac.cooldown, 0.01f));
        //throw new NotImplementedException();
    }

    public void SetFillAmount(float fillAmount)
    {
        this.fillAmount = fillAmount;
        view.fillAmount = fillAmount;
    }


    IEnumerator UICooldown(float cooldownTime, float stepRate)
    {
        inCooldown = true;
        float timer = cooldownTime;
        view.fillAmount = 0;
        while(timer > 0)
        {
            fillAmount = (cooldownTime - timer) / cooldownTime;
            view.fillAmount = fillAmount;
            timer -= stepRate;
            yield return new WaitForSeconds(stepRate);
        }
        view.fillAmount = 1;
        inCooldown = false;
    }

    
}

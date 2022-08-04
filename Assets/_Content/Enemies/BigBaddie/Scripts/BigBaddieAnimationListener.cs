using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NPCAgent))]
public class BigBaddieAnimationListener : MonoBehaviour
{

    public Animator animator;
    public NPCState attackState;

    Health h;
    NPCAgent npc;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        h = GetComponent<Health>();
        npc = GetComponent<NPCAgent>();
        h.OnTakeDamage.AddListener(GetHit);
        h.OnDeath.AddListener(Die);
        npc.OnStart += OnNewState;
    }

    private void OnDisable()
    {
        h.OnTakeDamage.RemoveListener(GetHit);
        h.OnDeath.RemoveListener(Die);
        npc.OnStart -= OnNewState;
    }

    private void OnNewState(NPCState obj)
    {
        animator.SetBool("Attack", obj == attackState);
    }

    private void Die()
    {
        animator.SetBool("Dead", true);
        animator.SetTrigger("Die");
    }

    private void GetHit()
    {
        animator.SetTrigger("GetHit");
    }

}

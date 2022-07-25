using Corrupted;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NPCMovement))]
[RequireComponent(typeof(Health))]
public class NPCStaggerOnTakeDamage : MonoBehaviour
{

    public FloatVariable staggerDelay = 2f;

    NPCMovement movement;
    Health health;

    Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<NPCMovement>();
        health = GetComponent<Health>();
        health.OnTakeDamage.AddListener(OnTakeDamage);
    }

    private void OnTakeDamage()
    {
        coroutine = StartCoroutine(Stagger());
    }

    IEnumerator Stagger()
    {
        movement.enabled = false;
        yield return new WaitForSeconds(staggerDelay);
        movement.enabled = true;
    }
}

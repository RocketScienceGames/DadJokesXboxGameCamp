using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Corrupted.CorruptedCommandListener<CombatController>;

[RequireComponent(typeof(CombatController))]
[RequireComponent(typeof(RigidbodyMovement))]
public class PlayerAnimator : MonoBehaviour
{

    public Animator anim;

    CombatController combatController;
    RigidbodyMovement rigidbodyMovement;

    // Start is called before the first frame update
    void Start()
    {
        combatController = GetComponent<CombatController>();
        rigidbodyMovement = GetComponent<RigidbodyMovement>();
        PlayerAttackCommandListener.OnCommandListenerStart += OnAttack;
        //CombatController.OnAttack += OnAttack;
    }

   

    private void OnDestroy()
    {
        //CombatController.OnAttack -= OnAttack;
        PlayerAttackCommandListener.OnCommandListenerStart -= OnAttack;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("MoveSpeed", rigidbodyMovement.movement.magnitude);
        anim.SetBool("IsGrounded", rigidbodyMovement.isGrounded);
    }

    private void OnAttack(CombatController obj, CommandListener cl)
    {
        if (obj == combatController)
            anim.SetTrigger(cl.name);
    }
}

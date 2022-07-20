using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour
{

    public FloatVariable health;
    public FloatVariable maxHealth;
    public FloatVariable explosiveForceMultiplier = 1f;

    public UnityEvent OnTakeDamage, OnDeath;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float TakeDamage(DamageInfo damgae)
    {
        health -= damgae.damage;
        rb.AddExplosionForce(damgae.damage * explosiveForceMultiplier, damgae.position, damgae.radius);
        OnTakeDamage?.Invoke();
        if (health <= 0)
            Die();
        return health;
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }



}

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
    public BoolVariable startFullHealth;

    public IntVariable team;

    public UnityEvent OnTakeDamage, OnDeath;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (startFullHealth)
            health = maxHealth;
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

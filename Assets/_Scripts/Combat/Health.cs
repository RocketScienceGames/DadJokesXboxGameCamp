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

    public FloatVariable DestroyOnDeathDelay = -1;

    public IntVariable team;

    public UnityEvent OnTakeDamage, OnDeath;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (startFullHealth)
            health.Value = maxHealth;
    }

    public float TakeDamage(DamageInfo damage)
    {
        //Debug.Log($"{gameObject.name}: {damage.damage} damage taken from {damage.source.name}");
        health.Value -= damage.damage;
        rb.AddExplosionForce(damage.damage * explosiveForceMultiplier, damage.position, damage.radius);
        OnTakeDamage?.Invoke();
        if (health <= 0)
            Die();
        return health;
    }

    public void Die()
    {
        OnDeath?.Invoke();
        if(DestroyOnDeathDelay > 0)
        {
            Destroy(gameObject, DestroyOnDeathDelay);
        }
    }



}

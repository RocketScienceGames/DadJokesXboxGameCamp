using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DestroyOnDeath : MonoBehaviour
{
    public FloatVariable delayInSeconds;
    Health h;

    // Start is called before the first frame update
    void Start()
    {
        h = GetComponent<Health>();
        h.OnDeath.AddListener(Die);
    }

    void Die()
    {
        Destroy(gameObject, delayInSeconds);
    }
}

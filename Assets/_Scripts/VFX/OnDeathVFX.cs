using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Health))]
public class OnDeathVFX : MonoBehaviour
{
    public GameObjectVariable prefab;
    public float lifespan;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath.AddListener(PlayVFX);
    }

    private void OnDestroy()
    {
        if (health != null)
            health.OnDeath.RemoveListener(PlayVFX);
    }

    public void PlayVFX()
    {
        VFXManager.Spawn(prefab, lifespan, null, transform.position, transform.rotation);
    }

}

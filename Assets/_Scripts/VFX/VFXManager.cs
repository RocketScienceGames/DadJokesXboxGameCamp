using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

public class VFXManager : Singleton<VFXManager>
{

    public List<VFXInstance> activeVFX;


    // Update is called once per frame
    void Update()
    {
        foreach(VFXInstance vfx in activeVFX)
        {
            vfx.Update();
        }
    }

    public void SpawnVFX(GameObject prefab, float lifespan, Transform parent = null, Vector3? position = null, Quaternion? rotation = null, System.Action<VFXInstance> OnUpdate = null, System.Action<VFXInstance> OnDestroy = null)
    {
        VFXInstance vfx = new VFXInstance();
        vfx.lifespan = lifespan;

        vfx.gameObject = Instantiate(prefab, parent, parent == null);

        vfx.gameObject.transform.localPosition = position.GetValueOrDefault(prefab.transform.localPosition);
        vfx.gameObject.transform.localRotation = rotation.GetValueOrDefault(prefab.transform.localRotation);

        if (OnUpdate != null)vfx.OnUpdate += OnUpdate;
        if (OnDestroy != null) vfx.OnDestroy += OnDestroy;

        vfx.OnDestroy += (VFXInstance vfx) =>
        {
            StartCoroutine(RemoveActiveVFX(vfx));
        };
    }


    IEnumerator RemoveActiveVFX(VFXInstance vfx)
    {
        Destroy(vfx.gameObject);
        yield return null;
        activeVFX.Remove(vfx);
    }

    public static void Spawn(GameObject prefab, float lifespan, Transform parent = null, Vector3? position = null, Quaternion? rotation = null, System.Action<VFXInstance> OnUpdate = null, System.Action<VFXInstance> OnDestroy = null)
    {
        if(Instance == null)
        {
            Debug.LogError("VFXManager: Failed to spawn vfx because manager is not instantiated!");
            return;
        }
        Instance.SpawnVFX(prefab, lifespan, parent, position, rotation, OnUpdate, OnDestroy);
    }

}

[System.Serializable]
public struct VFXInstance
{
    public GameObject gameObject;
    public Transform parent;
    public Vector3? position;
    public Vector3? rotation;
    public float lifespan;
    public System.Action<VFXInstance> OnUpdate;
    public System.Action<VFXInstance> OnDestroy;


    public void Update()
    {
        OnUpdate?.Invoke(this);
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
            OnDestroy?.Invoke(this);
    }
}

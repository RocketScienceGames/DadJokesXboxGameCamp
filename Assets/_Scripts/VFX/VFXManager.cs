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
            vfx.Update(Time.deltaTime);
        }
    }

    public VFXInstance SpawnVFX(GameObject prefab, float lifespan, Transform parent = null, Vector3? position = null, Quaternion? rotation = null, System.Action<VFXInstance> OnUpdate = null, System.Action<VFXInstance> OnDestroy = null)
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
            //Debug.Log($"VFXManager: Active VFX {prefab.name} has exceeded its lifespan and was destroyed.");
            RemoveVFX(vfx);
        };

        activeVFX.Add(vfx);
        return vfx;
    }

    public void RemoveVFX(VFXInstance instance)
    {
        StartCoroutine(RemoveActiveVFX(instance));
    }


    IEnumerator RemoveActiveVFX(VFXInstance vfx)
    {
        if (vfx.gameObject == null || activeVFX.Contains(vfx) == false)
            yield break;
        Destroy(vfx.gameObject);
        yield return null;
        activeVFX.Remove(vfx);
    }

    public static VFXInstance Spawn(GameObject prefab, float lifespan, Transform parent = null, Vector3? position = null, Quaternion? rotation = null, System.Action<VFXInstance> OnUpdate = null, System.Action<VFXInstance> OnDestroy = null)
    {
        if(Instance == null)
        {
            Debug.LogError("VFXManager: Failed to spawn vfx because manager is not instantiated!");
            return null;
        }
        return Instance.SpawnVFX(prefab, lifespan, parent, position, rotation, OnUpdate, OnDestroy);
    }

    public static void Remove(VFXInstance instance)
    {
        if (Instance == null)
        {
            Debug.LogError("VFXManager: Failed to remove vfx because manager is not instantiated!");
            return;
        }
        Instance.RemoveVFX(instance);
    }

}

[System.Serializable]
public class VFXInstance
{
    public GameObject gameObject;
    public Transform parent;
    public Vector3? position;
    public Vector3? rotation;
    public float lifespan;
    public float timeElapsed { get; protected set; }
    public System.Action<VFXInstance> OnUpdate;
    public System.Action<VFXInstance> OnDestroy;


    public void Update(float deltaTime)
    {
        OnUpdate?.Invoke(this);
        timeElapsed += deltaTime;
        //Debug.Log($"VFXManager: Update {gameObject.name}, lifespan remaining {lifespan - timeElapsed}", gameObject);
        if (timeElapsed > lifespan)
            OnDestroy?.Invoke(this);
    }
}

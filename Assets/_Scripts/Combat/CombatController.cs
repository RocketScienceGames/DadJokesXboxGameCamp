using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

public class CombatController : MonoBehaviour
{

    public static System.Action<CombatController> OnAttack;
    public static System.Action<CombatController, Health> OnAttackHit;


    static List<DamageGizmo> gizmos = new List<DamageGizmo>();

    public Health[] AttackAtPointLocalSpace(Vector3 localPos, float radius, float damage, System.Action<Health> OnHit = null)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return AttackAtPointWorldSpace(worldPos, radius, damage, this, OnHit);
    }

    public Health[] SurveyPointLocalSpace(Vector3 localPos, float radius, System.Action<Health> OnHit = null)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return SurveyPointWorldSpace(worldPos, radius, OnHit);
    }

    public Health[] SurveyPointLocalSpace(Vector3 localPos, float radius, LayerMask layer, System.Action<Health> OnHit = null)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return SurveyPointWorldSpace(worldPos, radius, layer, OnHit);
    }

    public static Health[] AttackAtPointWorldSpace(Vector3 pos, float radius, float damage, CombatController source, System.Action<Health> OnHit = null)
    {
        //Health[] hits = pos.GetOverlapSphere<Health>(radius, layer);
        DamageInfo info = new DamageInfo { damage = damage, position = pos , source = source, radius = radius};
        gizmos.Add(new DamageGizmo(pos, radius, 3f, Color.blue, true));
        OnAttack?.Invoke(source);
        return SurveyPointWorldSpace(pos, radius, (Health h)=>
        {
            h.TakeDamage(info);
            OnHit?.Invoke(h);
            gizmos.Add(new DamageGizmo(h.transform.position, 0.5f, 3f, Color.red));
            OnAttackHit?.Invoke(source, h);
        });
    }

    public static Health[] SurveyPointWorldSpace(Vector3 pos, float radius, System.Action<Health> OnHit = null)
    {
        Health[] hits = pos.GetOverlapSphere<Health>(radius);
        //Debug.Log($"CombatController: Survey hit {hits.Length} instances on layer {LayerMask.LayerToName(0 << layer)}");
        foreach(Health h in hits)
        {
            OnHit?.Invoke(h);
        }
        return hits;
    }

    public static Health[] SurveyPointWorldSpace(Vector3 pos, float radius, LayerMask layer, System.Action<Health> OnHit = null)
    {
        Health[] hits = pos.GetOverlapSphere<Health>(radius, layer);
        Debug.Log($"CombatController: Survey hit {hits.Length} instances on layer {layer}");
        foreach (Health h in hits)
        {
            OnHit?.Invoke(h);
        }
        return hits;
    }


    private void OnDrawGizmosSelected()
    {
        List<DamageGizmo> toRemove = new List<DamageGizmo>();
        foreach(DamageGizmo gizmo in gizmos)
        {
            Gizmos.color = gizmo.color;
            if (gizmo.wireframe)
                Gizmos.DrawWireSphere(gizmo.pos, gizmo.radius);
            else
                Gizmos.DrawSphere(gizmo.pos, gizmo.radius);
            gizmo.lifespan -= Time.deltaTime;
            if (gizmo.lifespan <= 0)
                toRemove.Add(gizmo);
        }
        foreach(DamageGizmo gizmo in toRemove)
        {
            gizmos.Remove(gizmo);
        }
    }


}

[System.Serializable]
public struct DamageInfo
{
    public float damage;
    public Vector3 position;
    public float radius;
    public CombatController source;
}



public class DamageGizmo
{
    public Vector3 pos;
    public float radius;
    public Color color;
    public float lifespan;
    public bool wireframe;

    public DamageGizmo(Vector3 pos, float radius, float lifespan, Color color, bool wireframe = false)
    {
        this.pos = pos;
        this.radius = radius;
        this.color = color;
        this.lifespan = lifespan;
        this.wireframe = wireframe;
    }
}

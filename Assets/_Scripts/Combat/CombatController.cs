using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

[RequireComponent(typeof(Health))]
public class CombatController : MonoBehaviour
{

    public static System.Action<CombatController> OnAttack;
    public static System.Action<CombatController, Health> OnAttackHit;


    static List<DamageGizmo> gizmos = new List<DamageGizmo>();

    public int team => health.team;

    public Health health
    {
        get; protected set;
    }

    public Rigidbody rigidbody
    {
        get; protected set;
    }

    private void Awake()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody>();
    }

    #region SphereCast

    public RayHit<Health>[] AttackAtDirectionLocalSpace(Vector3 localPos, Vector3 direction, float radius, float distance, float damage, System.Action<RayHit<Health>> OnHit = null)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return AttackAtDirectionWorldSpace(worldPos, direction, radius, distance, damage, this, OnHit);
    }

    public RayHit<Health>[] SurveyDirectionLocalSpace(Vector3 localPos, Vector3 direction, float radius, float distance, System.Action<RayHit<Health>> OnHit = null)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return SurveyDirectionWorldSpace(worldPos, direction, radius, distance, OnHit);
    }

    public RayHit<Health>[] SurveyDirectionLocalSpace(Vector3 localPos, Vector3 direction, float radius, float distance, LayerMask layer, System.Action<RayHit<Health>> OnHit = null)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return SurveyDirectionWorldSpace(worldPos, direction, radius, layer, OnHit);
    }

    public static RayHit<Health>[] AttackAtDirectionWorldSpace(Vector3 pos, Vector3 direction, float radius, float distance, float damage, CombatController source, System.Action<RayHit<Health>> OnHit = null)
    {
        //Health[] hits = pos.GetOverlapSphere<Health>(radius, layer);
        DamageInfo info = new DamageInfo { damage = damage, position = pos, source = source, radius = radius };
        gizmos.Add(new DamageGizmo(pos, radius, 3f, Color.blue, true));
        OnAttack?.Invoke(source);
        return SurveyDirectionWorldSpace(pos, direction, radius, distance, (RayHit<Health> h) =>
        {
            if (source.team == h.value.team.Value)
                return;
            info.position = h.hit.point;
            h.value.TakeDamage(info);
            OnHit?.Invoke(h);
            gizmos.Add(new DamageGizmo(h.value.transform.position, 0.5f, 3f, Color.red));
            OnAttackHit?.Invoke(source, h);
        });
    }

    public static RayHit<Health>[] SurveyDirectionWorldSpace(Vector3 pos, Vector3 direction, float radius, float distance = Mathf.Infinity, System.Action<RayHit<Health>> OnHit = null)
    {
        RayHit<Health>[] hits = pos.GetSphereCast<Health>(direction, radius, distance);
        //Debug.Log($"CombatController: Survey hit {hits.Length} instances on layer {LayerMask.LayerToName(0 << layer)}");
        foreach (RayHit<Health> h in hits)
        {
            OnHit?.Invoke(h);
        }
        return hits;
    }

    public static RayHit<Health>[] SurveyDirectionWorldSpace(Vector3 pos, Vector3 direction, float radius, float distance, LayerMask layer, System.Action<Health> OnHit = null)
    {
        RayHit<Health>[] hits = pos.GetSphereCast<Health>(direction, radius, distance, layer);
        Debug.Log($"CombatController: Survey hit {hits.Length} instances on layer {layer}");
        foreach (RayHit<Health> h in hits)
        {
            OnHit?.Invoke(h);
        }
        return hits;
    }

    #endregion


    #region OverlapSphere

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
            if (source.team == h.team.Value)
                return;
            Debug.Log($"CombatController: Attack from {source.name} hit {h.transform.name}", h.transform);
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

    #endregion


    #region LinearProjectile

    public RayHit<Health>[] AttackLinearProjectileSelfLocalSpace(Vector3 localPos, Vector3 direction, float radius, float distance, float damage, System.Action<RayHit<Health>> OnHit = null)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return AttackLinearProjectileWorldSpace(rigidbody, worldPos, direction, radius, distance, damage, this, OnHit);
    }

    public RayHit<Health>[] AttackLinearProjectileLocalSpace(Rigidbody projectile, Vector3 localPos, Vector3 direction, float radius, float distance, float damage, System.Action<RayHit<Health>> OnHit = null)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return AttackLinearProjectileWorldSpace(projectile, worldPos, direction, radius, distance, damage, this, OnHit);
    }

    public static RayHit<Health>[] AttackLinearProjectileWorldSpace(Rigidbody projectile, Vector3 pos, Vector3 direction, float radius, float distance, float damage, CombatController source, System.Action<RayHit<Health>> OnHit = null)
    {
        //Health[] hits = pos.GetOverlapSphere<Health>(radius, layer);
        DamageInfo info = new DamageInfo { damage = damage, position = pos, source = source, radius = radius };
        gizmos.Add(new DamageGizmo(pos, radius, 3f, Color.blue, true));
        OnAttack?.Invoke(source);
        projectile.transform.position += (direction.normalized * distance) + (Vector3.up * 0.1f);
        return SurveyDirectionWorldSpace(pos, direction, radius, distance, (RayHit<Health> h) =>
        {
            if (source.team == h.value.team.Value)
                return;
            info.position = h.hit.point;
            h.value.TakeDamage(info);
            OnHit?.Invoke(h);
            gizmos.Add(new DamageGizmo(h.value.transform.position, 0.5f, 3f, Color.red));
            OnAttackHit?.Invoke(source, h);
        });
    }

    #endregion

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

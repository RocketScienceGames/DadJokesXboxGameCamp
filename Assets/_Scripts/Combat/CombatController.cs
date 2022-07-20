using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

public class CombatController : MonoBehaviour
{

    static List<DamageGizmo> gizmos = new List<DamageGizmo>();

    public Health[] AttackAtPointLocalSpace(Vector3 localPos, float radius, float damage)
    {
        Vector3 worldPos = transform.localToWorldMatrix.MultiplyPoint(localPos);
        return AttackAtPointWorldSpace(worldPos, radius, damage, this);
    }

    public static Health[] AttackAtPointWorldSpace(Vector3 pos, float radius, float damage, CombatController source)
    {
        Health[] hits = pos.GetOverlapSphere<Health>(radius);
        DamageInfo info = new DamageInfo { damage = damage, position = pos , source = source, radius = radius};
        gizmos.Add(new DamageGizmo(pos, radius, 3f, Color.blue, true));
        foreach(Health h in hits)
        {
            h.TakeDamage(info);
            gizmos.Add(new DamageGizmo(h.transform.position, 0.5f, 3f, Color.red));
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

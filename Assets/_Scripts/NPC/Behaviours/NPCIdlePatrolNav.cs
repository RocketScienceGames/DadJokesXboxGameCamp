using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NPCNavMeshMovement))]
public class NPCIdlePatrolNav : NPCBehaviour
{

    public Transform[] patrolPoints;

    int direction = 1;
    int index = 0;

    NPCNavMeshMovement npcMovement;

    private void Awake()
    {
        npcMovement = GetComponent<NPCNavMeshMovement>();
    }

    public override void OnEnd()
    {
        npcMovement.StopMovement();
    }

    public override void OnStart()
    {
        MoveToNextPoint();
    }

    public override void OnUpdate()
    {
        
    }

    public void MoveToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;
        Vector3 pos = patrolPoints[index].position;
        index = NextIndex(index);

        npcMovement.MoveTo(pos, () => MoveToNextPoint());
    }

    int NextIndex(int i)
    {
        if (i <= 0)
            direction = 1;
        if (i >= patrolPoints.Length - 1)
            direction = -1;
        return i + direction;
    }

}

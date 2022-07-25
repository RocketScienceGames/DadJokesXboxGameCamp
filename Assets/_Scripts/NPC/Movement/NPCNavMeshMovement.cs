using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCNavMeshMovement : NPCMovement
{


    NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    public override bool DestinationReached(Vector3 destination)
    {
        return Vector3.Distance(transform.position, nav.destination) <= nav.stoppingDistance;
    }

    public override bool ExecuteMove(Vector3 destination)
    {
        return nav.SetDestination(destination);
    }
}

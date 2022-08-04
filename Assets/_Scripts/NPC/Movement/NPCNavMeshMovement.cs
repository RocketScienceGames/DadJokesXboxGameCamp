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

    private void OnEnable()
    {
        nav.enabled = true;
        if (nav.isOnNavMesh == false)
            nav.enabled = false;
    }

    private void OnDisable()
    {
        nav.enabled = false;
    }

    public override bool DestinationReached(Vector3 destination)
    {
        return Vector3.Distance(transform.position, nav.destination) <= nav.stoppingDistance;
    }

    public override bool ExecuteMove(Vector3 destination)
    {
        nav.enabled = true;
        return nav.SetDestination(destination);
    }

    public override void StopMovement(bool cancelRequests = true)
    {
        base.StopMovement(cancelRequests);
        nav.enabled = false;
    }
}

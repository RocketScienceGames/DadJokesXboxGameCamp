using Corrupted;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NPCNavMeshMovement))]
public class NPCAttackChasePlayer : NPCBehaviour
{



    NPCNavMeshMovement npcMovement;

    bool attackInProgress = false;

    private void Awake()
    {
        npcMovement = GetComponent<NPCNavMeshMovement>();
    }


    public override void OnEnd()
    {

    }

    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {
        npcMovement.ExecuteMove(agent.target.position);
    }




}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NPCAgent))]
public abstract class NPCBehaviour : MonoBehaviour
{

    protected NPCAgent agent;

    public NPCState requiredState;

    private void OnEnable()
    {
        if(agent == null)agent = GetComponent<NPCAgent>();
        agent.OnStart += OnStartBehavior;
        agent.OnEnd += OnEndBehaviour;
        agent.OnUpdate += OnUpdateBehaviour;
    }

    private void OnDisable()
    {
        agent.OnStart -= OnStartBehavior;
        agent.OnEnd -= OnEndBehaviour;
        agent.OnUpdate -= OnUpdateBehaviour;
    }

    private void OnUpdateBehaviour(NPCState obj)
    {
        if (obj == requiredState)
            OnUpdate();
    }

    private void OnEndBehaviour(NPCState obj)
    {
        if (obj == requiredState)
            OnEnd();
    }

    private void OnStartBehavior(NPCState obj)
    {
        if (obj == requiredState)
            OnStart();
    }

    public abstract void OnStart();

    public abstract void OnEnd();

    public abstract void OnUpdate();
}

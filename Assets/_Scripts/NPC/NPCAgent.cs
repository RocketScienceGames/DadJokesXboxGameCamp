using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAgent : MonoBehaviour
{

    public NPCState currentState;

    public IntVariable team;

    public System.Action<NPCState> OnUpdate, OnStart, OnEnd;
    public System.Action<NPCState, NPCState> OnStateTransition;


    private void Awake()
    {
        gameObject.IfHasComponent((Health h) =>
        {
            team = h.team;
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        if (currentState != null)
        {
            OnStart?.Invoke(currentState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate?.Invoke(currentState);
    }

    public void SetState(NPCState state)
    {
        if(currentState == null)
        {
            currentState = state;
            OnStart?.Invoke(currentState);
            return;
        }

        if (currentState == state)
            return;

        OnEnd?.Invoke(currentState);
        OnStateTransition?.Invoke(currentState, state);
        currentState = state;
        OnStart?.Invoke(currentState);
    }

}

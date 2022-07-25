using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCMovement : MonoBehaviour
{

    public bool queueRequests = false;

    public bool moveInProgress
    {
        get; protected set;
    }

    public abstract bool DestinationReached(Vector3 destination);

    public abstract bool ExecuteMove(Vector3 destination);

    Coroutine movement;
    Queue<MoveRequest> queuedRequests = new Queue<MoveRequest>();

    protected virtual IEnumerator MoveToCR(Vector3 pos, System.Action OnDestinationReached = null)
    {
        bool success = ExecuteMove(pos);
        if (success)
        {
            moveInProgress = true;
            yield return new WaitUntil(()=> DestinationReached(pos));
            moveInProgress = false;
            OnDestinationReached?.Invoke();
            if(queueRequests && queuedRequests.Count > 0)
            {
                MoveRequest mr = queuedRequests.Dequeue();
                yield return MoveToCR(mr.destination, mr.OnDestinationReached);
            }
        }
        else{
            Debug.LogError("NPCMovement: Execute move invalid!", gameObject);
        }
    }

    public virtual void MoveTo(Vector3 pos, System.Action OnDestinationReached = null)
    {
        if (queueRequests && moveInProgress)
        {
            queuedRequests.Enqueue(new MoveRequest { destination = pos, OnDestinationReached = OnDestinationReached });
        }
        else
        {
            if(movement != null)StopCoroutine(movement);
            movement = StartCoroutine(MoveToCR(pos, OnDestinationReached));
        }
    }

    public virtual void StopMovement(bool cancelRequests = true)
    {
        if(movement != null)StopCoroutine(movement);
        if (cancelRequests)
            queuedRequests.Clear();
        moveInProgress = false;
    }

    public struct MoveRequest
    {
        public Vector3 destination;
        public System.Action OnDestinationReached;
    }

}

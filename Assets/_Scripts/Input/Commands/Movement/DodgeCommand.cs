using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

[CreateAssetMenu(fileName = "Dodge Command", menuName = "DadJokes/Input/Dodge")]
public class DodgeCommand : CorruptedCommand<RigidbodyMovement>, IFixedUpdateListener<RigidbodyMovement>
{


    public FloatVariable force;


    bool isPressed;

    public override void EndExecute(RigidbodyMovement t)
    {

    }

    public override void StartExecute(RigidbodyMovement t)
    {
        if (t.movement.magnitude <= 0.1f)
            t.AddForce(t.transform.forward * force, ForceMode.Impulse);
        else
            t.AddForce(t.movement.normalized * force, ForceMode.Impulse);
    }

    public override void WhileExecute(RigidbodyMovement t)
    {
    }

    public void OnFixedUpdate(RigidbodyMovement t)
    {

    }
}

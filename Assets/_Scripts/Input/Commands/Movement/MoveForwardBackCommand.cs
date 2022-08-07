using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

[CreateAssetMenu(fileName = "MoveForwardBack", menuName = "DadJokes/Input/MoveForwardBack")]
public class MoveForwardBackCommand : CorruptedAxisCommand<RigidbodyMovement>, IFixedUpdateListener<RigidbodyMovement>
{

    [Header("Navigation")]
    public Vector3 direction;

    [Header("Movement")]
    public FloatVariable moveAcceleration;
    public FloatVariable depthBoundary;
    public FloatVariable maxSpeed;

    //[Header("Settings")]
    //public FloatVariable idleDrag;
    //public FloatVariable activeDrag;


    public override void OnStart(RigidbodyMovement t)
    {
        //t.drag = idleDrag;
    }

    public override void EndExecute(RigidbodyMovement t)
    {
        //t.AddForce(GetMoveVelocity(t) * -1f);
        //t.drag = idleDrag;
    }

    public override void StartExecute(RigidbodyMovement t)
    {
        //t.drag = activeDrag;
    }

    public override void WhileExecute(RigidbodyMovement t, float axis)
    {
        t.movement = direction.normalized * axis;
    }

    public void OnFixedUpdate(RigidbodyMovement t)
    {
        //t.Move(moveAcceleration);
        //ClampMoveVelocity(t, maxSpeed);
        ClampPosition(t, depthBoundary);
    }

    public Vector3 GetMoveVelocity(RigidbodyMovement t)
    {
        return t.velocity.Multiply(direction.normalized);
    }

    public Vector3 ClampMoveVelocity(RigidbodyMovement t, float speed)
    {
        Vector3 moveVelocity = GetMoveVelocity(t);
        Vector3 clampedMoveVelocity = Vector3.ClampMagnitude(moveVelocity, speed);
        Vector3 nonMoveVelocity = t.velocity - moveVelocity;

        t.velocity = nonMoveVelocity + clampedMoveVelocity;

        return t.velocity;
    }

    public Vector3 ClampPosition(RigidbodyMovement t, float depth)
    {
        Vector3 pos = t.position;
        if (Mathf.Abs(pos.z) > depth)
        {
            pos.z = Mathf.Sign(pos.z) * depth;
            t.position = pos;
            if (Mathf.Sign(t.movement.z) == Mathf.Sign(pos.z))
                t.movement = new Vector3(t.movement.x, t.movement.y, -t.movement.z);
        }
        return pos;
    }
}

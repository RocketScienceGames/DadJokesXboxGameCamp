using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

[CreateAssetMenu(fileName = "MoveLeftRight", menuName = "DadJokes/Input/MoveLeftRight")]
public class MoveLeftRightCommand : CorruptedAxisCommand<RigidbodyMovement>, IFixedUpdateListener<RigidbodyMovement>
{

    [Header("Navigation")]
    public Vector3 direction;

    [Header("Movement")]
    public FloatVariable moveAcceleration;
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
        //t.AddForce(GetMoveVelocity(t) * -1f, ForceMode.Impulse);
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
}

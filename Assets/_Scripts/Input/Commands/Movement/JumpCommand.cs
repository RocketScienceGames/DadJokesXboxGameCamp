using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;

[CreateAssetMenu(fileName = "Jump Command", menuName = "DadJokes/Input/Jump")]
public class JumpCommand : CorruptedCommand<RigidbodyMovement>, IFixedUpdateListener<RigidbodyMovement>
{

    public Vector3 direction;

    public FloatVariable force;

    public FloatVariable fallMulitplier;

    public FloatVariable airDrag;

    public LayerMask groundLayer;

    bool isPressed;

    public override void EndExecute(RigidbodyMovement t)
    {
        t.AddForce(-direction * force);
        isPressed = false;
    }

    public override void StartExecute(RigidbodyMovement t)
    {
        t.AddForce(direction * force);
        isPressed = true;
    }

    public override void WhileExecute(RigidbodyMovement t)
    {
        //t.AddForce(direction * (force - Physics.gravity.y));
    }

    public void OnFixedUpdate(RigidbodyMovement t)
    {
        t.isGrounded = Physics.Raycast(t.position, -direction.normalized, 0.2f, groundLayer);
        if (t.isGrounded == false)
            t.drag = airDrag;
        if (t.isGrounded)
            t.gravityScale = 0;
        else
        {
            t.drag = airDrag;
            t.gravityScale = 1;
            if (t.velocity.y < 0)
                t.gravityScale = fallMulitplier;
            else if (t.velocity.y > 0 && isPressed == false)
                t.gravityScale = fallMulitplier / 2;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Corrupted;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Jump Command", menuName = "DadJokes/Input/Jump")]
public class JumpCommand : CorruptedCommand<RigidbodyMovement>, IFixedUpdateListener<RigidbodyMovement>
{
    [Header("Jump Settings")]
    public float maxJumpHeight = 1f;
    public float maxJumpTime = 0.5f;
    public float fallMultiplier = 2f;

    [Header("Float Settings")]
    public float RideHeight;
    public float RideSpringStrength;
    public float RideSpringDamper;


    [Header("Collision Settings")]
    public LayerMask groundLayer;




    float gravity = -9.8f;
    float groundedGravity = -0.05f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    
    bool isJumping = false;


    void OnEnable()
    {
        SetupJumpValues();
    }

    [Button]
    void SetupJumpValues()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    public override void EndExecute(RigidbodyMovement t)
    {
        isJumpPressed = false;
    }

    public override void StartExecute(RigidbodyMovement t)
    {
        isJumpPressed = true;
    }

    public override void WhileExecute(RigidbodyMovement t)
    {
        HandleJump(t);
    }

    public void OnFixedUpdate(RigidbodyMovement t)
    {
        HandleIsGrounded(t);
        HandleGravity(t);
    }

    void HandleIsGrounded(RigidbodyMovement t)
    {
        t.isGrounded = t.position.IfRaycast(Vector3.down, 0.3f, (hit) =>
         {
             Vector3 velocity = t.velocity;
             Vector3 rayDir = Vector3.down;
             Vector3 otherVelocity = Vector3.zero;
             Rigidbody hitBody = hit.rigidbody;
             if (hitBody != null)
             {
                 otherVelocity = hitBody.velocity;
             }

             float rayDirVel = Vector3.Dot(rayDir, velocity);
             float otherDirVel = Vector3.Dot(rayDir, otherVelocity);

             float relVel = rayDirVel - otherDirVel;

             float x = hit.distance - RideHeight;
             float springForce = (x * RideSpringStrength) - (relVel * RideSpringDamper);

             t.AddForce(rayDir * springForce);
             if(hitBody != null)
             {
                 hitBody.AddForceAtPosition(rayDir * -springForce, hit.point);
             }
         },groundLayer);
        if(isJumping && t.isGrounded)
        {
            isJumping = false;
        }
    }

    void HandleGravity(RigidbodyMovement t)
    {
        bool isFalling = /*t.velocity.y <= 0 ||*/ isJumpPressed == false;
        float modifier = isFalling ? fallMultiplier : 1;

        //apply the poroper gravity if the player is grounded or not
        if (t.isGrounded)
        {
            t.velocity = new Vector3(t.velocity.x, groundedGravity, t.velocity.z);
        }
        else
        {
            float previousYVelocity = t.velocity.y;
            float newYVelocity = t.velocity.y + (gravity * modifier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) / 2;
            t.velocity = new Vector3(t.velocity.x, nextYVelocity, t.velocity.z);
        }
    }

    void HandleJump(RigidbodyMovement t)
    {
        if (isJumping == false && t.isGrounded)
        {
            isJumping = true;
            t.AddForce(Vector3.up * initialJumpVelocity * t.mass * 0.5f, ForceMode.Impulse);
        }
    }
}

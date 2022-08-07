using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMovement : MonoBehaviour
{

    [Header("Movement")]
    public FloatVariable maxSpeed = 8;
    public FloatVariable acceleration = 200;

    public AnimationCurve AccelerationFactorFromDot;

    [Header("Physics")]
    public FloatVariable maxAccelForce = 150;
    public Vector3 forceScale;

    [Range(-1, 1)]
    public float neededAccelFloat;
      
    Rigidbody rb;

    public bool isGrounded;


    protected Vector3 _movement;
    public Vector3 movement
    {
        get
        {
            return _movement;
        }
        set
        {
            _movement = (movement + value).normalized /** ((movement.magnitude + value.magnitude) / 2)*/;
        }
    }

    public Vector3 position
    {
        get
        {
            return rb.position;
        }
        set
        {
            rb.position = value;
        }
    }
    public Vector3 velocity
    {
        get
        {
            return rb.velocity;
        }
        set
        {
            rb.velocity = value;
        }
    }

    public float mass
    {
        get
        {
            return rb.mass;
        }
    }
    public float drag
    {
        get
        {
            return rb.drag;
        }
        set
        {
            rb.drag = value;
        }
    }

    public float gravityScale;

    Vector3 m_UnitGoal;
    Vector3 m_GoalVel;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void AddForce(Vector3 force, ForceMode fm = ForceMode.Force)
    {
        rb.AddForce(force, fm);
    }

    //public void Move(float acceleration)
    //{
    //    AddForce(movement * acceleration, ForceMode.Impulse);
    //}

    private void FixedUpdate()
    {
        //AddForce(Physics.gravity * gravityScale);
        //if (_movement.magnitude > 0.1f)
            Move();
        transform.LookAt(transform.position + movement);
        _movement = Vector3.zero;
    }


    public void Move()
    {

        rb.MovePosition(transform.position + movement * maxSpeed * Time.deltaTime);

        //if (_movement.magnitude > 1f)
        //    _movement.Normalize();


        ////Vector3 unitVel = m_GoalVel.normalized;

        ////if (velDot < 0.5f)
        ////    Debug.Log($"{_movement} -> {m_UnitGoal} <-> {velDot}");


        //m_UnitGoal = _movement;
        //float velDot = Vector3.Dot(m_UnitGoal, velocity.normalized);


        //float accel = acceleration * (velDot < 0.9f ? acceleration : 1f);

        //Vector3 goalVel = _movement * maxSpeed; // * SpeedFactor

        //m_GoalVel = Vector3.MoveTowards(m_GoalVel, goalVel /*+ groundVel*/, accel /** PhysicsOneUpdatePerFrame.currentTimeStep*/);

        //Vector3 neededAccel = (m_GoalVel - velocity); // / PhysicsOneUpdatePerFrame.currentTimeStep

        //float maxAccel = maxAccelForce * (velDot < 0 ? 2f : 1f);

        ////neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
        //neededAccelFloat = velDot;

        //AddForce(Vector3.Scale(neededAccel * rb.mass, forceScale));


    }
}

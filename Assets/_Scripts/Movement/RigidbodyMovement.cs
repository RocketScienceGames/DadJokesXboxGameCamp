using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMovement : MonoBehaviour
{

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
            _movement = (movement + value).normalized;
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


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    public void Move(float acceleration)
    {
        AddForce(movement * acceleration);
    }

    private void FixedUpdate()
    {
        AddForce(Physics.gravity * gravityScale);
        transform.LookAt(transform.position + movement);
        _movement = Vector3.zero;
    }
}

using Corrupted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    public Vector3 direction;
    public BoolVariable useLocalSpace;


    // Start is called before the first frame update
    void Start()
    {
        if (useLocalSpace)
        {
            direction = transform.localToWorldMatrix.MultiplyVector(direction);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += direction * Time.fixedDeltaTime;
    }
}

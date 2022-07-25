using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    Transform target;

    // Update is called once per frame
    void Update()
    {
        if(target == null) target = Camera.main.transform;
        transform.LookAt(target);
    }
}

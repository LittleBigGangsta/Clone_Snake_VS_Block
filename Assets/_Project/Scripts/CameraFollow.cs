using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    void FixedUpdate()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.z = Target.position.z - 12f;
        transform.position = transformPosition;
    }
}

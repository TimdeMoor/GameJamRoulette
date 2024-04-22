using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FollowObject : MonoBehaviour
{
    [SerializeField] GameObject Target = null;
    [SerializeField] Vector3 Offset = new Vector3(0f, 0f, 0f);

    private void FixedUpdate()
    {
        transform.position = Target.transform.position + Offset;
    }
}

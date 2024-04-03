using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform follow;
    public float cameraHeight = 15f;
    public Vector2 cameraOffset;

    private Vector3 focus;
    void Start()
    {
        focus = follow.position;
    }

    private void LateUpdate()
    {
        Vector3 offset = new Vector3(cameraOffset.x, 0f, cameraOffset.y);
        transform.position = focus + Vector3.up * cameraHeight + offset;
        transform.LookAt(focus);
        
        FocusUpdate();
    }

    private Vector3 velocity;
    private void FocusUpdate()
    {
        float distance = Vector3.Distance(focus, follow.position);
        if (distance  > 2f)
        {
            float mult = 1 - Mathf.InverseLerp(4f, 5f, distance);
            Debug.Log(mult * 4f); 
            focus = Vector3.SmoothDamp(focus, follow.position, ref velocity, mult * 4f );
        }
    }
}

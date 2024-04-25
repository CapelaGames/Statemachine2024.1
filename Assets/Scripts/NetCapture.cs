using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCapture : MonoBehaviour
{
    public Transform netGO;
    public float speed = 1.0f;
    private float currentTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 upDir = Vector3.up;
            Vector3 forwardDir = gameObject.transform.forward;

            netGO.rotation = Quaternion.LookRotation(forwardDir, upDir);
            currentTime = 0.0f;
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, netGO.position);
            //get mouse direction from gameobject
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 target = Vector3.zero;
            if (plane.Raycast(ray, out float distance))
            {
                target = ray.GetPoint(distance);
            }

            Vector3 upDir = Vector3.up;
            Vector3 forwardDir = target - gameObject.transform.position;

            Quaternion upRotation = Quaternion.LookRotation(forwardDir, upDir);
            Quaternion forwardRotation = Quaternion.LookRotation(-upDir, forwardDir);

            currentTime += Time.deltaTime * speed;
            Debug.Log(currentTime);
            netGO.rotation = Quaternion.Lerp(upRotation, forwardRotation, currentTime);

        }
        if (Input.GetMouseButtonUp(0))
        {
        }
    }
}

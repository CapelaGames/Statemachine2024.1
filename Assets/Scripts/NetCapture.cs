using System;
using System.Collections;
using UnityEngine;

public class NetCapture : MonoBehaviour
{
    public Transform netGO;
    public float speed = 4.0f;
    public float currentTime;

    private void Update()
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPos = Vector3.zero;

            if (plane.Raycast(ray, out float distance))
            {
                targetPos = ray.GetPoint(distance);
            }

            Vector3 upDir = Vector3.up;
            Vector3 forwardDir = targetPos - netGO.position;

            Quaternion upRotation = Quaternion.LookRotation(forwardDir, upDir);
            Quaternion forwardRotation = Quaternion.LookRotation(-upDir, forwardDir);

            currentTime += Time.deltaTime * speed;

            netGO.rotation = Quaternion.Lerp(upRotation, forwardRotation, currentTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StateMachine sm = other.GetComponent<StateMachine>();
        if (sm != null)
        {
            sm.state = StateMachine.States.Idle;
            StartCoroutine(Capture(other.gameObject));
        }
    }

    IEnumerator Capture(GameObject slime)
    {
        float captureTime = 0;
        while(captureTime < 1.0f)
        {
            float wave = Mathf.Sin(Time.time * 20f) * 0.1f + 1f;
            float wave2 = Mathf.Cos(Time.time * 20f) * 0.1f + 1f;
            netGO.localScale = new Vector3(wave, wave2, wave);

            float reduce = 2.4f * Time.deltaTime;

            if (slime != null && slime.transform.localScale.magnitude > 0.1f)
            {
                slime.transform.localScale = new Vector3(slime.transform.localScale.x - reduce,
                    slime.transform.localScale.y - reduce,
                    slime.transform.localScale.z - reduce);
            }
            else
            {
                Destroy(slime);
            }
            captureTime += Time.deltaTime;
            yield return null;
        }
        netGO.localScale = Vector3.one;
    }
}

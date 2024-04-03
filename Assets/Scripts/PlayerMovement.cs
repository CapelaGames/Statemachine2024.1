using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 input;
    private bool isGrounded;

    public Camera camera;
    public float speed = 5f;
    public LayerMask groundMask;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    void Update()
    { 
        input = new Vector3(Input.GetAxis("Horizontal"), 
                                    0f,
                                    Input.GetAxis("Vertical"));
        if (input.magnitude > 1)
        {
            input.Normalize();
        }
        //Relational Movement
        float inputMag = input.magnitude;
        input = camera.transform.TransformDirection(input);
        input.y = 0f;
        input.Normalize();
        input *= inputMag;
        

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + input * (speed * Time.deltaTime));

        isGrounded = Physics.Raycast(transform.position, -transform.up, 1.05f, groundMask);
       
        //Debug.DrawRay(transform.position, -transform.up * 1.05f, Color.magenta);
    }
}

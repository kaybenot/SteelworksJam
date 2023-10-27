using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Head;
    public Transform HeadMountPoint;
    public float JumpHeight = 0.5f;
    public float MovementSpeed = 1f;
    public float LookSensitivity = 0.1f;

    public bool IsGrounded => Vector3.Dot(collisionNormal, Vector3.up) > 0.7f;

    private Rigidbody rb;
    private Vector3 collisionNormal = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogAssertion("Could not locate Rigidbody on Player!");

        if (Head == null)
            Debug.LogAssertion("There is no assigned Head to Player script!");
    }
    
    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void FixedUpdate()
    {
        EvaluateMovement();
    }

    public void Move(Vector2 direction)
    {
        velocity = new Vector3(direction.x * MovementSpeed, 0f, direction.y * MovementSpeed);
    }

    public void Turn(Vector2 delta)
    {
        delta *= LookSensitivity;
        Head.transform.eulerAngles += new Vector3(0f, delta.x, 0f);
        
        var headEulerAngles = Head.transform.eulerAngles;
        if (headEulerAngles.x > 180f)
            headEulerAngles.x -= 360f;
        
        if (headEulerAngles.x - delta.y < 90f && headEulerAngles.x - delta.y > -90f)
            Head.transform.eulerAngles += new Vector3(-delta.y, 0f, 0f);
        else if (headEulerAngles.x - delta.y > 0)
            Head.transform.eulerAngles = new Vector3(90f, Head.transform.eulerAngles.y, Head.transform.eulerAngles.z);
        else
            Head.transform.eulerAngles = new Vector3(-90f, Head.transform.eulerAngles.y, Head.transform.eulerAngles.z);
    }

    public void Interact()
    {
        Debug.Log("Tried interacting");
        // TODO: Interacting
    }

    public void Jump()
    {
        if (!IsGrounded)
            return;
        
        var jumpForce = Mathf.Sqrt(-2f * Physics.gravity.y * JumpHeight);
        var jumpDirection = Vector3.up;

        rb.velocity += jumpDirection * jumpForce;
    }

    private void EvaluateCollision(Collision collision)
    {
        collisionNormal = Vector3.zero;
        
        for (var i = 0; i < collision.contactCount; i++)
        {
            var contact = collision.contacts[i];
            collisionNormal += contact.normal;
            collisionNormal.Normalize();
        }
    }

    private void EvaluateMovement()
    {
        var forward = Head.transform.forward;
        forward.y = 0f;
        forward.Normalize();
        
        var right = Head.transform.right;
        right.y = 0f;
        right.Normalize();

        rb.velocity = forward * velocity.z + right * velocity.x + rb.velocity.y * Vector3.up;
    }
}

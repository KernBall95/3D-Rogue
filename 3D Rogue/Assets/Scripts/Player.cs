using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : Character {

    public float forwardSpeed = 8.0f;
    public float backwardSpeed = 4.0f;
    public float strafeSpeed = 6.0f;
    public int runMulitplier = 2;
    public KeyCode runKey = KeyCode.LeftShift;
    public float jumpForce = 30f;
    public Camera cam;
    public LookTowardMouse mouseLook = new LookTowardMouse();

    private Rigidbody rb;
    private CapsuleCollider capsule;
    private bool isGrounded, jump, isJumping;
    private float groundCheckDistance = 0.1f;
    private float stickToGroundDistance = 0.05f;
    private Vector3 groundContactNormal;
    private float currentTargetSpeed = 8f;
   
	void Start () {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        mouseLook.Init(transform, cam.transform);
        this.currentHealth = 10;
	}
	
	void Update () {
        CheckGrounded();
        RotatePlayer();
        Move();

        Debug.Log(this.currentHealth);

        if (isGrounded)
        {
            isJumping = false;
            rb.drag = 5f;
            /*if (Input.GetButtonDown("Jump"))
            {
                rb.drag = 5f;
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                isJumping = true;
                Debug.Log("Jumping");              
            }
            */
        }

        if(this.currentHealth <= 0)
        {
            Die(this.gameObject);
        }
    }
  
    void CheckGrounded()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out hit, ((capsule.height/2f) - capsule.radius) + groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            isGrounded = true;
            groundContactNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            groundContactNormal = Vector3.up;
        }      
    }

    void RotatePlayer()
    {
        float oldRotation = transform.eulerAngles.y;
        mouseLook.LookRotation(transform, cam.transform);

        if (isGrounded)
        {
            Quaternion velocityRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldRotation, Vector3.up);
            rb.velocity = velocityRotation * rb.velocity;
        }
    }

    void OnColliderEnter(Collision other)
    {
        if(other.collider.tag == "Enemy")
        {
            TakeDamage(this, 1);
        }
    }

    void Move()
    {
        if (Input.GetButton("Forward"))
        {
            Vector3 desiredMove = cam.transform.forward;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;
            desiredMove.x = desiredMove.x * forwardSpeed;
            desiredMove.y = desiredMove.y * forwardSpeed;
            desiredMove.z = desiredMove.z * forwardSpeed;
            rb.AddForce(desiredMove, ForceMode.Impulse);
        }
        if (Input.GetButton("Backward"))
        {
            Vector3 desiredMove = cam.transform.forward;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;
            desiredMove.x = desiredMove.x * -backwardSpeed;
            desiredMove.y = desiredMove.y * -backwardSpeed;
            desiredMove.z = desiredMove.z * -backwardSpeed;
            rb.AddForce(desiredMove, ForceMode.Impulse);

        }
        if (Input.GetButton("Left"))
        {
            Vector3 desiredMove = cam.transform.right;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;
            desiredMove.x = desiredMove.x * -strafeSpeed;
            desiredMove.y = desiredMove.y * -strafeSpeed;
            desiredMove.z = desiredMove.z * -strafeSpeed;
            rb.AddForce(desiredMove, ForceMode.Impulse);
        }
        if (Input.GetButton("Right"))
        {
            Vector3 desiredMove = cam.transform.right;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;
            desiredMove.x = desiredMove.x * strafeSpeed;
            desiredMove.y = desiredMove.y * strafeSpeed;
            desiredMove.z = desiredMove.z * strafeSpeed;
            rb.AddForce(desiredMove, ForceMode.Impulse);
        }
    }
}

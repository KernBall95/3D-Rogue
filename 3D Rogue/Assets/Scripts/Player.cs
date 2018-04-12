using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : Character {

    [Header("Movment Settings")]
    public float forwardSpeed = 8.0f;
    public float backwardSpeed = 4.0f;
    public float strafeSpeed = 6.0f;
    public float runMulitplier = 1.5f;
    public int jumpForce = 1300;

    [Header("Player Camera")]
    public Camera cam;
       
    private Rigidbody rb;
    private CapsuleCollider capsule;
    private bool isGrounded, jump, isJumping;
    private float groundCheckDistance = 0.1f;
    private Vector3 groundContactNormal;
    private KeyCode runKey = KeyCode.LeftShift;
    
    private Slider healthBar;
    private Slider maxHealthBar;
    private Slider movementSpeedBar;

    private LookTowardMouse mouseLook = new LookTowardMouse();

    private SwitchScene sceneSwitcher;

    void Awake () {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        mouseLook.Init(transform, cam.transform);
        currentHealth = maxHealth;

        healthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        maxHealthBar = GameObject.Find("Max Health Bar").GetComponent<Slider>();
        movementSpeedBar = GameObject.Find("Movement Speed Bar").GetComponent<Slider>();

        healthBar.value = currentHealth;
        maxHealthBar.value = maxHealth;
        movementSpeedBar.value = forwardSpeed;

        sceneSwitcher = GetComponent<SwitchScene>();
	}
	
	void Update () {
        CheckGrounded();
        RotatePlayer();
        Move();

        healthBar.value = currentHealth;
        maxHealthBar.value = maxHealth;
        movementSpeedBar.value = forwardSpeed;

        if (isGrounded)
        {
            isJumping = false;
            rb.drag = 5f;
        }
        else
        {
            isJumping = true;
            rb.drag = 0f;
        }

        if(this.currentHealth <= 0)
        {
            Die(this.gameObject);
            sceneSwitcher.SwitchToMenu();
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

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Enemy")
        {
            TakeDamage(this, 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spikes")
        {
            Die(this.gameObject);
            sceneSwitcher.SwitchToMenu();
        }
    }

    void Move()
    {
        
        if (Input.GetKeyDown(runKey))
        {
            forwardSpeed *= runMulitplier;
            backwardSpeed *= runMulitplier;
            strafeSpeed *= runMulitplier;
        }
        else if (Input.GetKeyUp(runKey))
        {
            forwardSpeed /= runMulitplier;
            backwardSpeed /= runMulitplier;
            strafeSpeed /= runMulitplier;
        }

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(0, jumpForce, 0);
        }
        if (Input.GetButton("Forward") && isJumping == false)
        {
            Vector3 desiredMove = cam.transform.forward;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;
            desiredMove.x = desiredMove.x * forwardSpeed;
            desiredMove.y = desiredMove.y * forwardSpeed;
            desiredMove.z = desiredMove.z * forwardSpeed;
            rb.AddForce(desiredMove, ForceMode.Impulse);
        }
        if (Input.GetButton("Backward") && isJumping == false)
        {
            Vector3 desiredMove = cam.transform.forward;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;
            desiredMove.x = desiredMove.x * -backwardSpeed;
            desiredMove.y = desiredMove.y * -backwardSpeed;
            desiredMove.z = desiredMove.z * -backwardSpeed;
            rb.AddForce(desiredMove, ForceMode.Impulse);

        }
        if (Input.GetButton("Left") && isJumping == false)
        {
            Vector3 desiredMove = cam.transform.right;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;
            desiredMove.x = desiredMove.x * -strafeSpeed;
            desiredMove.y = desiredMove.y * -strafeSpeed;
            desiredMove.z = desiredMove.z * -strafeSpeed;
            
            rb.AddForce(desiredMove, ForceMode.Impulse);
        }
        if (Input.GetButton("Right") && isJumping == false)
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

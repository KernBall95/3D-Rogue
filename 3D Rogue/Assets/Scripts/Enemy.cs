using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : Character {

    private Player player;
    private Vector3 target;
    private int mass;
    private bool stopUpdate = false;
    public float maxSpeed = 5;
    public float maxSteer = 2;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        
        Debug.Log("Target");
        this.currentHealth = this.maxHealth / 2;
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        Seek(transform.position, target);
        if (this.currentHealth <= 0 && stopUpdate == false)
        {
            Die(this.gameObject);
            stopUpdate = true;
            Debug.Log("Enemy killed");
            
        }
        
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Bullet")
        {
            TakeDamage(this, 1);
            Debug.Log("Hurt enemy");
        }
    }

    void Seek(Vector3 vehicle, Vector3 target)
    {
        Vector3 desiredVelocity = target - vehicle;
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity *= maxSpeed;
        Vector3 steeringForce = desiredVelocity - rb.velocity;
        if(steeringForce.magnitude > maxSteer)
        {
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxSteer);
        }
        Vector3 newVelocity = rb.velocity + steeringForce;
        if(newVelocity.magnitude > maxSpeed)
        newVelocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);
        rb.velocity += newVelocity;

        
    }
}

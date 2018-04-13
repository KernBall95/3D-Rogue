using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : Character {
    public float maxSpeed = 5;
    public float maxSteer = 2;
    
    private Player player;
    private Vector3 target;
    private int mass;
    private bool stopUpdate = false;
    private Rigidbody rb;
    private Room room;
    private float maxFlockDistance;
    private float minFlockDistance;
    private Vector3 alignment;
    private Vector3 cohesion;
    private Vector3 seperation;
    private DropPickup dropPickup;
    private Weapon weapon;
    
	void Awake () {       
        this.currentHealth = this.maxHealth;
        rb = GetComponent<Rigidbody>();
        room = GetComponentInParent<Room>();
        dropPickup = GetComponent<DropPickup>();
        weapon = GameObject.Find("Player").GetComponentInChildren<Weapon>();
    }
	
	void Update () {
        target = GameObject.Find("Player").transform.position;

        if(room.playerInRoom == true)
        {
            if(gameObject.name == "Zombie(Clone)" || gameObject.name == "Skull Enemy(Clone)")
            {
                Seek(transform.position, target);
            }
            else if(gameObject.name == "Skeleton(Clone)")
            {
                GetComponent<NavMeshAgent>().destination = target;
            }
        }
        else
        {
            Wander();
        }
        
        if (this.currentHealth <= 0 && stopUpdate == false)
        {
            dropPickup.DropItem();
            Die(this.gameObject);
            
            stopUpdate = true;
            room.enemyCount--;   
        }        
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Bullet")
        {          
            TakeDamage(this, weapon.damage);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spikes")
        {
            Die(this.gameObject);
        }
    }

    void Seek(Vector3 vehicle, Vector3 target)
    {
        Vector3 desiredVelocity = target - vehicle;
        desiredVelocity.y = 1f;
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity *= maxSpeed;
        Vector3 steeringForce = desiredVelocity - rb.velocity;
        if(steeringForce.magnitude > maxSteer)
        {
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxSteer);
        }
        Vector3 newVelocity = rb.velocity + steeringForce;
        if(newVelocity.magnitude > maxSpeed)
        {
            newVelocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);
        }      
        rb.velocity += newVelocity;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void Wander()
    {
        Vector3 displacement = new Vector3(rb.velocity.x + Random.Range(-25, 25), 0f, rb.velocity.z + Random.Range(-25, 25));
        Vector3 desiredVelocity = rb.velocity + displacement;
        desiredVelocity = desiredVelocity.normalized;
        desiredVelocity *= maxSpeed;
        Vector3 steeringForce = desiredVelocity - rb.velocity;
        if(steeringForce.magnitude > maxSteer)
        {
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxSteer);
        }
        Vector3 newVelocity = rb.velocity + steeringForce;
        if (newVelocity.magnitude > maxSpeed)
        {
            newVelocity = Vector3.ClampMagnitude(newVelocity, maxSpeed);
        }
        rb.velocity += newVelocity;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
}

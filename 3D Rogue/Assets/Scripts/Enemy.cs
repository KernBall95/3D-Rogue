using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Enemy : Character {
    public float maxSpeed = 5;
    public float maxSteer = 2;
    public GameObject[] pickups;

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
    private int pickupType;
    private int spawnPickupDecider;
    
	// Use this for initialization
	void Awake () {       
        this.currentHealth = this.maxHealth;
        rb = GetComponent<Rigidbody>();
        room = GetComponentInParent<Room>();       
	}
	
	// Update is called once per frame
	void Update () {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;

        if(room.playerInRoom == true)
        {
            if(gameObject.name == "Enemy(Clone)" || gameObject.name == "Enemy 3(Clone)")
            {
                Seek(transform.position, target);
            }
            else if(gameObject.name == "Enemy 2(Clone)")
            {
                GetComponent<NavMeshAgent>().destination = target;
                Debug.Log("agent");
            }
        }
        else
        {
            Wander();
        }
        
        if (this.currentHealth <= 0 && stopUpdate == false)
        {
            Die(this.gameObject);
            pickupType = Random.Range(0, pickups.Length);
            spawnPickupDecider = Random.Range(0, 3);
            if(spawnPickupDecider == 1)
            {
                Instantiate(pickups[pickupType], transform.position, Quaternion.identity);
            }
            
            stopUpdate = true;
            room.enemyCount--;   
        }        
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Bullet")
        {
            TakeDamage(this, 1);
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
        Vector3 displacement = new Vector3(rb.velocity.x + Random.Range(-25, 25), 1f, rb.velocity.z + Random.Range(-25, 25));
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

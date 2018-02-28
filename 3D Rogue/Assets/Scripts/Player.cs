using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour {

    public float forwardSpeed = 8.0f;
    public float backwardSpeed = 4.0f;
    public float strafeSpeed = 6.0f;
    public int runMulitplier = 2;
    public KeyCode runKey = KeyCode.LeftShift;
    public float jumpForce = 30f;
    public Camera cam;

    [Serializable]
    public class PlayerStats
    {
        public int maxHealth;
        public int currentHealth;
    }

    public PlayerStats playerStats = new PlayerStats();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

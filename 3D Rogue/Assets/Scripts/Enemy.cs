using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private Player player;
    private Vector3 target;
    private int mass;

	// Use this for initialization
	void Start () {
        target = GetComponent<Player>().gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnColliderEnter(Collision other)
    {
        if(other.collider.tag == "Bullet")
        {
            TakeDamage(this, 1);
        }
    }
}

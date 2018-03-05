using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : ObjectClass {

    private bool hasItem;

	void Start () {
        this.currentHealth = this.maxHealth;
    }
	
	void Update () {
        if (this.currentHealth <= 0)
        {
            DestroyObject(this.gameObject);
        }
    }

    void SpawnItem()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Bullet")
        {
            TakeDamage(this, 1);
        }
    }
}

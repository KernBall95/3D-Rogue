using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : ObjectClass {

	void Start () {
        this.currentHealth = this.maxHealth;
        weapon = GameObject.Find("Player").GetComponentInChildren<Weapon>();
    }
	
	void Update () {
        if (this.currentHealth <= 0)
        {
            DestroyObject(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Bullet")
        {
            TakeDamage(this, weapon.damage);
        }
    }
}

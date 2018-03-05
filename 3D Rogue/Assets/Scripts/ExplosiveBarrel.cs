using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : ObjectClass {

    
    public ParticleSystem explosion;

    private int damage;
    private Collider explosionRadius;
    private bool stopUpdate = false;
    
	void Start () {
        this.currentHealth = this.maxHealth;
	}

    void Update()
    {
        if(this.currentHealth <= 0 && stopUpdate == false)
        {
            DestroyObject(this.gameObject);
            Instantiate(explosion, transform.position, Quaternion.Euler(-90, 0, 0));
            stopUpdate = true;
        }
    }

    void ApplyDamage()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Bullet")
        {
            TakeDamage(this, 1);
        }
    }
}

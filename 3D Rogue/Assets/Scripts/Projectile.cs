using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float timeSinceSpawn = 0f;
    private int timeUntilDeath = 2;
    private float particleDuration;
    public GameObject explosion;

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if(timeSinceSpawn >= timeUntilDeath)
            Destroy(this.gameObject);
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float timeSinceSpawn = 0f;
    private int timeUntilDeath = 2;


    void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if(timeSinceSpawn >= timeUntilDeath)
        {
            Destroy(this.gameObject);
        }
    }

   
}

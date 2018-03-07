using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    private int damage = 1;
    private float timeSinceSpawn;

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if(timeSinceSpawn >= 1f)
        {
            Destroy(this.gameObject);
        }
    }

    void ApplyDamage(Character player, int explosionDamage)
    {
        player.currentHealth -= explosionDamage;
        Debug.Log(player.currentHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ApplyDamage(other.GetComponent<Character>(), damage);
            Debug.Log("Damage player");
        }
    }
}

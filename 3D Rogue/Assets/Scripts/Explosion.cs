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

    void ApplyDamageToObject(ObjectClass obj, int explosionDamage)
    {
        obj.currentHealth -= explosionDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ApplyDamage(other.GetComponent<Character>(), damage);
            Debug.Log("Damage player");
        }
        else if(other.tag == "Enemy")
        {
            ApplyDamage(other.GetComponent<Character>(), damage * 2);
        }
        else if(other.tag == "Obstacle")
        {
            ApplyDamageToObject(other.GetComponent<ObjectClass>(), damage * 5);
        }
    }
}

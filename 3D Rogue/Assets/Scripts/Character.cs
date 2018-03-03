using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public bool isAlive;

    public void Die(GameObject characterToDie)
    {
        Destroy(characterToDie);
    }

    public void TakeDamage(Character characterHit, int damage)
    {
        characterHit.currentHealth--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ObjectClass : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;

    public void TakeDamage(ObjectClass obj, int damage)
    {
        obj.currentHealth -= damage;
    }

    public void DestroyObject(GameObject objectToBeDestroyed)
    {
        Destroy(objectToBeDestroyed);
    }
}

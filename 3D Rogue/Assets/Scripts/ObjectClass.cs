using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ObjectClass : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    public Weapon weapon;

    private DropPickup dropPickup;

    public void TakeDamage(ObjectClass obj, int damage)
    {
        obj.currentHealth -= damage;
    }

    public void DestroyObject(GameObject objectToBeDestroyed)
    {
        dropPickup = GetComponent<DropPickup>();
        dropPickup.DropItem();
        Destroy(objectToBeDestroyed);
    }
}

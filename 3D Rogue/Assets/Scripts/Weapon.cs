using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform projectileOrigin;
    public Rigidbody projectile;
    public float bulletSpeed;
    private Rigidbody rb;

	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bulletClone = Instantiate(projectile, projectileOrigin.position, Quaternion.identity);
            bulletClone.velocity = transform.forward * bulletSpeed;
        }
    }
}

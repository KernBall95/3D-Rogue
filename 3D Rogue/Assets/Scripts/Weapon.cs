using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform projectileOrigin;
    public Rigidbody projectile;
    public float bulletSpeed;
    public float fireRate = .1f;
    private Rigidbody rb;
    private IEnumerator coroutine;
    private bool allowFire = true;

	void Update () {
        if (Input.GetButton("Fire1") && allowFire == true)
        {
            coroutine = Shoot();
            StartCoroutine(coroutine);
        }
    }

    IEnumerator Shoot()
    {
        allowFire = false;
        Rigidbody bulletClone = Instantiate(projectile, projectileOrigin.position, Quaternion.identity);
        bulletClone.velocity = transform.forward * bulletSpeed;
        yield return new WaitForSeconds(fireRate);
        allowFire = true;
    }
}

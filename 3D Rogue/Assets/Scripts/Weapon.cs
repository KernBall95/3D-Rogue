using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public Transform projectileOrigin;
    public Rigidbody projectile;

    public Camera cam;

    [Header("Weapon Stats")]
    public float fireRate;
    public int damage;
    public float bulletSpeed;

    private Rigidbody rb;
    private IEnumerator coroutine;
    private bool allowFire = true;

    private Slider fireRateBar;
    private Slider weaponDamageBar;

    private float x = Screen.width / 2;
    private float y = Screen.height / 2;

    void Start()
    {
        fireRateBar = GameObject.Find("Fire Rate Bar").GetComponent<Slider>();
        weaponDamageBar = GameObject.Find("Weapon Damage Bar").GetComponent<Slider>();
    }

	void Update () {
        fireRateBar.value = fireRate * -1;
        weaponDamageBar.value = damage;

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

        Ray ray = cam.ScreenPointToRay(new Vector3(x, y, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 bulletVector = (hit.point - projectileOrigin.position).normalized;
            bulletClone.velocity = bulletVector * bulletSpeed;
            yield return new WaitForSeconds(fireRate);
            allowFire = true;
        }
    }
}

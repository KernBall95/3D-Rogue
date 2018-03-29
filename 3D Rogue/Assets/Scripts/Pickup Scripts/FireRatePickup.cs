using UnityEngine;

public class FireRatePickup : Item {

    private float fireRateBonus = -0.05f;
	
	void Start () {
        name = "Fire Rate Pickup";
	}

    void Update()
    {
        RotateItem();
    }
	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            IncreaseFireRate(other.gameObject.GetComponentInChildren<Weapon>(), fireRateBonus);
        }
    }
}

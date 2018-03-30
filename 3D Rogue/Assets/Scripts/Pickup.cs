using UnityEngine;

public class Pickup : Item {
    public PickupType type;
    public string pickupName;

    public int speedBonus = 1;
    public int healthBonus = 1;
    public float fireRateBonus = -0.05f;
    public int damageBonus = 1;

    void Update()
    {
        RotateItem();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            Weapon gun = other.gameObject.GetComponentInChildren<Weapon>();
            switch (type)
            {
                case PickupType.MovementSpeed:
                    AddSpeed(player, speedBonus);
                    break;
                case PickupType.SmallHealth:
                    AddHealth(player, healthBonus);
                    break;
                case PickupType.FireRate:
                    IncreaseFireRate(gun, fireRateBonus);
                    break;
                case PickupType.WeaponDamage:
                    IncreaseWeaponDamage(gun, damageBonus);
                    break;
                default:
                    Debug.LogError("Invalid pickup type!");
                    break;
            }
        }
    }

}



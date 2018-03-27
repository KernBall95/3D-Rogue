using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : MonoBehaviour {
    public string name;

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
    public void UpdatePlayerStats(Player player, string pickupType, PlayerUpgrade upgrade)
    {
        switch (pickupType)
        {
            case "Health":
                player.currentHealth += upgrade.healthBonus;
                break;
            case "Speed":
                player.forwardSpeed += upgrade.playerSpeedBonus;
                player.backwardSpeed += upgrade.playerSpeedBonus;
                player.strafeSpeed += upgrade.playerSpeedBonus;
                break;
            case "Max Health":
                player.maxHealth += upgrade.maxHealthBonus;
                break;
            default:
                Debug.LogError("Invalid pickup type!");
                break;
        }
    }
    public void UpdateWeaponStats(Weapon gun, string pickupType, PlayerUpgrade upgrade)
    {
        switch (pickupType)
        {
            case "Fire Rate":
                gun.fireRate += upgrade.fireRateBonus;
                break;
            default:
                Debug.LogError("Invalid pickup type!");
                break;
        }
    }
}

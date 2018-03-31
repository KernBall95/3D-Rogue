using UnityEngine;

public class Item : MonoBehaviour {
    public string itemName;
    public float rotateSpeed = 100f;

    public void RotateItem()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
    
    public void AddHealth(Player player, int healthBonus)
    {
        if(player.currentHealth < player.maxHealth)
        {
            player.currentHealth += healthBonus;
            DestroyItem();
        }
        else
        {
            Debug.Log("Already at max health!");
        }
    }

    public void AddSpeed(Player player, float speedBonus)
    {
        if (player.forwardSpeed < 15)
        {
            player.forwardSpeed += speedBonus;
            player.backwardSpeed += speedBonus;
            player.strafeSpeed += speedBonus;
            DestroyItem();
        }
        else
        {
            Debug.Log("Already at max speed!");
        }
    }
    public void IncreaseFireRate(Weapon gun, float fireRateBonus)
    {
        if(gun.fireRate > 0.1f)
        {
            gun.fireRate += fireRateBonus;
            DestroyItem();
        }
        else
        {
            Debug.Log("Already at fire rate!");
        }
    }

    public void IncreaseWeaponDamage(Weapon gun, int damageBonus)
    {
        if (gun.damage < 5)
        {
            gun.damage += damageBonus;
            DestroyItem();
        }
        else
        {
            Debug.Log("Already at weapon damage!");
        }
    }
}

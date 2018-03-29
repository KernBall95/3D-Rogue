using UnityEngine;

abstract public class Item : MonoBehaviour {
    [HideInInspector]public string name;
    private float rotateSpeed = 100f;

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
        player.currentHealth += healthBonus;
        DestroyItem();
    }

    public void AddSpeed(Player player, float speedBonus)
    {
        player.forwardSpeed += speedBonus;
        player.backwardSpeed += speedBonus;
        player.strafeSpeed += speedBonus;
        DestroyItem();
    }
    public void IncreaseFireRate(Weapon gun, float fireRateBonus)
    {
        gun.fireRate += fireRateBonus;
        DestroyItem();
    }
}

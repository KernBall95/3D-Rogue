using UnityEngine;

public class SmallHealthPickup : Item {

    private int healthBonus = 1;

	void Start () {
        name = "Health";
	}

    void Update()
    {
        RotateItem();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AddHealth(other.gameObject.GetComponent<Player>(), healthBonus);
        }
    }
}

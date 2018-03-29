using UnityEngine;

public class SpeedPickup : Item {

    private float speedBonus = 1;

	void Start () {
        name = "Speed";
	}

    void Update()
    {
        RotateItem();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AddSpeed(other.gameObject.GetComponent<Player>(), speedBonus);
        }
    }
}

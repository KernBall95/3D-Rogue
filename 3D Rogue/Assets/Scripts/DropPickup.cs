using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickup : MonoBehaviour {

    public GameObject[] pickups;

    private int pickupType;
    private int spawnPickupDecider;
    private float spawnHeight = 1.5f;

	public void DropItem () {
            pickupType = Random.Range(0, pickups.Length);
            spawnPickupDecider = Random.Range(0, 4);
            if (spawnPickupDecider == 1)
            {
                Instantiate(pickups[pickupType], new Vector3(transform.position.x, spawnHeight, transform.position.z), Quaternion.identity);
            }
    }
}

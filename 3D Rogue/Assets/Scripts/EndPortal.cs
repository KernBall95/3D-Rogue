using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour {

    private RoomGenerator roomGenerator;
    private GameObject roomGenObject;

    void Start()
    {
        roomGenObject = GameObject.Find("Room Generator");
        roomGenerator = roomGenObject.GetComponent<RoomGenerator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            roomGenerator.DestroyAndRegen();
        }
    }
}

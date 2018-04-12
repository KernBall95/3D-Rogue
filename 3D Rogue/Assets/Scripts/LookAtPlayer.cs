using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public GameObject head;

    private GameObject player;

	void Start () {
        player = GameObject.Find("Player");
	}
	
	void Update () {
        head.transform.LookAt(player.transform);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public GameObject head;

    private GameObject player;

	void Start () {
        player = GameObject.Find("Player(Clone)");
	}
	
	void Update () {
        head.transform.LookAt(player.transform);
	}
}

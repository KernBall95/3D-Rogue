using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Player player;
    private Vector3 target;
    private int mass;

	// Use this for initialization
	void Start () {
        target = GetComponent<Player>().gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

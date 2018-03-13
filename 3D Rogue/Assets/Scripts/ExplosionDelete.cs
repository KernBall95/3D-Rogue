using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDelete : MonoBehaviour {

    public ParticleSystem ps;
    private float explosionDuration;
	void Start () {
        explosionDuration = ps.main.duration + ps.main.startLifetimeMultiplier;
        Destroy(this.gameObject, explosionDuration);
	}	
}

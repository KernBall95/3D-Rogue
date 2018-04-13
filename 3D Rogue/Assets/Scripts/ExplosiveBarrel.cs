using UnityEngine;

public class ExplosiveBarrel : ObjectClass {

    
    public ParticleSystem explosion;
    public GameObject explosionAudioSource;

    private bool stopUpdate = false;
      
	void Start () {
        this.currentHealth = this.maxHealth;
        weapon = GameObject.Find("Player").GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        if(this.currentHealth <= 0 && stopUpdate == false)
        {
            DestroyObject(this.gameObject);

            GameObject audioClone = Instantiate(explosionAudioSource, transform.position, Quaternion.identity);
            Destroy(audioClone, 3f);
            Instantiate(explosion, transform.position, Quaternion.Euler(-90, 0, 0));

            stopUpdate = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Bullet")
        {
            TakeDamage(this, weapon.damage);
        }
    }
}

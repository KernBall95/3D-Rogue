using UnityEngine;

public class CorridorChecker : MonoBehaviour {

    public bool hasCorridor = false;    //Is true if a corridor is detected
    public Vector3 spawnPoint;

    private Collider corridorChecker;

    void Awake()
    {
        spawnPoint = transform.position;    //Sets the spawn position for the door
    }
    
    //Check for corridor
	void OnTriggerStay(Collider other)
    {
        if(other.tag == "Corridor")
        {
            hasCorridor = true;
        }
        else
        {
            hasCorridor = false;
        }
        corridorChecker = GetComponent<Collider>();
        Destroy(corridorChecker);
    }
}

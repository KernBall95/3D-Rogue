using UnityEngine;

public class MiniMap : MonoBehaviour {

    private GameObject player;
    private bool playerIsFound;

    void Start()
    {
        player = GameObject.Find("Player(Clone)");
        playerIsFound = false;
    }
	
	void Update () {
        if(playerIsFound == false)
        {
            player = GameObject.Find("Player(Clone)");
            playerIsFound = true;
        }
        
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
}

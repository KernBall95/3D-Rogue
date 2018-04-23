using UnityEngine;

public class MiniMap : MonoBehaviour {

    private GameObject player;
	
	void Update () {

       if(player == null)
        player = GameObject.Find("Player");
       
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
}

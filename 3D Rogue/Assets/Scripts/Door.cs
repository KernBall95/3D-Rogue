using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasEnemies;     //Is true if there are enemies in the room
    public bool playerInRoom;   //Is true if player is in the room
    public bool doorIsClosed;   //Is true if the door is closed

    private Room room;

    void Start()
    {
        doorIsClosed = true;
        room = GetComponentInParent<Room>();
        room.hasEnemies = false;
        
    }

    void Update()
    {
        if (room.hasEnemies == true && doorIsClosed == false && room.playerInRoom == true)
        {
            CloseDoor();
        }
        else if(room.hasEnemies == false && doorIsClosed == true)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y - 5.2f, startPos.z);

        transform.position = targetPos;
        doorIsClosed = false;
    }

    public void CloseDoor()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y + 5.2f, startPos.z);

        transform.position = targetPos;
        doorIsClosed = true;
    }
}

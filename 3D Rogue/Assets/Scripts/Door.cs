using UnityEngine;

public class Door : MonoBehaviour
{
    public bool playerInRoom;   //Is true if player is in the room
    
    private Room room;
    private bool doorIsClosed;   //Is true if the door is closed

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

    void OpenDoor()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y - 5.2f, startPos.z);

        transform.position = targetPos;
        doorIsClosed = false;
    }

    void CloseDoor()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y + 5.2f, startPos.z);

        transform.position = targetPos;
        doorIsClosed = true;
    }
}

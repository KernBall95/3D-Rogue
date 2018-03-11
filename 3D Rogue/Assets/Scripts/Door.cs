using UnityEngine;

public class Door : MonoBehaviour
{   
    private bool doorIsClosed;   //Is true if the door is closed

    public void OpenDoor()
    {
        if (doorIsClosed == true)
        {
            Vector3 startPos = transform.position;
            Vector3 targetPos = new Vector3(startPos.x, startPos.y - 5.2f, startPos.z);

            transform.position = targetPos;
            doorIsClosed = false;
        }
        else
        {
            Debug.Log("Door already open.");
        }
    }

    public void CloseDoor()
    {
        if (doorIsClosed == false)
        {
            Vector3 startPos = transform.position;
            Vector3 targetPos = new Vector3(startPos.x, startPos.y + 5.2f, startPos.z);

            transform.position = targetPos;
            doorIsClosed = true;
        }
        else
        {
            Debug.Log("Door already closed");
        }
    }
}

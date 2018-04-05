using UnityEngine;

public class Door : MonoBehaviour
{   

    public void OpenDoor()
    {
            Vector3 startPos = transform.position;
            Vector3 targetPos = new Vector3(startPos.x, startPos.y - 5.2f, startPos.z);

            transform.position = targetPos;
    }

    public void CloseDoor()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y + 5.2f, startPos.z);

        transform.position = targetPos;
    }

}

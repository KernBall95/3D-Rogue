using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorPrefab;
    public GameObject doorwayBlocker;
    public CorridorChecker[] doorSpawns;
    public bool playerInRoom;
    public GameObject[] objectPrefabs;
    public Transform[] objectSpawns;
    public Transform[] enemySpawns;
    public GameObject[] enemyPrefabs;
    public int enemyCount = 0;

    private GameObject roomGenObject;
    private int objectSpawnDecider;
    private int objectTypeDecider;
    private RoomGenerator roomGenerator;
    public List<Door> doors;
    public bool doorsAreClosed = false;
    private int enemyType;
    private bool doorsAreSpawned = false;
    
    void Start()
    {
        roomGenObject = GameObject.Find("Room Generator");
        roomGenerator = roomGenObject.GetComponent<RoomGenerator>();

        IEnumerator genCoroutine = WaitForGen();
        StartCoroutine(genCoroutine);

        SpawnObjects();
        SpawnEnemies();
    }

    void Update()
    {
        if(doorsAreSpawned == true)
            DoorControl();     
    }

    //Check if player enters room
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRoom = true;
        }      
    }
    
    //Check if player exits room
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRoom = false;
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < objectSpawns.Length; i++)
        {
            objectSpawnDecider = Random.Range(0, 2);
            objectTypeDecider = Random.Range(0, objectPrefabs.Length);
            if (objectSpawnDecider == 1)
            {
                Instantiate(objectPrefabs[objectTypeDecider], objectSpawns[i].position, Quaternion.identity, transform);               
            }
        }
    }

    void SpawnDoors()
    { 
        for (int i = 0; i < doorSpawns.Length; i++)
        {
            if (doorSpawns[i].hasCorridor == true) //Instantiate door if there is a corridor
            {
                doorSpawns[i].spawnPoint = new Vector3(doorSpawns[i].spawnPoint.x, doorSpawns[i].spawnPoint.y - 5.2f, doorSpawns[i].spawnPoint.z);
                Door door = Instantiate(doorPrefab, doorSpawns[i].spawnPoint, doorSpawns[i].gameObject.transform.rotation, transform).GetComponent<Door>();
                doors.Add(door);

            }
            else //Instantiate a doorway blocker if there is no corridor
            {
                Instantiate(doorwayBlocker, doorSpawns[i].spawnPoint, doorSpawns[i].gameObject.transform.rotation, transform);
            }
        }
        doorsAreSpawned = true;
    }

    void SpawnEnemies()
    {
        for(int i = 0; i < enemySpawns.Length; i++)
        {
            objectSpawnDecider = Random.Range(0, 2);
            if(objectSpawnDecider == 1 && this.name != "Starting Room(Clone)")
            {
                enemyType = Random.Range(0, 3);
                if (enemyType == 0 || enemyType == 1)
                {
                    Instantiate(enemyPrefabs[enemyType], enemySpawns[i].position, Quaternion.identity, transform);
                    enemyCount++;
                }
                else if(enemyType == 2 && roomGenerator.dungeonLevel > 0)
                {
                    for(int j = 0; j < 4; j++)
                    {
                        Instantiate(enemyPrefabs[enemyType], enemySpawns[j].position, Quaternion.identity, transform);
                        enemyCount++;
                    }
                }            
            }
        }
    }

    void DoorControl()
    {
        if (enemyCount > 0 && doorsAreClosed == false && playerInRoom == true)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].CloseDoor();               
            }
            doorsAreClosed = true;
        }
        else if (enemyCount == 0 && doorsAreClosed == true)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].OpenDoor();
            }
            doorsAreClosed = false;
        }
    }


    private IEnumerator WaitForGen()
    {
        while(roomGenerator.generationFinished == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SpawnDoors();
    }

    private IEnumerator WaitForEnemySpawnCondition()
    {
        while(roomGenerator.spawnEnemies == false)
        {
            yield return null;
        }
        SpawnEnemies();
    }

}

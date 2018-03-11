using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorPrefab;
    public GameObject doorwayBlocker;
    public CorridorChecker[] doorSpawns;
    public bool playerInRoom;
    public bool hasEnemies;
    public GameObject[] objectPrefabs;
    public Transform[] objectSpawns;
    public Transform[] enemySpawns;
    public GameObject enemyPrefab;
    [HideInInspector] public int enemyCount = 0;

    private GameObject roomGenObject;
    private int objectSpawnDecider;
    private int objectTypeDecider;
    private RoomGenerator roomGenerator;
    
    void Start()
    {
        roomGenObject = GameObject.Find("Room Generator");
        roomGenerator = roomGenObject.GetComponent<RoomGenerator>();
        IEnumerator coroutine = WaitForGen();
        StartCoroutine(coroutine);
        SpawnObjects();
        SpawnEnemies();
        playerInRoom = false;
    }

    void Update()
    {
        DetectEnemies();
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
                Instantiate(doorPrefab, doorSpawns[i].spawnPoint, doorSpawns[i].gameObject.transform.rotation, transform);
            }
            else //Instantiate a doorway blocker if there is no corridor
            {
                Instantiate(doorwayBlocker, doorSpawns[i].spawnPoint, doorSpawns[i].gameObject.transform.rotation, transform);
            }
        }
    }

    void SpawnEnemies()
    {
        for(int i = 0; i < enemySpawns.Length; i++)
        {
            objectSpawnDecider = Random.Range(0, 2);
            if(objectSpawnDecider == 1)
            {
                Instantiate(enemyPrefab, enemySpawns[i].position, Quaternion.identity, transform);
                enemyCount++;
            }
        }
    }

    void DetectEnemies()
    {
        if (enemyCount > 0)
        {
            hasEnemies = true;
        }
        else hasEnemies = false;
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

}

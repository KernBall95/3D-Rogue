using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    //Public variables
    public GameObject[] rooms;                                                      //Array of different rooms that can be spawned
    public GameObject corridor;                                                     //Prefab of the corridor. May become array of multiple corridor types   
    public GameObject endPortal;                                                    //Portal which when touched by player will generate next level of dungeon
    public GameObject player;                                                       //The player

    [Range(4, 99)]
    public int roomCount;                                                           //Amount of attempts to spawn rooms
   
    [HideInInspector] public bool generationFinished;                               //Is true when the generation of a dungeon level has finished   
    [HideInInspector] public List<GameObject> spawnedObjects;                       //A list of the objects that have been spawned

    //Private variables
    private GameObject currentRoom;                                                 //The most recent spawned room
    private float genX, genY, genZ;                                                 //Coordinates
    private Vector3 roomSpawnPos;                                                   //Room spawn coordinates
    private int roomNumber;                                                         //Which room type will be spawned
    private int roomDirNumber;                                                      //Random number to decide which direction to generate
    private string nextSpawnDirection;                                              //Which direction the generator will go
    private bool northRayHit, EastRayHit, southRayHit, westRayHit;                  //Ray hits
    private Transform northRay, southRay, eastRay, westRay;                         //Checks if a room is already where the generator wants to spawn a room
    private Transform northDoorSpawn, eastDoorSpawn, southDoorSpawn, westDoorSpawn; //Door spawn points
    private GameObject playerClone;
    private int dungeonLevel = 1;                                                    //The level of the dungeon

    void Start()
    {
        generationFinished = false;

        //Main generation loop
        IEnumerator coroutine = GenerationLoop();
        StartCoroutine(coroutine);      
    }

    IEnumerator GenerationLoop()
    {       
        //Set starting point
        genX = 0;
        genY = 0;
        genZ = 0;

        //Instantiate room at starting point
        roomSpawnPos = new Vector3(genX, genY, genZ);
        transform.position = roomSpawnPos;
        roomNumber = Random.Range(0, 5);
        currentRoom = Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
        spawnedObjects.Add(currentRoom);
        playerClone = Instantiate(player, new Vector3(0, 1.8f, -20), Quaternion.identity);
        FindRaysAndDoorSpawns();

        for (int i = 0; i <= roomCount; i++)
        {
            yield return new WaitForSeconds(0.000001f);
            GetDirection();
            roomNumber = Random.Range(0, 4); //Decide which room type will be spawned
            
            if (nextSpawnDirection == "North")
            {
                Ray forward = new Ray(northRay.transform.position, northRay.transform.forward);
                Ray downward = new Ray(northRay.transform.position, -northRay.transform.up);
                Vector3 corridorSpawnPos = new Vector3(northDoorSpawn.transform.position.x, northDoorSpawn.transform.position.y - 2.5f, northDoorSpawn.transform.position.z);
                Quaternion corridorRotation = Quaternion.identity;

                GenerateRoom(forward, downward, corridorSpawnPos, corridorRotation);               
            }
            else if (nextSpawnDirection == "East")
            {
                Ray forward = new Ray(eastRay.transform.position, eastRay.transform.forward);
                Ray downward = new Ray(eastRay.transform.position, -eastRay.transform.up);
                Vector3 corridorSpawnPos = new Vector3(eastDoorSpawn.transform.position.x, eastDoorSpawn.transform.position.y - 2.5f, eastDoorSpawn.transform.position.z);
                Quaternion corridorRotation = Quaternion.Euler(0, 90, 0);

                GenerateRoom(forward, downward, corridorSpawnPos, corridorRotation);
            }
            else if (nextSpawnDirection == "South")
            {
                Ray forward = new Ray(southRay.transform.position, southRay.transform.forward);
                Ray downward = new Ray(southRay.transform.position, -southRay.transform.up);
                Vector3 corridorSpawnPos = new Vector3(southDoorSpawn.transform.position.x, southDoorSpawn.transform.position.y - 2.5f, southDoorSpawn.transform.position.z);
                Quaternion corridorRotation = Quaternion.Euler(0, 180, 0);

                GenerateRoom(forward, downward, corridorSpawnPos, corridorRotation);
            }
            else if (nextSpawnDirection == "West")
            {
                Ray forward = new Ray(westRay.transform.position, westRay.transform.forward);
                Ray downward = new Ray(westRay.transform.position, -westRay.transform.up);
                Vector3 corridorSpawnPos = new Vector3(westDoorSpawn.transform.position.x, westDoorSpawn.transform.position.y - 2.5f, westDoorSpawn.transform.position.z);
                Quaternion corridorRotation = Quaternion.Euler(0, -90, 0);

                GenerateRoom(forward, downward, corridorSpawnPos, corridorRotation);
            }
        }
        Instantiate(endPortal, transform.position, Quaternion.identity, currentRoom.transform);
        generationFinished = true; //Generation of the current dungeon level is finished
    }

    //Generates a random number to decide direction and then translates it to a string
    void GetDirection()
    {
        roomDirNumber = Random.Range(1, 5);

        switch (roomDirNumber)
        {
            case 1:
                nextSpawnDirection = "North";
                break;
            case 2:
                nextSpawnDirection = "East";
                break;
            case 3:
                nextSpawnDirection = "South";
                break;
            case 4:
                nextSpawnDirection = "West";
                break;
            default:
                Debug.LogError("roomDirNumber is out of range");
                break;
        }
    }

    //Generates the next room in the given direction
    void GenerateRoom(Ray forwardRay, Ray downwardRay, Vector3 corridorSpawn, Quaternion currentCorridorRotation)
    {
        RaycastHit hit;
        RaycastHit otherHit;
        if (Physics.Raycast(forwardRay, out hit, 60f) && !Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, currentCorridorRotation);

            spawnedObjects.Add(newCorridor);
        }
        else if(Physics.Raycast(forwardRay, out hit, 60f) && Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("Already a room and corridor.");
        }
        else
        {
            switch (nextSpawnDirection)
            {
                case "North":
                    genZ += 100;
                    break;
                case "East":
                    genX += 100;
                    break;
                case "South":
                    genZ -= 100;
                    break;
                case "West":
                    genX -= 100;
                    break;
            }
            
            Vector3 roomSpawnPos = new Vector3(genX, genY, genZ);
            transform.position = roomSpawnPos;
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, currentCorridorRotation);
            spawnedObjects.Add(newCorridor);

            currentRoom = Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
            spawnedObjects.Add(currentRoom);

            FindRaysAndDoorSpawns();
        }
    }

    //Find the Rays and Door Spawns to prepare for next iteration of generation
    void FindRaysAndDoorSpawns()
    {
        northRay = currentRoom.transform.Find("Rays/North Ray");
        southRay = currentRoom.transform.Find("Rays/South Ray");
        eastRay = currentRoom.transform.Find("Rays/East Ray");
        westRay = currentRoom.transform.Find("Rays/West Ray");

        northDoorSpawn = currentRoom.transform.Find("Door Spawns/North Door Spawn");
        eastDoorSpawn = currentRoom.transform.Find("Door Spawns/East Door Spawn");
        southDoorSpawn = currentRoom.transform.Find("Door Spawns/South Door Spawn");
        westDoorSpawn = currentRoom.transform.Find("Door Spawns/West Door Spawn");
    }

    //Destroys current dungeon and starts the generation of the next level
    public void DestroyAndRegen()
    {        
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            Destroy(spawnedObjects[i]);          
        }
        Destroy(playerClone);
        spawnedObjects.Clear();
        dungeonLevel++;
        if(roomCount < 99)
        {
            roomCount += 2;
        }
        else
        {
            roomCount = 99;
        }
        generationFinished = false;
        IEnumerator coroutine = GenerationLoop();
        StartCoroutine(coroutine);
    }
}

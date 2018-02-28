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
        playerClone = Instantiate(player, new Vector3(0, 2, -20), Quaternion.identity);
        FindRaysAndDoorSpawns();

        for (int i = 0; i <= roomCount; i++)
        {
            yield return new WaitForSeconds(.000001f);
            GetDirection();
            roomNumber = Random.Range(0, 5); //Decide which room type will be spawned
            
            if (nextSpawnDirection == "North")
            {
                GenerateNorth();               
            }
            else if (nextSpawnDirection == "East")
            {
                GenerateEast();               
            }
            else if (nextSpawnDirection == "South")
            {
                GenerateSouth();               
            }
            else if (nextSpawnDirection == "West")
            {
                GenerateWest();              
            }
        }
        Instantiate(endPortal, transform.position, Quaternion.identity, currentRoom.transform);
        generationFinished = true; //Generation of the current dungeon level is finished
    }

    //Generates a random number to decide direction and then translates it to a string
    public void GetDirection()
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

    //Generates a room to the north of the previous room
    public void GenerateNorth()
    {
        Ray forwardRay = new Ray(northRay.transform.position, northRay.transform.forward);
        Ray downwardRay = new Ray(northRay.transform.position, -northRay.transform.up);
        RaycastHit hit;
        RaycastHit otherHit;
        if (Physics.Raycast(forwardRay, out hit, 60f) && !Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("North ray hit wall");
            Vector3 corridorSpawn = new Vector3(northDoorSpawn.transform.position.x, northDoorSpawn.transform.position.y - 2.5f, northDoorSpawn.transform.position.z);
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, Quaternion.identity);
            spawnedObjects.Add(newCorridor);
        }
        else if(Physics.Raycast(forwardRay, out hit, 60f) && Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("North ray hit wall and corridor");
        }
        else
        {
            genZ += 100;
            roomSpawnPos = new Vector3(genX, genY, genZ);
            transform.position = roomSpawnPos;
            Vector3 corridorSpawn = new Vector3(northDoorSpawn.transform.position.x, northDoorSpawn.transform.position.y - 2.5f, northDoorSpawn.transform.position.z);
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, Quaternion.identity);
            spawnedObjects.Add(newCorridor);

            currentRoom = Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
            spawnedObjects.Add(currentRoom);

            FindRaysAndDoorSpawns();
        }
    }

    //Generates a room to the east of the previous room
    public void GenerateEast()
    {
        Ray forwardRay = new Ray(eastRay.transform.position, eastRay.transform.forward);
        Ray downwardRay = new Ray(eastRay.transform.position, -eastRay.transform.up);
        RaycastHit hit;
        RaycastHit otherHit;
        if (Physics.Raycast(forwardRay, out hit, 60f) && !Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("East Ray hit wall");
            Vector3 corridorSpawn = new Vector3(eastDoorSpawn.transform.position.x, eastDoorSpawn.transform.position.y - 2.5f, eastDoorSpawn.transform.position.z);
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, Quaternion.Euler(0, 90, 0));
            spawnedObjects.Add(newCorridor);
        }
        else if (Physics.Raycast(forwardRay, out hit, 60f) && Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("East ray hit wall and corridor");
        }
        else
        {
            genX += 100;
            roomSpawnPos = new Vector3(genX, genY, genZ);
            transform.position = roomSpawnPos;

            Vector3 corridorSpawn = new Vector3(eastDoorSpawn.transform.position.x, eastDoorSpawn.transform.position.y - 2.5f, eastDoorSpawn.transform.position.z);
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, Quaternion.Euler(0, 90, 0));
            spawnedObjects.Add(newCorridor);

            currentRoom = Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
            spawnedObjects.Add(currentRoom);

            FindRaysAndDoorSpawns(); ;
       }
    }

    //Generates a room to the south of the previous room
    public void GenerateSouth()
    {
        Ray forwardRay = new Ray(southRay.transform.position, southRay.transform.forward);
        Ray downwardRay = new Ray(southRay.transform.position, -southRay.transform.up);
        RaycastHit hit;
        RaycastHit otherHit;
        if (Physics.Raycast(forwardRay, out hit, 60f) && !Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("South Ray hit wall");
            Vector3 corridorSpawn = new Vector3(southDoorSpawn.transform.position.x, southDoorSpawn.transform.position.y - 2.5f, southDoorSpawn.transform.position.z);
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, Quaternion.Euler(0, 180, 0));
            spawnedObjects.Add(newCorridor);
        }
        else if (Physics.Raycast(forwardRay, out hit, 60f) && Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("South ray hit wall and corridor");
        }
        else
        {
            genZ -= 100;
            roomSpawnPos = new Vector3(genX, genY, genZ);
            transform.position = roomSpawnPos;

            Vector3 corridorSpawn = new Vector3(southDoorSpawn.transform.position.x, southDoorSpawn.transform.position.y - 2.5f, southDoorSpawn.transform.position.z);
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, Quaternion.Euler(0, 180, 0));
            spawnedObjects.Add(newCorridor);

            currentRoom = Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
            spawnedObjects.Add(currentRoom);

            FindRaysAndDoorSpawns();
        }
    }

    //Generates a room to the west of the previous room
    public void GenerateWest()
    {
        Ray forwardRay = new Ray(westRay.transform.position, westRay.transform.forward);
        Ray downwardRay = new Ray(westRay.transform.position, -westRay.transform.up);
        RaycastHit hit;
        RaycastHit otherHit;
        if (Physics.Raycast(forwardRay, out hit, 60f) && !Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("West ray hit wall");
            Vector3 corridorSpawn = new Vector3(westDoorSpawn.transform.position.x, westDoorSpawn.transform.position.y - 2.5f, westDoorSpawn.transform.position.z);
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, Quaternion.Euler(0, -90, 0));
            spawnedObjects.Add(newCorridor);
        }
        else if (Physics.Raycast(forwardRay, out hit, 60f) && Physics.Raycast(downwardRay, out otherHit, 10f))
        {
            Debug.Log("West ray hit wall and corridor");
        }
        else
        {
            genX -= 100;
            roomSpawnPos = new Vector3(genX, genY, genZ);
            transform.position = roomSpawnPos;
            Vector3 corridorSpawn = new Vector3(westDoorSpawn.transform.position.x, westDoorSpawn.transform.position.y - 2.5f, westDoorSpawn.transform.position.z);
            GameObject newCorridor = Instantiate(corridor, corridorSpawn, Quaternion.Euler(0, -90, 0));
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

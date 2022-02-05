


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateDungeonLevel : MonoBehaviour
{
    public Tilemap darkness;
    public Grid grid;
    public GameObject player;
    public float percentAlive;
    public int minSteps;
    public int maxSteps;

    #region
    //wall tiles
    public Tile wall;
    public Tile topwall;
    public Tile topright;
    public Tile topleft;

    #endregion

    //public Camera cam;

    // Start is called before the first frame update
    void Start()
    {





        // randomRooms();

        //generate rooms
        //getBSPMap();

        //int[,] map = Automata.MakeMap(percentAlive, Random.Range(darkness.size.x/2, darkness.size.x), Random.Range(darkness.size.y/2, darkness.size.y), Random.Range(minSteps,maxSteps));
        int[,] map = Automata.accreteRooms(darkness.size.x, darkness.size.y);//Automata.getBSPMap(darkness.size.x, darkness.size.y);


        //transfer map into tilesheet.
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 0)
                {
                    darkness.SetTile(new Vector3Int(i, j, 0), null);
                }
                else if (map[i, j] == 2)
                { 
                    darkness.SetTile(new Vector3Int(i, j, 0), wall);
                }

                switch (map[i, j]) 
                {
                    case 0:
                        darkness.SetTile(new Vector3Int(i, j, 0), null);
                        break;
                    case 1:
                        break;
                    case 2:
                        darkness.SetTile(new Vector3Int(i, j, 0), wall);
                        break;
                    case 3:
                        darkness.SetTile(new Vector3Int(i, j, 0), topwall);
                        break;
                    case 4:
                        darkness.SetTile(new Vector3Int(i, j, 0), topleft);
                        break;
                    case 5:
                        darkness.SetTile(new Vector3Int(i, j, 0), topright);
                        break;
                }
            }
        }

        //player.transform.position = new Vector3(LevelData.room[0].centerX, LevelData.room[0].centerY, 0);

        int randx;
        int randy;

        do
        {
            randx = Random.Range(0, map.GetLength(0));
            randy = Random.Range(0, map.GetLength(1));
            
        }
        while (map[randx, randy] != 0);
        
        player.transform.position = new Vector3(randx, randy, 0);
        //darkness.SetTile(new Vector3Int(100, 100, 0), null); // Remove tile at 0,0,0
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void SetMultipleTiles(Vector3Int start, Vector3Int toPoint, Tile tile) 
    {
        // sets several tiles to a given value
        for (int i = start.x ; i <= toPoint.x; i++)
        {
            for (int j = start.y ; j < toPoint.y; j++)
            {
                darkness.SetTile(new Vector3Int(i, j, 0), tile); // set the tile
            }
        }
    
    }

    void randomRooms() 
    {
       // // Generate rooms by randomly placing them and checking if they collide
       // int numberRooms = Random.Range(5, 15);
       // for (int i = 0; i < numberRooms; i++)
       // {
       //     Room generatedRoom;
       //
       //     do
       //     {
       //         //Debug.Log(i);
       //         int roomHeight = Random.Range(2, 15);
       //         int roomWidth = Random.Range(roomHeight -1, roomHeight * 2);
       //
       //         int roomX = Random.Range(1, darkness.size.x - 1 - roomWidth);
       //         int roomY = Random.Range(1, darkness.size.y - 1 - roomHeight);
       //
       //         generatedRoom = new Room(roomX, roomY, roomWidth, roomHeight);
       //
       //     }
       //     while (IsRoomValid(generatedRoom) == false);
       //
       //     LevelData.room.Add(generatedRoom);
       // }

    }



}

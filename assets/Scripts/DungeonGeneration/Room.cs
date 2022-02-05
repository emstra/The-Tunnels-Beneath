using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    //Room Width
    //Room Height
    //Room X
    //Room Y

    public int width = 3;
    public int height = 3;
    public int x;
    public int y;
    public int centerX;
    public int centerY;
    public bool visited;
    public int[,] roomMap;// walls and floor layers
    public int[,] roomObjects;// space for game objects
    public Room parent;

    public Room(int roomX, int roomY, int roomWidth, int roomHeight, Room parent)
    {
        //constructor

        x = roomX;//partition.partitionX;// + 1;
        y = roomY;//partition.partitionY; //+ 1;

        width = roomWidth ;//Random.Range(2, partition.partitionWidth);

            //Debug.Log("Error Width: " + partition.partitionWidth + "X: " + partition.partitionX + "Y: " + partition.partitionY);
            //RoomWidth = partition.partitionWidth;

        height = roomHeight ;//Random.Range(2, partition.partitionHeight);
        this.parent = parent;


        centerX = x + width / 2;
        centerY = y + height / 2;

        roomMap = new int[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                roomMap[i, j] = 0;

                if (i == 0 || i == width - 1  || j == 0 || j == height - 1 )
                {
                    
                    roomMap[i, j] = 1;
                }

            }
        }

        int cornerCuts = Random.Range(0,10);
        
        

        
        if (cornerCuts > 7)
        {
            roomMap[2, height - 2] = 1;
            roomMap[width - 3, height - 2] = 1;

            roomMap[2, 1] = 1;
            roomMap[width - 3, 1] = 1;
        }

        if (cornerCuts > 3)
        {
            roomMap[1, height - 2] = 1;
            roomMap[width - 2, height - 2] = 1;

            roomMap[1, 1] = 1;
            roomMap[width - 2, 1] = 1;

            cornerCuts = Random.Range(0, 10);
        }
        
        if (cornerCuts > 7)
        {
            roomMap[1, height - 3] = 1;
            roomMap[width - 2, height - 3] = 1;

            roomMap[1, 2] = 1;
            roomMap[width - 2, 2] = 1;
        }



        //
        //Program.room.Add(this);

        //LevelData.room.Add(this);
    }

    public Room(SpacePartition partition) 
    {
        //constructor

        //Random rand = new Random();

        x = partition.partitionX + 1;
        y = partition.partitionY + 1;
        try
        {
            width = Random.Range(2, partition.partitionWidth);
        }
        catch
        {
            Debug.Log("Error Width: " + partition.partitionWidth + "X: " + partition.partitionX + "Y: " + partition.partitionY);
            //RoomWidth = partition.partitionWidth;
        }

        try
        {
            height = Random.Range(2, partition.partitionHeight);
        }
        catch
        {
            Debug.Log("Error Height: " + partition.partitionHeight + "  X: " + partition.partitionX + "  Y: " + partition.partitionY);
            //RoomWidth = partition.partitionWidth;
        }

        centerX = x + width / 2;
        centerY = y + height / 2;

        //LevelData.room.Add(this);
    }
}

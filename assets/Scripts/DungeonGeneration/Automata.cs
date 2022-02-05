using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class Automata
{
    //private static float chanceAlive;
   // private static int width;
    //private static int height;
    //private static int numSteps;
    //private static int[,] map;
    
    //public static int[,] floodMap;

    // Generates a cave system
    public static int[,] MakeMap(float chanceAlive, int width, int height, int numSteps) 
    {
        int[,] map;
        ///call to make a new map
        map = new int[width, height];



        map = InitializeMap(width, height, chanceAlive, map);

        //step through the map
        for (int i = 0; i < numSteps; i++)
        {
            map = StepMap(width, height, map);
        }

        int startx = width / 2;
        int starty = height / 2;
        bool startFlood = false;
        do 
        {
            if (map[startx, starty] == 0)
            {
                startFlood = true;
            }
            else 
            {
                startx++;
            }
        }
        while (startFlood == false);

        map = FillRecursion(startx,starty, map);

        map = addWalls(map);

        return map;
    }


    private static int[,] InitializeMap(int width, int height, float chanceAlive, int[,] map) 
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (UnityEngine.Random.Range(0.0f, 1.0f) < chanceAlive)
                {
                    map[x, y] = 1;
                }
                else 
                {
                    map[x, y] = 0;
                }

            }
        }

        return map;
    }

    private static int[,] StepMap(int width, int height, int[,] map) 
    {
        //make the map do a step in the cellular automata



        int[,] newmap = map;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                int neighbors = getNumNeighbors(x, y, map);


                if (newmap[x,y] == 1)
                {
                    if (neighbors < 3)
                    {
                        newmap[x, y] = 0;
                    }
                    else 
                    {
                        newmap[x, y] = 1;
                    }
                }
                else
                {
                    if (neighbors > 5)
                    {
                        newmap[x,y] = 1;
                    }
                    else
                    {
                        newmap[x,y] = 0;
                    }
                }

            }
        }

        map = newmap;
        return map;

    }

    private static int getNumNeighbors(int x, int y, int[,] map) 
    {
        int neighbors = 0;

        for (int i = -1; i < 2; i++)
        {

            for (int j = -1; j < 2; j++)
            {

                if (i == 0 && j == 0)
                {
                    neighbors += 0;
                }
                else if (x + i < 0 || y + j < 0 || x + i >= map.GetLength(0) || y + j >= map.GetLength(1))
                {
                    // if array is out of bounds
                    neighbors += 1;
                }
                else if(map[x + i, y + j] >= 1)
                {
                    neighbors += 1;
                }

            }

        }

        return neighbors;
    }


    private static int[,] FillRecursion(int x, int y, int[,] map) 
    {
        int[,] floodedMap;
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        floodedMap = new int[width, height];

        //fill map
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                floodedMap[i, j] = 1;
            }
        }

        //set up queues for map creation
        Queue<Tuple<int, int>> fillQueue = new Queue<Tuple<int, int>>();
        List<Tuple<int, int>> allPoints = new List<Tuple<int, int>>();

        //put start into queue
        Tuple<int, int> point = new Tuple<int,int>( x, y);

        fillQueue.Enqueue(point);
        allPoints.Add(point);



        while (fillQueue.Count > 0)
        {
            //pick a point off the queue
            Tuple<int, int> testPoint = fillQueue.Dequeue();

            //if it is empty on the map
            if (map[testPoint.Item1, testPoint.Item2] == 0) {
                //set the flood point on the flood map to zero
                floodedMap[testPoint.Item1, testPoint.Item2] = 0;

                //add it's neighbors, check if they have already been added
                if (!allPoints.Any(p => p.Item1 == testPoint.Item1 + 1 && p.Item2 == testPoint.Item2) && testPoint.Item1 + 1 > 0 && testPoint.Item1 + 1 < width) 
                {
                    Tuple<int, int> adjPoint = new Tuple<int, int>(testPoint.Item1 + 1, testPoint.Item2);
                    fillQueue.Enqueue(adjPoint);
                    allPoints.Add(adjPoint);

                }
                if (!allPoints.Any(p => p.Item1 == testPoint.Item1 - 1 && p.Item2 == testPoint.Item2) && testPoint.Item1 - 1 > 0 && testPoint.Item1 - 1 < width)
                {
                    Tuple<int, int> adjPoint = new Tuple<int, int>(testPoint.Item1 - 1, testPoint.Item2);
                    fillQueue.Enqueue(adjPoint);
                    allPoints.Add(adjPoint);

                }
                if (!allPoints.Any(p => p.Item1 == testPoint.Item1 && p.Item2 == testPoint.Item2 - 1) && testPoint.Item2 - 1 > 0 && testPoint.Item2 - 1 < width)
                {
                    Tuple<int, int> adjPoint = new Tuple<int, int>(testPoint.Item1, testPoint.Item2+ 1);
                    fillQueue.Enqueue(adjPoint);
                    allPoints.Add(adjPoint);

                }
                if (!allPoints.Any(p => p.Item1 == testPoint.Item1 + 1 && p.Item2 == testPoint.Item2 - 1) && testPoint.Item2 - 1 > 0 && testPoint.Item2 - 1 < width)
                {
                    Tuple<int, int> adjPoint = new Tuple<int, int>(testPoint.Item1, testPoint.Item2 - 1);
                    fillQueue.Enqueue(adjPoint);
                    allPoints.Add(adjPoint);

                }
            }


            //pick another point, if there are points to pick
        }


        return floodedMap;
    }

    public static int[,] accreteRooms(int width, int height) 
    {
        int[,] map = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = 1;
            }
        }


        List<Room> rooms = new List<Room>();

        int rWidth = UnityEngine.Random.Range(6,16);
        int rHeight = UnityEngine.Random.Range(6, 16);
        int rX = 35;//UnityEngine.Random.Range(0, width-rWidth); ;
        int rY = 35;//UnityEngine.Random.Range(6, height-rHeight); ;

        Room room = new Room(rX,rY, rWidth, rHeight, null);
        rooms.Add(room);
        bool roomValid;

        for (int i = 0; i < UnityEngine.Random.Range(4, 11); i++)
        {


            Room secondRoom;
            int count = 0;
            do
            {
                room = rooms[UnityEngine.Random.Range(0,rooms.Count)];

                int direction = UnityEngine.Random.Range(0, 4);
                Debug.Log("Generating rooms");

                switch (direction)
                {
                    case 0:
                        //right room
                        rWidth = UnityEngine.Random.Range(6, 16);
                        rHeight = UnityEngine.Random.Range(6, 16);
                        rX = room.x + room.width;
                        rY = room.y + UnityEngine.Random.Range(-room.height / 2, room.height/2);//offsetY // + room.height;
                        break;
                    case 1:
                        //up room
                        rWidth = UnityEngine.Random.Range(6, 16);
                        rHeight = UnityEngine.Random.Range(6, 16);
                        rX = room.x + UnityEngine.Random.Range(-room.width / 2, room.width / 2);// + room.width;
                        rY = room.y + room.height;
                        break;
                    case 2:
                        rWidth = UnityEngine.Random.Range(6, 16);
                        rHeight = UnityEngine.Random.Range(6, 16);
                        rX = room.x - rWidth;
                        rY = room.y + UnityEngine.Random.Range(-room.height / 2, room.height / 2);// + room.height;
                        break;
                    case 3:
                        rWidth = UnityEngine.Random.Range(6, 16);
                        rHeight = UnityEngine.Random.Range(6, 16);
                        rX = room.x + UnityEngine.Random.Range(-room.width / 2, room.width / 2);
                        rY = room.y - rHeight;
                        break;
                }

                count++;

                secondRoom = new Room(rX, rY, rWidth, rHeight, room);

                roomValid = IsRoomValid(secondRoom, rooms, width, height);
                if (count > 10)
                {
                    roomValid = true;
                }

            } while (roomValid == false);

            roomValid = IsRoomValid(secondRoom, rooms, width, height);
            if (roomValid)
            {
                rooms.Add(secondRoom);
            }
            //room = secondRoom;
        }

        // carve into map
        foreach (Room rm in rooms)
        {
            for (int i = 0; i < rm.width; i++)
            {
                for (int j = 0; j < rm.height; j++)
                {
                    map[i + rm.x, j + rm.y] = rm.roomMap[i, j];
                }
            }
        }

        foreach (Room rm in rooms)
        {
            if (rm.visited == false)
            {
                Room end = rm.parent;

                //Hallfinder astar = new Hallfinder(new Location { X = room.centerX, Y = room.centerY }, new Location { X = end.centerX, Y = end.centerY }, bspMap);
                //bspMap = astar.map;

                if (end != null)
                {
                    map = Hallfinder.AstarHalls(new Location { X = rm.centerX, Y = rm.centerY }, new Location { X = end.centerX, Y = end.centerY }, map);
                }
            }

        }

        map = addWalls(map);

        return map;

    }

    public static int[,] getBSPMap(int width, int height)
    {
        //get a map made through bsp


        //set up a basic map filled with ones
        int[,] bspMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bspMap[x, y] = 1;
            }
        }


        SpacePartition rootPartition = new SpacePartition(0, 0, UnityEngine.Random.Range(width / 3, width), UnityEngine.Random.Range(height / 3, height), null);

        //put rooms into map
        foreach (Room r in LevelData.room) 
        {
            for (int x = 0; x < r.width; x++)
            {
                for (int y = 0; y < r.height; y++)
                {
                    bspMap[r.x + x, r.y + y] = 0;
                }
            }
        }


        int numCorridors = UnityEngine.Random.Range(LevelData.room.Count / 2, LevelData.room.Count * 2);

        for (int i = 0; i < numCorridors; i++)
        {
            // get two random rooms and A* between them
            Room start = LevelData.room[UnityEngine.Random.Range(0, LevelData.room.Count - 1)];
            Room end = LevelData.room[UnityEngine.Random.Range(0, LevelData.room.Count - 1)];

            start.visited = true;
            end.visited = true;

            

            //Hallfinder astar = new Hallfinder(new Location { X = start.centerX, Y = start.centerY }, new Location { X = end.centerX, Y = end.centerY }, bspMap);
            //bspMap = astar.map;
        }


        // make sure each room connects to at least one other room.
        foreach (Room room in LevelData.room)
        {
            if (room.visited == false)
            {
                Room end;
                do
                {
                    end = LevelData.room[UnityEngine.Random.Range(0, LevelData.room.Count - 1)];
                }
                while (end == room);

                //Hallfinder astar = new Hallfinder(new Location { X = room.centerX, Y = room.centerY }, new Location { X = end.centerX, Y = end.centerY }, bspMap);
                //bspMap = astar.map;
            }

        }


        bspMap = FillRecursion(LevelData.room[0].centerX, LevelData.room[0].centerY, bspMap);

        bspMap = addWalls(bspMap);

        return bspMap;
    }


    private static int[,] addWalls(int[,] map) 
    {

        for (int i = 1; i < map.GetLength(0); i++)
        {
            //map cleanup, add walls
            for (int j = 1; j < map.GetLength(1) ; j++)
            {


                if ( i < map.GetLength(0) -1 && j < map.GetLength(1) -1 && map[i, j] >= 1 && map[i, j + 1] == 0 && map[i, j - 1] == 0)
                {
                    map[i, j] = 0;
                }

                if (i == map.GetLength(0) -1 || j == map.GetLength(1) -1)
                {
                    map[i, j] = 1;
                }

            }
        }
        //figure out where wall tiles should go.
        int[,] wallmap;
        wallmap = map;

        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            //map cleanup, add walls
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                



                if (map[i, j] >= 1  && map[i, j - 1] >= 1 && map[i, j + 1] == 0 && map[i-1, j] == 0  && map[i + 1, j] == 0)
                {
                    //top side walls
                    wallmap[i, j] = 3;
                }

                if (map[i, j] >= 1 && map[i, j - 1] >= 1 && map[i, j + 1] == 0 && map[i - 1, j] == 0 && map[i + 1, j] >= 1)
                {
                    //top right walls
                    wallmap[i, j] = 4;
                }

                if (map[i, j] >= 1 && map[i, j - 1] >= 1 && map[i, j + 1] == 0 && map[i - 1, j] >= 1 && map[i + 1, j] == 0)
                {
                    //top left walls
                    wallmap[i, j] = 5;
                }


                if (map[i, j] >= 1 && map[i, j + 1] >= 1 && map[i, j - 1] == 0)
                {
                    wallmap[i, j] = 2;
                }

            }
        }

        return map;
    }



    private static bool IsRoomValid(Room room, List<Room> rooms, int mapwidth, int mapheight)
    {

        if (room.x < 0 || room.y < 0 || room.x + room.width > mapwidth || room.y + room.y > mapheight) 
        {
            //check if out of bounds
            return false;
        }

        //returns true if there is no overlap between rooms
        foreach (Room inDungeon in rooms)
        {
            if (room.x < inDungeon.x + inDungeon.width &&
                room.x + room.width  > inDungeon.x &&
                room.y < inDungeon.y + inDungeon.height &&
                room.y + room.height  > inDungeon.y) // check collision
            {
                return false;
            }

           
        }
        return true;

    }
}

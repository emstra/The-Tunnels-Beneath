using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePartition
{
    public int partitionX;
    public int partitionY;
    public int partitionWidth;
    public int partitionHeight;

    //bool isLeaf = false;

    public SpacePartition Parent;

    public SpacePartition ChildOne;
    public SpacePartition ChildTwo;

    public Room room;

    public SpacePartition(int X, int Y, int width, int height, SpacePartition parent)
    {
        partitionHeight = height;
        partitionWidth = width;
        partitionX = X;
        partitionY = Y;

        Parent = parent;



        PartitionSpace();

        //if (Parent != null) 
        //{
        //    Console.ForegroundColor = ConsoleColor.Gray;
        //
        //    if (Parent.Parent != null) 
        //    {
        //        Console.ForegroundColor = ConsoleColor.Yellow;
        //
        //        if (Parent.Parent.Parent != null) 
        //        {
        //            Console.ForegroundColor = ConsoleColor.DarkYellow;
        //        }
        //    }
        //}


        //Console.WriteLine("X: " + partitionX + ", Y: " + partitionY + "Height: " + partitionWidth + "Width: " + partitionHeight);
        //Console.ForegroundColor = ConsoleColor.White;

        //if (ChildOne == null && ChildTwo == null)
        //{
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine( "X: " + room.RoomX + ", Y: " + room.RoomY + "Height:" + room.RoomWidth + "Width: " + room.RoomHeight);
        //    Console.ForegroundColor = ConsoleColor.White;
        //}

    }


    public void PartitionSpace()
    {
        //create two child nodes

        //Random rand = new Random();
        int ToSplit = Random.Range(0, 7);

        //Check to see if area is big enough to split(10 x 10), and then if we choose to split it\
        if ((ToSplit != 0 && partitionWidth > 20 && partitionHeight > 20) || (partitionWidth >= 50 && partitionHeight >= 50))
        {
            //Check if splitting height or width

            // SpacePartition(int X, int Y, int width, int height, SpacePartition parent) 

            if (ToSplit < 5)
            {
                //if so split it by height
                ChildOne = new SpacePartition(partitionX, partitionY, partitionWidth, partitionHeight / 2, this);

                ChildTwo = new SpacePartition(partitionX, partitionY + partitionHeight / 2, partitionWidth, partitionHeight / 2, this);
            }
            else
            {
                //if so split it by width
                ChildOne = new SpacePartition(partitionX, partitionY, partitionWidth / 2, partitionHeight, this);

                ChildTwo = new SpacePartition(partitionX + partitionWidth / 2, partitionY, partitionWidth / 2, partitionHeight, this);
            }
        }
        else if (ChildOne == null && ChildTwo == null)
        {
            //spawn a room
            //isLeaf = true;

            room = new Room(this);
        }


    }
}

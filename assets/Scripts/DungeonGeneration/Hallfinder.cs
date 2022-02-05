using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public static class Hallfinder
{


   

    //Tilemap darkness;
    //public int[,] map;
    //int g = 0;// dist start location to current location

    public static int[,] AstarHalls(Location start, Location target, int[,] map)
    {

        //Location current;

        List<Location> openList = new List<Location>();
        List<Location> closedList = new List<Location>();

        openList.Add(start);

        //This is where we created the map from our previous step etc. 

        while (openList.Any())
        {
            Location checkTile = openList.OrderBy(x => x.F()).First();

            if (checkTile.X == target.X && checkTile.Y == target.Y)
            {
                //Console.WriteLine("We are at the destination!");
                //We can actually loop through the parents of each tile to find our exact path which we will show shortly. 

                //We found the destination and we can be sure (Because the the OrderBy above)
                //That it's the most low cost option. 
                var tile = checkTile;
                bool found = false;

                while (found == false)
                {
                    //Console.WriteLine($"{tile.X} : {tile.Y}");
                    //if (Program.map[tile.X,tile.Y] != null)
                    //{
                    //    //var newMapRow = map[tile.Y].ToCharArray();
                    //    //newMapRow[tile.X] = '*';
                    //    //map[tile.Y] = new string(newMapRow);
                    //    ///
                    //}

                    //Program.map[tile.Y - 1, tile.X - 1] = 0;
                    //darkness.SetTile(new Vector3Int(tile.Y, tile.X, 0), null); // set the tile
                    map[tile.X, tile.Y] = 0;

                    tile = tile.Parent;
                    if (tile == null)
                    {
                        //Console.WriteLine("Map looks like :");
                        //map.ForEach(x => Console.WriteLine(x));
                        //Console.WriteLine("Done!");
                        found = true;
                    }
                }

                //return;
            }

            closedList.Add(checkTile);
            openList.Remove(checkTile);

            List<Location> walkableTiles = GetWalkableLocations(map, checkTile, target);

            foreach (Location walkableTile in walkableTiles)
            {
                //We have already visited this tile so we don't need to do so again!
                if (closedList.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    continue;

                //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                if (openList.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                {
                    Location existingTile = openList.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                    if (existingTile.F() > checkTile.F())
                    {
                        openList.Remove(existingTile);
                        openList.Add(walkableTile);
                    }
                }
                else
                {
                    //We've never seen this tile before so add it to the list. 
                    openList.Add(walkableTile);
                }
            }
        }

        Debug.Log("No Path Found!");

        return map;

    }





    private static List<Location> GetWalkableLocations(int[,] map, Location currentTile, Location targetTile)
    {
        // creates a list of possible next locations
        var possibleTiles = new List<Location>()
        {
            new Location { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, G = currentTile.G + 1 },
            new Location { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, G = currentTile.G + 1 },
            new Location { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, G = currentTile.G + 1 },
            new Location { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, G = currentTile.G + 1 },
        };

        possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

        var maxX = map.GetLength(0);
        var maxY = map.GetLength(1);

        return possibleTiles
                .Where(tile => tile.X >= 0 && tile.X <= maxX)
                .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                //.Where(tile => map[tile.Y,tile.X] == ' ' || map[tile.Y,tile.X] == 'B')
                .ToList();
    }
}

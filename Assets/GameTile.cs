using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    GameTile north, east, south, west, nextOnPath;
    
    static Quaternion northRotation = Quaternion.Euler(0f, 0f, 0f),
        eastRotation = Quaternion.Euler(0f, 0f, 270f),
        southRotation = Quaternion.Euler(0f, 0f, 180f),
        westRotation = Quaternion.Euler(0f, 0f, 90f);
    
    int distance;
    
    [SerializeField] Transform arrow = default;

    public static void MakeEastWestNeighbors(GameTile east, GameTile west)
    {
        Debug.Assert(
            west.east == null && east.west == null, "Redefined neighbors!"
            );
        west.east = east;
        east.west = west;
    }
    
    public static void MakeNorthSouthNeighbors(GameTile north, GameTile south)
    {
        Debug.Assert(
            south.north == null && north.south == null, "Redefined neighbors!"
            );
        south.north = north;
        north.south = south;
    }

    public void ClearPath()
    {
        distance = int.MaxValue;
        nextOnPath = null;
    }
    
    public void BecomeDestination()
    {
        distance = 0;
        nextOnPath = null;
    }
    
    public bool HasPath => distance != int.MaxValue;

    GameTile GrowPathTo(GameTile neighbor)
    {
        Debug.Assert(HasPath, "No Path!");
        if (neighbor == null || neighbor.HasPath)
        {
            return null;
        }
        neighbor.distance = distance + 1;
        neighbor.nextOnPath = this;
        return neighbor;
    }
    public GameTile GrowPathNorth() => GrowPathTo(north);
    
    public GameTile GrowPathEast() => GrowPathTo(east);
    
    public GameTile GrowPathSouth() => GrowPathTo(south);
    
    public GameTile GrowPathWest() => GrowPathTo(west);

    public void ShowPath()
    {
        if (distance == 0)
        {
            arrow.gameObject.SetActive(false);
            return;
        }
        arrow.gameObject.SetActive(true);
        arrow.localRotation = nextOnPath == north ? northRotation :
            nextOnPath == east ? eastRotation :
            nextOnPath == south ? southRotation :
            westRotation;
    }
    
}

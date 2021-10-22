using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bfsPathfindingScript : MonoBehaviour
{
    //0 = empty space 
    //1 = wall
    //-1 = player
    //-2 = goal
    //-3 = the path that the player follows 
    //-4 = path the player has walked on
    //Each path has their own number, and only the number is retained at the end
    //Each square that a number touches cannot be passed again, and when a certain number hits a dead end it comes back if the value it comes back to is less than that of another
    //As soon as another path is more efficient, it gains priority  
    public int[,] pathfindingArray = new int[,]{{ 0, 0, 0, 0, 0},
                                                { 0, 0, 0, 0,-2},
                                                { 0, 0, 0, 0, 0},
                                                { 0,-1, 0, 0, 0},
                                                { 0, 0, 0, 0, 0}};
    public Dictionary<int, int> findNewPath = new Dictionary<int, int>(); // this will store the path that everything bridges off of.
    public List<Vector4> alternatePaths; // x and y are coordinates and z is the distance and w is the path that it is apart of
    public Vector2 pathfinderLocation;
    public Vector2 playerLocation;
    public Vector2 targetLocation;
    void BFSPathfinding(int[,] array)
    {
        int currentPath = 2;
        int pathnumber = 2;
        findNewPath.Add(2, -1);
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                if (array[y,x] == -1)
                {
                    playerLocation = new Vector2(x, y);
                    pathfinderLocation = new Vector2(x, y);
                }
                else if (array[y,x] == -2)
                {
                    targetLocation = new Vector2(x, y);
                }
            }
        }
        while (pathfinderLocation != targetLocation)
        {
            Debug.Log("The square has a: " + array[(int)pathfinderLocation.y, (int)pathfinderLocation.x]);
            Vector4[] directionMoveCount = new Vector4[4]; // 0 = left, 1 = right, 2 = up, 3 = down // x and y equals place and z = distance
            Vector3 minMoves = new Vector3(-2, -2, -2);
            // To the find the distance, and availablitly of each move
            if (pathfinderLocation.x > 0 && array[(int)pathfinderLocation.y, (int)pathfinderLocation.x - 1] == 0)
            {
                int distance = (int)(Mathf.Abs(targetLocation.x - (pathfinderLocation.x - 1)) + Mathf.Abs(targetLocation.y - pathfinderLocation.y));
                pathnumber++;
                directionMoveCount[0] = new Vector4(pathfinderLocation.x - 1, pathfinderLocation.y, distance, pathnumber); 
                alternatePaths.Add(new Vector4(pathfinderLocation.x - 1, pathfinderLocation.y, distance, pathnumber));
                array[(int)pathfinderLocation.y, (int)(pathfinderLocation.x - 1)] = pathnumber;
                findNewPath.Add(pathnumber, currentPath);
            }
            else if (pathfinderLocation.x > 0 && array[(int)pathfinderLocation.y, (int)pathfinderLocation.x - 1] == -2)
            {
                directionMoveCount[0].z = -2;
            }
            else
            {
                directionMoveCount[0].z = -1;
            }
            if (pathfinderLocation.x < array.GetLength(1) - 1 && array[(int)pathfinderLocation.y, (int)pathfinderLocation.x + 1] == 0)
            {
                int distance = (int)(Mathf.Abs(targetLocation.x - (pathfinderLocation.x + 1)) + Mathf.Abs(targetLocation.y - pathfinderLocation.y));
                pathnumber++;
                directionMoveCount[1] = new Vector4(pathfinderLocation.x + 1, pathfinderLocation.y, distance, pathnumber);                
                alternatePaths.Add(new Vector4(pathfinderLocation.x + 1, pathfinderLocation.y, distance, pathnumber));
                array[(int)pathfinderLocation.y, (int)(pathfinderLocation.x + 1)] = pathnumber;
                findNewPath.Add(pathnumber, currentPath);
            }
            else if (pathfinderLocation.x < array.GetLength(1) - 1 && array[(int)pathfinderLocation.y, (int)pathfinderLocation.x + 1] == -2)
            {
                directionMoveCount[1].z = -2;
            }
            else
            {
                directionMoveCount[1].z = -1;
            }
            if (pathfinderLocation.y < array.GetLength(0) - 1 && array[(int)pathfinderLocation.y + 1, (int)pathfinderLocation.x] == 0)
            {
                int distance = (int)(Mathf.Abs(targetLocation.x - pathfinderLocation.x) + Mathf.Abs(targetLocation.y - (pathfinderLocation.y + 1)));
                pathnumber++;
                directionMoveCount[2] = new Vector4(pathfinderLocation.x, pathfinderLocation.y + 1, distance, pathnumber);               
                alternatePaths.Add(new Vector4(pathfinderLocation.x, pathfinderLocation.y + 1, distance, pathnumber));
                array[(int)(pathfinderLocation.y + 1), (int)(pathfinderLocation.x)] = pathnumber;
                findNewPath.Add(pathnumber, currentPath);
            }
            else if (pathfinderLocation.y < array.GetLength(0) - 1 && array[(int)pathfinderLocation.y + 1, (int)pathfinderLocation.x] == -2)
            {
                directionMoveCount[2].z = -2;
            }
            else
            {
                directionMoveCount[2].z = -1;
            }
            if (pathfinderLocation.y > 0 && array[(int)pathfinderLocation.y - 1, (int)pathfinderLocation.x] == 0)
            {
                int distance = (int)(Mathf.Abs(targetLocation.x - pathfinderLocation.x) + Mathf.Abs(targetLocation.y - (pathfinderLocation.y - 1)));
                pathnumber++;
                directionMoveCount[3] = new Vector4(pathfinderLocation.x, pathfinderLocation.y -1, distance, pathnumber);
                alternatePaths.Add(new Vector4(pathfinderLocation.x, pathfinderLocation.y - 1, distance, pathnumber));
                array[(int)(pathfinderLocation.y - 1), (int)(pathfinderLocation.x)] = pathnumber;
                findNewPath.Add(pathnumber, currentPath);
            }
            else if (pathfinderLocation.y > 0 && array[(int)pathfinderLocation.y - 1, (int)pathfinderLocation.x] == -2)
            {
                directionMoveCount[3].z = -2;
            }
            else
            {
                directionMoveCount[3].z = -1;
            }
            
            for (int i = 0; i < directionMoveCount.Length; i++) // to the find the min move out of possible moves
            {                
                if (minMoves.z == -2 && directionMoveCount[i].z > -1) 
                { 
                    minMoves = new Vector3(directionMoveCount[i].x, directionMoveCount[i].y, directionMoveCount[i].z);
                    currentPath = (int)directionMoveCount[i].w;
                }
                else if (directionMoveCount[i].z > -1 && minMoves.z > directionMoveCount[i].z) 
                {
                    minMoves = new Vector3(directionMoveCount[i].x, directionMoveCount[i].y, directionMoveCount[i].z);
                    currentPath = (int)directionMoveCount[i].w;
                }
                if (directionMoveCount[i].z == -2) { minMoves = new Vector3(targetLocation.x, targetLocation.y, 0); }
                //Debug.Log("The min Moves is: " + "(" + minMoves.x + ", " + minMoves.y + ")");
            }
            if (minMoves.z == -2) //getting rid of a path if it is trapped
            {
                for (int i = 0; i < alternatePaths.Count; i++)
                {
                    if (alternatePaths[i].x == pathfinderLocation.x && alternatePaths[i].y == pathfinderLocation.y)
                    {
                        alternatePaths.RemoveAt(i);
                    }
                }
            }
            for (int i = 0; i < alternatePaths.Count; i++) // find another path that is faster
            {
                //Debug.Log("The minDistance for a alternate")
                if (minMoves.z == -2)
                {
                    minMoves = new Vector3(alternatePaths[i].x, alternatePaths[i].y, alternatePaths[i].z);
                    currentPath = (int)alternatePaths[i].w;
                }
                else if (minMoves.z > alternatePaths[i].z)
                {
                    minMoves = new Vector3(alternatePaths[i].x, alternatePaths[i].y, alternatePaths[i].z);
                    currentPath = (int)alternatePaths[i].w;
                }
            }
            pathfinderLocation = new Vector2(minMoves.x, minMoves.y);
            array[(int)pathfinderLocation.y, (int)pathfinderLocation.x] = currentPath;
        }
        while (array[(int)pathfinderLocation.y, (int)pathfinderLocation.x] != -1)
        {
            Debug.Log("The tile I am standing on now is: " + array[(int)pathfinderLocation.y, (int)pathfinderLocation.x]);
            array[(int)pathfinderLocation.y, (int)pathfinderLocation.x] = -3;
            if (pathfinderLocation.x > 0 && array[(int)(pathfinderLocation.y), (int)(pathfinderLocation.x - 1)] == currentPath) //check left for current path
            {
                pathfinderLocation = new Vector2(pathfinderLocation.x - 1, pathfinderLocation.y);
                continue;
            }
            else if (pathfinderLocation.x < array.GetLength(1) - 1 && array[(int)(pathfinderLocation.y), (int)(pathfinderLocation.x + 1)] == currentPath) //check right for current path
            {
                pathfinderLocation = new Vector2(pathfinderLocation.x + 1, pathfinderLocation.y);
                continue;
            }
            else if (pathfinderLocation.y < array.GetLength(0) - 1 && array[(int)(pathfinderLocation.y + 1), (int)(pathfinderLocation.x)] == currentPath) //check up for current path
            {
                pathfinderLocation = new Vector2(pathfinderLocation.x, pathfinderLocation.y + 1);
                continue;
            }
            else if (pathfinderLocation.y > 0 && array[(int)(pathfinderLocation.y - 1), (int)(pathfinderLocation.x)] == currentPath) //check down for current path
            {
                pathfinderLocation = new Vector2(pathfinderLocation.x, pathfinderLocation.y - 1);
                continue;
            }

            //Debug.Log("The current path is: " + currentPath);
            currentPath = findNewPath[currentPath]; // change current path

            if (pathfinderLocation.x > 0 && array[(int)(pathfinderLocation.y), (int)(pathfinderLocation.x - 1)] == -1) //check left for -1
            {
                pathfinderLocation = new Vector2(pathfinderLocation.x - 1, pathfinderLocation.y);
                Debug.Log("I found -1");
                continue;
            }
            else if (pathfinderLocation.x < array.GetLength(1) - 1 && array[(int)(pathfinderLocation.y), (int)(pathfinderLocation.x + 1)] == -1) //check right for -1
            {
                pathfinderLocation = new Vector2(pathfinderLocation.x + 1, pathfinderLocation.y);
                Debug.Log("I found -1");
                continue;
            }
            else if (pathfinderLocation.y < array.GetLength(0) - 1 && array[(int)(pathfinderLocation.y + 1), (int)(pathfinderLocation.x)] == -1) //check up for -1
            {
                pathfinderLocation = new Vector2(pathfinderLocation.x, pathfinderLocation.y + 1);
                Debug.Log("I found -1");
                continue;
            }
            else if (pathfinderLocation.y > 0 && array[(int)(pathfinderLocation.y - 1), (int)(pathfinderLocation.x)] == -1) //check down for -1
            {
                pathfinderLocation = new Vector2(pathfinderLocation.x, pathfinderLocation.y - 1);
                Debug.Log("I found -1");
                continue;
            }
        }
        while (playerLocation != targetLocation)
        {
            if (playerLocation.x > 0 && array[(int)(playerLocation.y), (int)(playerLocation.x - 1)] == -3) //check left for -3
            {
                playerLocation = new Vector2(playerLocation.x - 1, playerLocation.y);
                array[(int)playerLocation.y, (int)playerLocation.x] = -4;
                continue;
            }
            else if (playerLocation.x < array.GetLength(1) - 1 && array[(int)(playerLocation.y), (int)(playerLocation.x + 1)] == -3) //check right for -3
            {
                playerLocation = new Vector2(playerLocation.x + 1, playerLocation.y);
                array[(int)playerLocation.y, (int)playerLocation.x] = -4;
                continue;
            }
            else if (playerLocation.y < array.GetLength(0) - 1 && array[(int)(playerLocation.y + 1), (int)(playerLocation.x)] == -3) //check up for -3
            {
                playerLocation = new Vector2(playerLocation.x, playerLocation.y + 1);
                array[(int)playerLocation.y, (int)playerLocation.x] = -4;
                continue;
            }
            else if (playerLocation.y > 0 && array[(int)(playerLocation.y - 1), (int)(playerLocation.x)] == -3) //check down for -3
            {
                playerLocation = new Vector2(playerLocation.x, playerLocation.y - 1);
                array[(int)playerLocation.y, (int)playerLocation.x] = -4;
                continue;
            }
        }
        //Print out the entire array
        Debug.Log(" " + array[0, 0] + ", " + array[0, 1] + ", " + array[0, 2] + ", " + array[0, 3] + ", " + array[0, 4]);
        Debug.Log(" " + array[1, 0] + ", " + array[1, 1] + ", " + array[1, 2] + ", " + array[1, 3] + ", " + array[1, 4]);
        Debug.Log(" " + array[2, 0] + ", " + array[2, 1] + ", " + array[2, 2] + ", " + array[2, 3] + ", " + array[2, 4]);
        Debug.Log(" " + array[3, 0] + ", " + array[3, 1] + ", " + array[3, 2] + ", " + array[3, 3] + ", " + array[3, 4]);
        Debug.Log(" " + array[4, 0] + ", " + array[4, 1] + ", " + array[4, 2] + ", " + array[4, 3] + ", " + array[4, 4]);
    }
    // Start is called before the first frame update
    void Start()
    {
        BFSPathfinding(pathfindingArray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

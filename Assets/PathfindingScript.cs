using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingScript : MonoBehaviour
{
    public List<Vector2> moveArray;
    public List<Vector4> possibleMoves;
    public Vector2 playerLocation = new Vector2(-1, -1);
    public Vector2 pathfinderLocation = new Vector2(-1, -1);
    public Vector2 targetLocation = new Vector2(-1, -1);
    //0 = empty space
    //1 = obstruction
    //-2 = flag
    //-1 = player
    //-3 = somewhere the player walks
    //For this first AI, the player won't be able to walk on the same square twice, and then I will optimize it, so this won't be needed
    //always pick the smallest number, but if the numbers are the same then pick randomly
    //move to the shortest alternate path when stuck
    public int[,] pathfindingArray = new int[,] { { 0, 0, 0, 0,-2},
                                                  { 0, 0, 0, 1, 1},
                                                  { 0, 0, 0, 1, 1},
                                                  { 0, 0, 0, 1, 0},
                                                  {-1, 0, 0, 0, 0}};
    void pathfinding(int[,] array)
    {
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                if (array[y,x] == -1)
                {
                    playerLocation = new Vector2(x, y);
                    break;
                }
            }
        }
        pathfinderLocation = new Vector2(playerLocation.x, playerLocation.y);
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                if (array[y,x] == -2)
                {
                    targetLocation = new Vector2(x, y);
                    break;
                }
            }
        }
        while (pathfinderLocation != targetLocation)
        {
            int[] differentMoveCounts = new int[4]; // 0 = left, 1 = right, 2 = up, 3 = down
            int minMoves = -2;
            if (pathfinderLocation.x > 0 && array[(int)pathfinderLocation.y, (int)pathfinderLocation.x - 1] != 1 && array[(int)pathfinderLocation.y, (int)pathfinderLocation.x - 1] != -3) //left
            {
                differentMoveCounts[0] = (int)(Mathf.Abs(targetLocation.x - (pathfinderLocation.x - 1)) + Mathf.Abs(targetLocation.y - pathfinderLocation.y));
            }
            else
            {
                differentMoveCounts[0] = -1;
            }
            if (pathfinderLocation.x < array.GetLength(1) - 1 && array[(int)pathfinderLocation.y, (int)pathfinderLocation.x + 1] != 1 && array[(int)pathfinderLocation.y, (int)pathfinderLocation.x + 1] != -3) //right
            {
                differentMoveCounts[1] = (int)(Mathf.Abs(targetLocation.x - (pathfinderLocation.x + 1)) + Mathf.Abs(targetLocation.y - pathfinderLocation.y));
            }
            else
            {
                differentMoveCounts[1] = -1;
            }
            if (pathfinderLocation.y < array.GetLength(0) - 1 && array[(int)pathfinderLocation.y + 1, (int)pathfinderLocation.x] != 1 && array[(int)pathfinderLocation.y + 1, (int)pathfinderLocation.x] != -3) //up
            {
                differentMoveCounts[2] = (int)(Mathf.Abs(targetLocation.x - pathfinderLocation.x) + Mathf.Abs(targetLocation.y - (pathfinderLocation.y + 1)));
            }
            else
            {
                differentMoveCounts[2] = -1;
            }
            if (pathfinderLocation.y > 0 && array[(int)pathfinderLocation.y - 1, (int)pathfinderLocation.x] != 1 && array[(int)pathfinderLocation.y - 1, (int)pathfinderLocation.x] != -3) //down
            {
                differentMoveCounts[3] = (int)(Mathf.Abs(targetLocation.x - pathfinderLocation.x) + Mathf.Abs(targetLocation.y - (pathfinderLocation.y - 1)));
            }
            else
            {
                differentMoveCounts[3] = -1;
            }
            //figure out if you need to go left, right, up, or down// I don't need this, just make it so that the tile the player moves on is a -3 and when the player can't move, back track until he can
            for (int i = 0; i < differentMoveCounts.Length; i++)
            {
                if (minMoves == -2 && differentMoveCounts[i] >= 0) { minMoves = differentMoveCounts[i]; }
                else if (differentMoveCounts[i] == -1) {  }
                else if (minMoves > differentMoveCounts[i]) { minMoves = differentMoveCounts[i]; }
                else if (minMoves == differentMoveCounts[i]) 
                {
                    if (i == 0) { possibleMoves.Add(new Vector4(pathfinderLocation.x - 1, pathfinderLocation.y, differentMoveCounts[i], moveArray.Count)); }
                    else if (i == 1) { possibleMoves.Add(new Vector4(pathfinderLocation.x + 1, pathfinderLocation.y, differentMoveCounts[i], moveArray.Count)); }
                    else if (i == 2) { possibleMoves.Add(new Vector4(pathfinderLocation.x, pathfinderLocation.y + 1, differentMoveCounts[i], moveArray.Count)); }
                    else if (i == 3) { possibleMoves.Add(new Vector4(pathfinderLocation.x, pathfinderLocation.y - 1, differentMoveCounts[i], moveArray.Count)); }
                }
            }
            array[(int)pathfinderLocation.y, (int)pathfinderLocation.x] = -3;
            if (minMoves > 0 || minMoves == -2)
            {
                if (differentMoveCounts[0] == minMoves)
                {
                    pathfinderLocation = new Vector2(pathfinderLocation.x - 1, pathfinderLocation.y);
                    moveArray.Add(pathfinderLocation);
                }
                else if (differentMoveCounts[1] == minMoves)
                {
                    pathfinderLocation = new Vector2(pathfinderLocation.x + 1, pathfinderLocation.y);
                    moveArray.Add(pathfinderLocation);
                }
                else if (differentMoveCounts[2] == minMoves)
                {
                    pathfinderLocation = new Vector2(pathfinderLocation.x, pathfinderLocation.y + 1);
                    moveArray.Add(pathfinderLocation);
                }
                else if (differentMoveCounts[3] == minMoves)
                {
                    pathfinderLocation = new Vector2(pathfinderLocation.x, pathfinderLocation.y - 1);
                    moveArray.Add(pathfinderLocation);
                }
                else
                {
                    Vector3 minRecountIndex = new Vector3(-1, -1, -1);
                    for (int i = 0; i < possibleMoves.Count; i++)
                    {
                        if (minRecountIndex.x == -1)
                        {
                            minRecountIndex.x = possibleMoves[i].z;
                            minRecountIndex.y = possibleMoves[i].w;
                            minRecountIndex.z = i;
                        }
                        else if (minRecountIndex.x > possibleMoves[i].z)
                        {
                            minRecountIndex.x = possibleMoves[i].z;
                            minRecountIndex.y = possibleMoves[i].w;
                            minRecountIndex.z = i;
                        }
                    }
                    pathfinderLocation = new Vector2(possibleMoves[(int)minRecountIndex.z].x, possibleMoves[(int)minRecountIndex.z].y);
                    for (int i = (int)minRecountIndex.y; i < moveArray.Count + 1; i++)
                    {
                        moveArray.Remove(moveArray[i]);
                    }
                    moveArray[(int)possibleMoves[(int)minRecountIndex.z].w] = pathfinderLocation;
                    possibleMoves.Remove(possibleMoves[(int)minRecountIndex.z]);



                }
            }
            else
            {
                pathfinderLocation = targetLocation;
                moveArray.Add(pathfinderLocation);
                Debug.Log("The size of the move array is: " + moveArray.Count);
            }
            /*Debug.Log("The amount of moves it would take if you moved left would be: " + differentMoveCounts[0]);
            Debug.Log("The amount of moves it would take if you moved right would be: " + differentMoveCounts[1]);
            Debug.Log("The amount of moves it would take if you moved up would be: " + differentMoveCounts[2]);
            Debug.Log("The amount of moves it would take if you moved down would be: " + differentMoveCounts[3]);*/
            //pathfinderLocation = targetLocation;
        }
        for (int i = 0; i < moveArray.Count; i++)
        {
            playerLocation = moveArray[i];
        }
        /*Debug.Log("Player's x is: " + playerLocation.x);
        Debug.Log("Player's y is: " + playerLocation.y);
        Debug.Log("Target's x is: " + targetLocation.x);
        Debug.Log("Target's y is: " + targetLocation.y);*/
    }
    // Start is called before the first frame update
    void Start()
    {
        pathfinding(pathfindingArray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

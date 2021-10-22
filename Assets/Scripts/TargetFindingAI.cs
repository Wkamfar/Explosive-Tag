using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFindingAI : MonoBehaviour
{
    public int[,] findMaxTargetArray = new int[,] {{0, 1, 1, 1, 0, 0, 1, 1, 0, 1},
                                                   {1, 0, 1, 0, 0, 1, 1, 0, 1, 1},
                                                   {0, 0, 1, 0, 0, 0, 0, 1, 1, 1},
                                                   {0, 1, 1, 1, 1, 0, 0, 0, 1, 1}};
    private Dictionary<int, int> nationMap = new Dictionary<int, int>();
    int findMaxTarget(int[,] array)
    {
        int maxTargets = 0;
        int nationID = 0;
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                int ans = 0;
                if (array[y, x] == 1)
                {
                    nationID--;
                    DFS(array, x, y, ref ans, nationID);
                    nationMap.Add(nationID, ans);
                    //maxTargets = maxTargets < ans ? ans : maxTargets;
                    maxTargets = Mathf.Max(maxTargets, ans);
                }
            }
        }
        return maxTargets;
    }
    Vector3 BuildTurretMax(int[,] array)
    {
        int maxBuildTarget = findMaxTarget(array);
        Vector2 maxBuildLocation = new Vector2();
        for (int y = 0; y < array.GetLength(0); y++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                if (array[y, x] == 0)
                {
                    int temporaryMax = 1;
                    Dictionary<int, bool> nationIndentification = new Dictionary<int, bool>();
                    bool wasCounted = false;
                    if (x > 0 && array[y, x - 1] <= -1)
                    {
                        temporaryMax += nationMap[array[y, x - 1]];
                        nationIndentification.Add(array[y, x - 1], true);
                    }
                    if (x < array.GetLength(1) - 1 && array[y, x + 1] <= -1)
                    {
                        wasCounted = nationIndentification.TryGetValue(array[y, x + 1], out wasCounted) ? true : false;
                        if (wasCounted == false)
                        {
                            temporaryMax += nationMap[array[y, x + 1]];
                            nationIndentification.Add(array[y, x + 1], true);
                        }
                    }
                    if (y < array.GetLength(0) - 1 && array[y + 1, x] <= -1)
                    {
                        wasCounted = nationIndentification.TryGetValue(array[y + 1, x], out wasCounted) ? true : false;
                        if (wasCounted == false)
                        {
                            temporaryMax += nationMap[array[y + 1, x]];
                            nationIndentification.Add(array[y + 1, x], true);
                        }  
                    }
                    if (y > 0 && array[y - 1, x] <= -1)
                    {
                        wasCounted = nationIndentification.TryGetValue(array[y - 1, x], out wasCounted) ? true : false;
                        if (wasCounted == false)
                        {
                            temporaryMax += nationMap[array[y - 1, x]];
                            nationIndentification.Add(array[y - 1, x], true);
                        }
                    }
                    if (temporaryMax > maxBuildTarget)
                    {
                        maxBuildTarget = temporaryMax;
                        maxBuildLocation = new Vector2(x, y);
                    }
                }
            }
        }
        return new Vector3(maxBuildLocation.x, maxBuildLocation.y, maxBuildTarget);
    }
    void DFS(int[,] array, int x, int y, ref int ans, int nationID)
    {
        if (x < 0 || y < 0 || x >= array.GetLength(1) || y >= array.GetLength(0) || array[y, x] <= 0 )
        {
            return;
        }
        array[y, x] = nationID;
        ans++;
        DFS(array, x - 1, y, ref ans, nationID);
        DFS(array, x + 1, y, ref ans, nationID);
        DFS(array, x, y - 1, ref ans, nationID);
        DFS(array, x, y + 1, ref ans, nationID);
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("The max number of targets is: " + findMaxTarget(findMaxTargetArray));
        Vector3 placeholder = BuildTurretMax(findMaxTargetArray);
        Debug.Log("The location of the turret would be: (" + placeholder.x + ", " + placeholder.y + ")" + " and the nation size would be: " + placeholder.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

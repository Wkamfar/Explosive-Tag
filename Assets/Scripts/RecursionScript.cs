using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursionScript : MonoBehaviour
{
    public int intN;
    public int discs;
    public int[] binarySearchArray;
    public int numberInArray;
    public int[] randomArray;
    public int randomArraySize;
    public int min;
    public int max;
    //ABSequence Values
    public int maxN;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("The Fibonacci Squence number " + intN + " is " + FibonacciSequence(intN));
        //Debug.Log("The Tower of Hanoi solution is: ");
        //TowerOfHonai(discs, 'a', 'c', 'b');
        //Debug.Log("The number " + numberInArray + " is in index " + binarySearch(binarySearchArray, numberInArray, 0, binarySearchArray.Length));
        SettingUpRandomArray();
        //Debug.Log("The max peak in the array is " + findMaxPeak(binarySearchArray));
        //Debug.Log("The max peak recurssive in the array is " + findMaxPeak(binarySearchArray, 0, binarySearchArray.Length));
        //ABSequence(maxN);
        //ABSequenceNoMod(maxN);
        Debug.Log("The outcome of the game is: " + PickingHighestSumGame(randomArray, 1, 0, randomArray.Length - 1));
        PickingHighestSumGameNoRecurssion(randomArray);
    }
    void SettingUpRandomArray()
    {
        randomArray = new int[randomArraySize];
        for (int i = 0; i < randomArray.Length; i++)
        {
            randomArray[i] = Random.Range(min, max);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    int FibonacciSequence(int index) // 1,1,2,3,5,8,13,21 // 5,4,3,2,1
    {
        if (index <= 1) { return index; }
        return FibonacciSequence(index - 1) + FibonacciSequence(index - 2);
    }
    void TowerOfHonai(int numberOfBlocks, char source, char destination, char extra) // 1 2 3 // four parameters // Debug.Log("moving block " + numberOfBlocks + " from " + source + " to " + destination);
    {
        if (numberOfBlocks <= 1)
        {
            Debug.Log("moving block " + numberOfBlocks + " from " + source + " to " + destination);
            return;
        }
        TowerOfHonai(numberOfBlocks - 1, source, extra, destination);
        Debug.Log("moving block " + numberOfBlocks + " from " + source + " to " + destination);
        TowerOfHonai(numberOfBlocks - 1, extra, destination, source);
    }
    int binarySearch(int[] array, int searchNumber, int left, int right)
    {

        Debug.Log("Number of Searches");
        int middle = (right + left) / 2;
        //Debug.Log("Middle is " + middle);
        //Debug.Log("Left is " + left);
        //Debug.Log("Right is " + right);
        if (array[middle] == searchNumber)
        {
            return middle;
        }
        else if (left >= right)
        {
            //Debug.Log("Number does not exist returning -1");
            return -1;
        }
        else if (searchNumber > array[middle]) { return binarySearch(array, searchNumber, middle + 1, right); }
        else { return binarySearch(array, searchNumber, left, middle - 1); }
    }
    int findMaxPeak(int[] array)
    {
        if (array.Length == 0)
        {
            Debug.Log("The array is empty");
            return 0;
        }
        else if (array.Length == 1)
        {
            return array[0];
        }
        else if (array[0] > array[1])
        {
            return array[0];
        }
        else if (array[array.Length - 1] > array[array.Length - 2])
        {
            return array[array.Length - 1];
        }
        for (int i = 1; i < (array.Length - 1); i++)
        {
            Debug.Log("Number of Times findMaxPeak Loops");
            if (array[i] > array[i - 1] && array[i] > array[i + 1])
            {
                return array[i];
            }
        }
        return 0;
    }
    int findMaxPeak(int[] array, int left, int right)
    {
        Debug.Log("find max peak recurssive solution was called");
        if (array.Length == 0)
        {
            Debug.Log("The array is empty");
            return 0;
        }
        else if (array.Length == 1)
        {
            return array[0];
        }
        else if (array[0] > array[1])
        {
            return array[0];
        }
        else if (array[array.Length - 1] > array[array.Length - 2])
        {
            return array[array.Length - 1];
        }
        //recurssive base case
        int middle = (left + right) / 2;
        if (array[middle] > array[middle - 1] && array[middle] > array[middle + 1])
        {
            return array[middle];
        }
        else if (array[middle] < array[middle - 1])
        {
            right = middle - 1;
        }
        else if (array[middle] < array[middle + 1])
        {
            left = middle + 1;
        }
        return findMaxPeak(array, left, right);
    }
    void ABSequence(int maxNum)
    {
        //print a if i is divisable by 2 print b if i is divisable by 3 print a and b if i is divisible by 2 and 3 else print i
        for (int i = 0; i < maxNum; i++)
        {
            if (i % 2 == 0)
            {
                if (i % 3 == 0)
                {
                    Debug.Log("AB: i is " + i);
                }
                else
                {
                    Debug.Log("A: i is " + i);
                }
            }
            else if (i % 3 == 0)
            {
                Debug.Log("B: i is " + i);
            }
            else
            {
                Debug.Log("i is " + i);
            }
        }
    }
    void ABSequenceNoMod(int maxNum)
    {
        //print a if i is divisable by 2 print b if i is divisable by 3 print a and b if i is divisible by 2 and 3 else print i
        string[] debugs = new string[] { "AB: i is ", "i is ", "A: i is ", "B: i is ", "A: i is ", "i is " };
        for (int i = 0; i < maxNum; i++)
        {
            Debug.Log(debugs[i % 6] + i);
        }
    }
    int PickingHighestSumGame(int[] array, int player, int startingPos, int endPos)
    {
        if (startingPos == endPos)
        {
            return array[startingPos] * player;
        }
        int e = array[endPos] * player + PickingHighestSumGame(array, -player, startingPos, endPos - 1);
        int s = array[startingPos] * player + PickingHighestSumGame(array, -player, startingPos + 1, endPos);
        return player * Mathf.Max(e * player, s * player);
    }
    void PickingHighestSumGameNoRecurssion(int[] array)
    {
        if (array.Length % 2 == 0) { Debug.Log("Player 1 wins"); }
        else { Debug.Log("Player 2 wins"); }
    }
}

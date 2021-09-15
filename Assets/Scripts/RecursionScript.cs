using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursionScript : MonoBehaviour
{
    public int intN;
    public int discs;
    public int[] binarySearchArray;
    public int numberInArray;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("The Fibonacci Squence number " + intN + " is " + FibonacciSequence(intN));
        //Debug.Log("The Tower of Hanoi solution is: ");
        //TowerOfHonai(discs, 'a', 'c', 'b');
        Debug.Log("The number " + numberInArray + " is in index " + binarySearch(binarySearchArray, numberInArray, 0, binarySearchArray.Length));
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
        TowerOfHonai(numberOfBlocks - 1, extra, destination , source);
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
}

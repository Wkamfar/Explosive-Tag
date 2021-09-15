using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursionScrip2 : MonoBehaviour
{
    public int intN;
    public int discs;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("The Fibonacci Squence number " + intN + " is " + FibonacciSequence(intN));
        Debug.Log("The Tower of Hanoi solution is: ");
        TowerOfHonai(discs, 'a', 'c', 'b');
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
    /*int binarySearch(int[] array, int searchNumber, int left, int right)
    {

    }*/
}
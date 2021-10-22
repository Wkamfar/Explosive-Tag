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
    //RearangeVowels
    public char[] wordArray;
    //Airplane seat problem
    public int numOfSeats;
    public int[] personSeats;
    public int[] takenSeats;
    //Stair problem
    public int stairProblemNumber;
    //money amount
    public float dollars;
    //Mill points 
    public int[] milkArray;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("The Fibonacci Squence number " + intN + " is " + FibonacciSequence(intN));
        //Debug.Log("The Tower of Hanoi solution is: ");
        //TowerOfHonai(discs, 'a', 'c', 'b');
        //Debug.Log("The number " + numberInArray + " is in index " + binarySearch(binarySearchArray, numberInArray, 0, binarySearchArray.Length));
        //SettingUpRandomArray();
        //Debug.Log("The max peak in the array is " + findMaxPeak(binarySearchArray));
        //Debug.Log("The max peak recurssive in the array is " + findMaxPeak(binarySearchArray, 0, binarySearchArray.Length));
        //ABSequence(maxN);
        //ABSequenceNoMod(maxN);
        //Debug.Log("The outcome of the game is: " + PickingHighestSumGame(randomArray, 1, 0, randomArray.Length - 1));
        //PickingHighestSumGameNoRecurssion(randomArray);
        //Debug.Log("The largest area of the random array is: " + MaxMilkContainer(randomArray));
        //RearrangeVowels(wordArray);
        //ReverseVowelOrder(wordArray);
        //Debug.Log("The number of possible ways to climb the stairs are: " + StairProblem(stairProblemNumber));
        //Debug.Log("The number of possible ways with no recurrsion to climb the stairs are: " + StairProblemNoRecurrsion(stairProblemNumber));
        //Debug.Log("The number of correct possibilities is: " + AirplaneSeatsProblem(numOfSeats));
        //Debug.Log("The number of possible arrangements is: " + AllChangePossibilities(dollars));
        Debug.Log("The amount of milk that can fit in the gaps of this array is: " + FillMilk(milkArray));
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
    int MaxMilkContainer(int[] array)
    {
        int left = 0;
        int right = array.Length - 1;
        int maxArea = 0;
        while(left < right)
        {
            maxArea = maxArea < ((right - left) * Mathf.Min(array[left], array[right])) ? ((right - left) * Mathf.Min(array[left], array[right])) : maxArea;
            if (array[left] > array[right]) { right--; }
            else { left++; }
            //maxArea = Mathf.Max(Mathf.Min(array[left], array[right]) * (right - left), maxArea);
        }
        return maxArea;
    }
    void RearrangeVowels(char[] array)
    {
        char[] vowelArray = new char[array.Length];
        int[] vowelLocationArray = new int[array.Length];
        int vowelCount = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 'a' || array[i] == 'e' || array[i] == 'u' || array[i] == 'i' || array[i] == 'o')
            {
                vowelArray[i] = array[i];
                vowelLocationArray[vowelCount] = i;
                vowelCount++;
            }
            else
            {
                continue;
            }
        }
        int[] vowelLocationCopy = new int[vowelLocationArray.Length];
        int[] newVowelLocationArray = new int[vowelLocationArray.Length];
        for (int i = 0; i < vowelCount; i++)
        {
            newVowelLocationArray[i] = vowelLocationArray[i];
            vowelLocationCopy[i] = vowelLocationArray[i];
        }
        for (int i = 0; i < vowelCount; i++)
        {
            int randNum = Random.Range(0, vowelCount);
            newVowelLocationArray[i] = vowelLocationCopy[randNum];
            newVowelLocationArray[randNum] = vowelLocationCopy[i];
            for (int n = 0; n < vowelCount; n++)
            {
                vowelLocationCopy[n] = newVowelLocationArray[n];
            }
        }
        for (int i = 0; i < vowelCount; i++)
        {
            array[vowelLocationArray[i]] = vowelArray[newVowelLocationArray[i]];
        }
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log("The Letter of the scrambled vowel word is: " + array[i] + " and the number it is the " + i + " letter.");
        }
    }
    void ReverseVowelOrder(char[] array)
    {
        int leftVowel = 0;
        int rightVowel = array.Length - 1;
        while (leftVowel < rightVowel)
        {
             if (!IsVowel(array[leftVowel]))
             {
                leftVowel++;
             }
             if (!IsVowel(array[rightVowel]))
             {
                rightVowel--;
             }
             if (IsVowel(array[leftVowel]) && IsVowel(array[rightVowel]))
             {
                char x = array[leftVowel];
                array[leftVowel] = array[rightVowel];
                array[rightVowel] = x;
                rightVowel--;
                leftVowel++;
            }
        }
    }
    bool IsVowel(char letter)
    {
        if (letter == 'a' || letter == 'e' || letter == 'i' || letter == 'o' || letter == 'u')
        {
            return true;
        }
        return false;
    }
    int StairProblem(int numOfStairs)
    {
        Debug.Log("climbing Stairs");
        if (numOfStairs < 3)
        {
            return 1;
        }
        return StairProblem(numOfStairs - 3) + StairProblem(numOfStairs - 1);
    }
    int StairProblemNoRecurrsion(int numOfStairs)
    {
        int[] dp = new int[] { 0, 1, 1, 2 };
        for (int i = 3; i < numOfStairs; i++)
        {
            dp[0] = dp[1];
            dp[1] = dp[2];
            dp[2] = dp[3];
            dp[3] = dp[2] + dp[0]; // {2, 3, 4, 6}
        }
        return numOfStairs < 3 ? dp[numOfStairs] : dp[3];
    }
    int AllChangePossibilities(float money)
    {
        int cents = (int)(money * 100);
        int[] change = new int[4];
        if (cents >= 25)
        {
            change[3] = cents / 25;
            cents = cents % 25;
        }
        if (cents >= 10)
        {
            change[2] = cents / 10;
            cents = cents % 10;
        }
        if (cents >= 5)
        {
            change[1] = cents / 5;
            cents = cents % 5;
        }
        if (cents >= 1)
        {
            change[0] = cents;
            cents = 0;
        }
        return change[3] * 12 + change[2] * 3 + change[1] * 1 + change[0] * 0 + 1;
    }
    int FillMilk(int[] array)
    {
        int left = 0;
        int right = array.Length - 1;
        int max = array[0];
        int count = 0;
        int sum = 0;
        int ans = 0;
        while (left < right)
        {
            if (array[left] >= max)
            {               
                ans += count * max - sum;
                max = array[left];
                count = 0;
                sum = 0;
            }
            else
            {
                count++;
                sum += array[left];
            }
            //max = array[left] >= max ? array[left] : max;
            left++;
        }
        left = right - count - 2;
        left = left < 0 ? 0 : left;
        count = 0;
        sum = 0;
        max = 0;
        while (left < right)
        {
            if (array[right] >= max)
            {
                ans += count * max - sum;
                max = array[right];
                count = 0;
                sum = 0;
            }
            else
            {
                count++;
                sum += array[right];
            }
            right--;
        }
        return ans;
    }
}

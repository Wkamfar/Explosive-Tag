using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FindWordsScript : MonoBehaviour
{
    private List<string> dictionary = new List<string>();
    public char specialLetter;
    private Dictionary<char, bool> letters;
    public char[] validLetters;
    // Start is called before the first frame update
    void Start()
    {
        letters = new Dictionary<char, bool>();
        for (int i = 0; i < validLetters.Length; i++)
        {
            letters.Add(validLetters[i], true);
        }
        LoadDictionary("C:\\Users\\wkamf\\Explosive Tag\\dictionary.txt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadDictionary(string filePath)
    {
        StreamReader input = new StreamReader(filePath);
        while (!input.EndOfStream)
        {
            string word = input.ReadLine();
            if (word.Length >= 4 && IsWordValid(word, specialLetter, letters))
            {
                dictionary.Add(word);
            }
        }
        string largest = "";
        int size = 0;
        for (int i = 0; i < dictionary.Count; i++)
        {
            if (size < dictionary[i].Length)
            {
                size = dictionary[i].Length;
                largest = dictionary[i];
            }
            Debug.Log("Word Number " + (i + 1) + " is: " + dictionary[i]);
        }
        Debug.Log("The largest word is: " + largest);
    }
    bool IsWordValid(string word, char _specialLetter, Dictionary<char, bool> useableLetters)
    {
        bool invalidLetter = false;
        bool specialLetterExists = false;
        for (int i = 0; i < word.Length; i++)
        {
            if (word[i] == _specialLetter)
            {
                specialLetterExists = true;
                continue;
            }
            invalidLetter = useableLetters.TryGetValue(word[i], out invalidLetter) ? false : true;
            if (invalidLetter)
            {
                return false;
            }
        }
        return specialLetterExists;
    }
}

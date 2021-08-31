using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class GameManagerScript : MonoBehaviour
{
    //Make it so that if everyone votes the timer drops down to five seconds if it no already below that point. // Make the timer that is syncronized
    //Camera and Minimap references, so that they only activate after he spawns
    public Camera mainCamera;
    public GameObject minimap;
    public Camera minimapCamera;
    //Map Selector
    public GameObject mapSelector;

    public GameObject[] maps;

    private int map1Number;
    private int map2Number;
    private int map3Number;

    private bool hasVoted;
    private int myVoteLocation;

    private int map1VoteCount;
    private int map2VoteCount;
    private int map3VoteCount;
    private int noVoteCount;

    public TextMeshProUGUI map1VoteCounter;
    public TextMeshProUGUI map2VoteCounter;
    public TextMeshProUGUI map3VoteCounter;
    public TextMeshProUGUI noVoteCounter;

    public GameObject map1Display;
    public GameObject map2Display;
    public GameObject map3Display;

    public TextMeshProUGUI mapSelectorTimerDisplay;
    public float mapSelectorTimer = 30; //Make this using Photon time and syncronize all of this with the server on wednesday or sunday
    //Class Selector // when the player either locks in the charcter or runs out of time they will be given that specific character // have the time drop down to five when everyone confirms their character
    public GameObject classSelector;
    public GameObject[] classSelectorButtons;
    public GameObject[] classSelectorDescriptions;
    private int classNumber;
    private int confirmedClassNumber = -1;

    public TextMeshProUGUI classSelectorTimerDisplay;
    public float classSelectorTimer = 30;
    //private bool lockedInCharacter;
    //InGame UI // Maybe reference the Character Controller and after you make all of the selections then it spawns you into the game.
    public GameObject playerPrefab;
    public GameObject mainCanvas;
    //Death UI / spectator mode // activate when the player dies, so call it from the CharacterContoller.cs
    public GameObject deathCanvas;
    //menu UI
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {

        //Initialize the map Selector
        map1Number = Random.Range(0, maps.Length);
        map2Number = Random.Range(0, maps.Length);
        map3Number = Random.Range(0, maps.Length);
        while (map2Number == map1Number || map3Number == map2Number || map3Number == map1Number)
        {
            map2Number = Random.Range(0, maps.Length);
            map3Number = Random.Range(0, maps.Length);
        }
        map1Display.GetComponent<RawImage>().texture = maps[map1Number].GetComponent<MapDataScript>().mapTexture;
        map2Display.GetComponent<RawImage>().texture = maps[map2Number].GetComponent<MapDataScript>().mapTexture;
        map3Display.GetComponent<RawImage>().texture = maps[map3Number].GetComponent<MapDataScript>().mapTexture;
        //Initialize the UI
        mapSelector.SetActive(true);
        classSelector.SetActive(false);
        mainCanvas.SetActive(false);
        deathCanvas.SetActive(false);
        pauseMenu.SetActive(false);
        //Initialize the class selector
        ClassSelection(0);

    }

    // Update is called once per frame
    void Update()
    {
        if(mapSelector.activeInHierarchy)
            MapSelection();
        else if(classSelector.activeInHierarchy)
            ClassSelection();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeInHierarchy)
                pauseMenu.SetActive(true);
            else
                pauseMenu.SetActive(false);
        }
    }
    public void MapSelection()
    {
        //Display Votes
        map1VoteCounter.text = map1VoteCount.ToString();
        map2VoteCounter.text = map2VoteCount.ToString();
        map3VoteCounter.text = map3VoteCount.ToString();
        noVoteCounter.text = noVoteCount.ToString();
        //Displaying Timer
        mapSelectorTimerDisplay.text = Mathf.Round(mapSelectorTimer).ToString();
        //For Deciding the map
        if (mapSelectorTimer <= 0)
        {
            int randNum;
            if (map1VoteCount > map2VoteCount && map1VoteCount > map3VoteCount && map1VoteCount > noVoteCount)
                maps[map1Number].SetActive(true);
            else if (map2VoteCount > map1VoteCount && map2VoteCount > map3VoteCount && map2VoteCount > noVoteCount)
                maps[map2Number].SetActive(true);
            else if (map3VoteCount > map2VoteCount && map3VoteCount > map1VoteCount && map3VoteCount > noVoteCount)
                maps[map3Number].SetActive(true);
            else if (map1VoteCount == map2VoteCount && map3VoteCount != map2VoteCount)
            {
                randNum = Random.Range(1, 2);
                if(randNum == 1) { maps[map1Number].SetActive(true); }
                if (randNum == 2) { maps[map2Number].SetActive(true); }
            }
            else if (map2VoteCount == map3VoteCount && map3VoteCount != map1VoteCount)
            {
                randNum = Random.Range(1, 2);
                if (randNum == 1) { maps[map2Number].SetActive(true); }
                if (randNum == 2) { maps[map3Number].SetActive(true); }
            }

            else if (map1VoteCount == map3VoteCount && map3VoteCount != map2VoteCount)
            {
                randNum = Random.Range(1, 2);
                if (randNum == 1) { maps[map1Number].SetActive(true); }
                if (randNum == 2) { maps[map3Number].SetActive(true); }
            }

            else if (noVoteCount == map1VoteCount || noVoteCount == map2VoteCount || noVoteCount == map3VoteCount || map1VoteCount == map2VoteCount && map2VoteCount == map3VoteCount)
            {
                randNum = Random.Range(1, 3);
                if (randNum == 1) { maps[map1Number].SetActive(true); }
                if (randNum == 2) { maps[map2Number].SetActive(true); }
                if (randNum == 3) { maps[map3Number].SetActive(true); }
            }

            mapSelector.SetActive(false);
            classSelector.SetActive(true);
        }
    }
    private void MapSelector(int mapSelected)
    {
        if (!hasVoted)
        {
            if(mapSelected == 0)
            {
                map1VoteCount++;
                myVoteLocation = 0;
            }
            else if (mapSelected == 1)
            {
                map2VoteCount++;
                myVoteLocation = 1;
            }
            else if (mapSelected == 2)
            {
                map3VoteCount++;
                myVoteLocation = 2;
            }
            else if (mapSelected == 3)
            {
                noVoteCount++;
                myVoteLocation = 3;
            }
            hasVoted = true;
        }
        else
        {
            if (myVoteLocation == 0)
            {
                map1VoteCount--;
            }
            else if (myVoteLocation == 1)
            {
                map2VoteCount--;
            }
            else if (myVoteLocation == 2)
            {
                map3VoteCount--;
            }
            else if (myVoteLocation == 3)
            {
                noVoteCount--;
            }
            if (mapSelected == 0)
            {
                map1VoteCount++;
                myVoteLocation = 0;
            }
            else if (mapSelected == 1)
            {
                map2VoteCount++;
                myVoteLocation = 1;
            }
            else if (mapSelected == 2)
            {
                map3VoteCount++;
                myVoteLocation = 2;
            }
            else if (mapSelected == 3)
            {
                noVoteCount++;
                myVoteLocation = 3;
            }

        }
    }
    public void Map1Button()
    {
        MapSelector(0);
    }
    public void Map2Button()
    {
        MapSelector(1);
    }
    public void Map3Button()
    {
        MapSelector(2);
    }
    public void NoVoteButton()
    {
        MapSelector(3);
    }

    //Class Selection
    private void ClassSelection()
    {
        if(classSelectorTimer <= 0)
        {
            if (confirmedClassNumber == -1)
                confirmedClassNumber = classNumber;
            classSelector.SetActive(false);
            SpawnPlayer();
        }
    }
    public void ClassSelection(int ClassNumber)
    {
        classSelectorButtons[classNumber].transform.localScale = new Vector3(1, 1);
        classSelectorDescriptions[classNumber].SetActive(false);
        classNumber = ClassNumber;
        classSelectorDescriptions[classNumber].SetActive(true);
        classSelectorButtons[classNumber].transform.localScale = new Vector3(1.2f, 1.2f);
    }
    public void Addict()
    {
        ClassSelection(0);
    }
    public void Grappler()
    {
        ClassSelection(1);
    }
    public void Phoenix()
    {
        ClassSelection(2);
    }
    public void Dasher()
    {
        ClassSelection(3);
    }
    public void Shouter()
    {
        ClassSelection(4);
    }
    public void Engineer()
    {
        ClassSelection(5);
    }
    public void Phantom()
    {
        ClassSelection(6);
    }
    public void Skater()
    {
        ClassSelection(7);
    }
    public void Architect()
    {
        ClassSelection(8);
    }
    public void RandomClass()
    {
        ClassSelection(Random.Range(0, classSelectorButtons.Length));
    }
    public void ConfirmClass()
    {
        confirmedClassNumber = classNumber;
    }
    public int GetConfirmedNumber()
    {
        return confirmedClassNumber;
    }
    //Player Spawning 
    private void SpawnPlayer()
    {
        Vector3 position = new Vector3(Random.Range(-48, 48), 1, Random.Range(-48, 48));
        PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        mainCanvas.SetActive(true);
        mainCamera.GetComponent<CameraScript>().SearchForPlayer();
        minimap.GetComponent<MinimapScript>().SearchForPlayer();
        minimapCamera.GetComponent<MinimapController>().SearchForPlayer();
    }
    //Death Canvas
    public void DeathCanvas() // Make a random death message or a custom one, that would be pretty cool
    {
        deathCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }
    //pause menu
    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
}

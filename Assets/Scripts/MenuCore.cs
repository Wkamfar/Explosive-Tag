using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuCore : MonoBehaviour
{
    public Color selectedColor;
    public Color normalColor;
    public GameObject[] menuTabs;
    private int menuTabNumber = 0;

    public GameObject[] menuPages;
    //Game 
    public GameObject changeModeButton;
    public GameObject changeModeMenu;
    public GameObject[] tagModes;
    private bool isTag;
    private bool isInfection;
    private bool isFreezeTag;
    private int gameModeNumber;
    //Skins
    //Class
    public GameObject[] classIcons;
    public GameObject[] classDescriptions;
    private int classIconsNumber = 0;
    //Shop
    public GameObject[] shopTabs;
    public GameObject[] shopMenus;
    private int shopTabsNumber;
    //Friends
    public GameObject[] friendsTabs;
    public GameObject[] friendsMenu;
    private int friendsTabsNumber;
    //Settings
    public GameObject[] settingsTabs;
    public GameObject[] settingsMenu;
    private int settingsTabNumber;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("MenuCore.Start:" + classIcons.Length);
        GameCore(0);
        SkinsCore(0);
        ClassesCore(0);
        ShopCore(0);
        FriendsCore(0);
        SettingsCore(0);
    }

    // Update is called once per frame
    void Update()
    {
        MenuController();
    }
    void MenuController()
    {
        menuPages[menuTabNumber].SetActive(false);
        menuTabs[menuTabNumber].GetComponent<Button>().GetComponent<Image>().color = normalColor;
        if (Input.GetKeyDown(KeyCode.D))
            menuTabNumber = (menuTabNumber + 1)% menuTabs.Length;

        else if (Input.GetKeyDown(KeyCode.A))
            menuTabNumber = menuTabNumber == 0 ? menuTabs.Length - 1 : menuTabNumber - 1;

        menuTabs[menuTabNumber].GetComponent<Button>().GetComponent<Image>().color = selectedColor;
        menuPages[menuTabNumber].SetActive(true);
    }
    void MenuController(int tabNumber)
    {
        menuPages[menuTabNumber].SetActive(false);
        menuTabs[menuTabNumber].GetComponent<Button>().GetComponent<Image>().color = normalColor;
        menuTabNumber = tabNumber;
        menuTabs[menuTabNumber].GetComponent<Button>().GetComponent<Image>().color = selectedColor;
        menuPages[menuTabNumber].SetActive(true);
    }
    public void Game()
    {
        MenuController(0);
    }
    public void Skins()
    {
        MenuController(1);
    }
    public void Class()
    {
        MenuController(2);
    }
    public void Shop()
    {
        MenuController(3);
    }
    public void Friend()
    {
        MenuController(4);
    }
    public void Settings()
    {
        MenuController(5);
    }
    public void GameCore(int gameNumber)
    {
        tagModes[gameModeNumber].GetComponent<Button>().GetComponent<RawImage>().color = normalColor;
        gameModeNumber = gameNumber;
        tagModes[gameModeNumber].GetComponent<Button>().GetComponent<RawImage>().color = selectedColor;
        CloseGameModeMenu();
        isTag = (gameModeNumber == 0);
        isInfection = (gameModeNumber == 1);
        isFreezeTag = (gameModeNumber == 2);
        //Debug.Log("MenuCore.GameCore: The mode are " + isTag + isInfection + isFreezeTag);
    }
    public void Tag()
    {
        GameCore(0);
    }
    public void Infection()
    {
        GameCore(1);
    }
    public void FreezeTag()
    {
        GameCore(2);
    }
    public void OpenGameModeMenu()
    {
        changeModeButton.SetActive(false);
        changeModeMenu.SetActive(true);
    }
    public void CloseGameModeMenu()
    {
        changeModeButton.SetActive(true);
        changeModeMenu.SetActive(false);
    }
    private void SkinsCore(int SkinsNumber)
    {

    }

    public void ClassesCore()
    {
        
    }
    void ClassesCore(int classNumber)
    {
        classDescriptions[classIconsNumber].SetActive(false);
        classIcons[classIconsNumber].transform.localScale = new Vector3(1, 1, 1);
        classIconsNumber = classNumber;
        classIcons[classIconsNumber].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        classDescriptions[classIconsNumber].SetActive(true);
    }
    public void Addict()
    {
        ClassesCore(0);
    }
    public void Grappler()
    {
        ClassesCore(1);
    }
    public void Phoenix()
    {
        ClassesCore(2);
    }
    public void Dasher()
    {
        ClassesCore(3);
    }
    public void Shouter()
    {
        ClassesCore(4);
    }
    public void Engineer()
    {
        ClassesCore(5);
    }
    public void Phantom()
    {
        ClassesCore(6);
    }
    public void Skater()
    {
        ClassesCore(7);
    }
    public void Architect()
    {
        ClassesCore(8);
    }
    public void RandomClass()
    {
        ClassesCore(Random.Range(0, classIcons.Length));
        
    }
    private void ShopCore(int ShopNumber)
    {
        shopMenus[shopTabsNumber].SetActive(false);
        shopTabs[shopTabsNumber].GetComponent<Button>().GetComponent<Image>().color = normalColor;
        shopTabsNumber = ShopNumber;
        shopTabs[shopTabsNumber].GetComponent<Button>().GetComponent<Image>().color = selectedColor;
        shopMenus[shopTabsNumber].SetActive(true);
    }
    public void Store()
    {
        ShopCore(0);
    }
    public void Battlepass()
    {
        ShopCore(1);
    }
    private void FriendsCore(int friendsNumber)
    {
        friendsMenu[friendsTabsNumber].SetActive(false);
        friendsTabs[friendsTabsNumber].GetComponent<Button>().GetComponent<Image>().color = normalColor;
        friendsTabsNumber = friendsNumber;
        friendsTabs[friendsTabsNumber].GetComponent<Button>().GetComponent<Image>().color = selectedColor;
        friendsMenu[friendsTabsNumber].SetActive(true);
    }
    public void FriendList()
    {
        FriendsCore(0);
    }
    public void AddFriends()
    {
        FriendsCore(1);
    }
    public void PendingInvites()
    {
        FriendsCore(2);
    }
    private void SettingsCore(int settingsNumber)
    {
        settingsMenu[settingsTabNumber].SetActive(false);
        settingsTabs[settingsTabNumber].GetComponent<Button>().GetComponent<Image>().color = normalColor;
        settingsTabNumber = settingsNumber;
        settingsTabs[settingsTabNumber].GetComponent<Button>().GetComponent<Image>().color = selectedColor;
        settingsMenu[settingsTabNumber].SetActive(true);
    }
    public void General()
    {
        SettingsCore(0);
    }
    public void Graphics()
    {
        SettingsCore(1);
    }
    public void Audio()
    {
        SettingsCore(2);
    }
    public void Controls()
    {
        SettingsCore(3);
    }
    public void Account()
    {
        SettingsCore(4);
    }
}

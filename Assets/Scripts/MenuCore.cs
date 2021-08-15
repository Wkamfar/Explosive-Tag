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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MenuController();
    }
    void MenuController()
    {
        menuTabs[menuTabNumber].GetComponent<Button>().GetComponent<Image>().color = normalColor;
        if (Input.GetKeyDown(KeyCode.D))
            menuTabNumber = (menuTabNumber + 1)% menuTabs.Length;

        else if (Input.GetKeyDown(KeyCode.A))
            menuTabNumber = menuTabNumber == 0 ? menuTabs.Length - 1 : menuTabNumber - 1;

        menuTabs[menuTabNumber].GetComponent<Button>().GetComponent<Image>().color = selectedColor;
    }
    void MenuController(int tabNumber)
    {
        menuTabs[menuTabNumber].GetComponent<Button>().GetComponent<Image>().color = normalColor;
        menuTabNumber = tabNumber;
        menuTabs[menuTabNumber].GetComponent<Button>().GetComponent<Image>().color = selectedColor;
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
}

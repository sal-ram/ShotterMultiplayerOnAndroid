using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Menu[] menus;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string nameMenu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].nameMenu == nameMenu)
            {
                menus[i].Open();
            }
            else if (menus[i].open == true)
            {
                menus[i].Close();
            }
        }
    }
    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                menus[i].Close();
            }
        }

        menu.Open();
    }
}

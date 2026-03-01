using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Menu_ID {
    MainMenu
}

public class MenuManager : Manager
{
    public Menu[] Menus = {
        new Menu(Menu_ID.MainMenu, "MainMenu", SceneIndex.MainMenu, new MenuMonomanager.MainMenu())
    };
    public Menu CurrentMenu;
    public bool isMenuRunning = false;

    public override void MenuStart_Event()
    {
        SceneManager.LoadSceneAsync((int) CurrentMenu.SceneIndex);
    }
    public override void Update_Event()
    {
        if (isMenuRunning) CurrentMenu.Monomanager.Update_Event();
    }
}

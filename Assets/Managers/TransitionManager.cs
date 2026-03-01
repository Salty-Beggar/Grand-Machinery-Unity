using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TransitionManager : Manager
{
    private bool isTransitioning = false;

    private void StartTransitioning() {
        isTransitioning = true;
    }

    private void EndTransitioning() {
        isTransitioning = false;
    }

    #region Menu
    private bool IsGoingToMenu = false;
    private bool IsExittingMenu = false;
    private Menu TargetMenu;

    public void GoToMenu(Menu TargetMenu) {
        StartTransitioning();
        IsGoingToMenu = true;
        this.TargetMenu = TargetMenu;
    }
    public void GoToMenu(Menu_ID TargetMenuID) {
        StartTransitioning();
        IsGoingToMenu = true;
        this.TargetMenu = Game.MenuManager.Menus[(int) TargetMenuID];
    }
    public void GoToMenuApply() {
        IsGoingToMenu = false;
        Game.MenuManager.CurrentMenu = TargetMenu;
        Game.MenuManager.isMenuRunning = true;
        Game.GameSupermanager.MenuStart_Event();
    }

    #endregion

    public override void Update_Event() {
        if (isTransitioning) {
            if (IsExittingMenu) {
                // TODO001 - Exitting menu code
            }

            if (IsGoingToMenu) {
                GoToMenuApply();
            }
            EndTransitioning();
        }
    }

}

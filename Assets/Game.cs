using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Game {

    #region Managers
    public static GameSupermanager GameSupermanager;
    public static MenuManager MenuManager = GameSupermanager.MenuManager;
    public static TransitionManager TransitionManager = GameSupermanager.TransitionManager;
    public static FPSManager FPSManager = GameSupermanager.FPSManager;
    public static InputManager InputManager = GameSupermanager.InputManager;
    public static GameplayManager GameplayManager = GameSupermanager.GameplayManager;
        #region Gameplay submanagers
        public static EntityAssetSubmanager EntityAssetSubmanager = GameplayManager.EntityAssetSubmanager;
        public static InterfaceSubmanager InterfaceSubmanager = GameplayManager.InterfaceSubmanager;
        public static AestheticsSubmanager AestheticsSubmanager = GameplayManager.AestheticsSubmanager;
        public static BlueprintSubmanager BlueprintSubmanager = GameplayManager.BlueprintSubmanager;
        #endregion
    #endregion

    public static PlayerInput Input;
}
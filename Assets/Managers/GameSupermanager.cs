using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SceneIndex {
    Setup,
    MainMenu
}
public class GameSupermanager : Manager
{
    public static MenuManager MenuManager = new();
    public static TransitionManager TransitionManager = new();
    public static FPSManager FPSManager = new();
    public static InputManager InputManager = new();
    public static GameplayManager GameplayManager = new();
    public GameObject parentObject;

    public GameSupermanager(GameObject ParentObject)
    {
        parentObject = ParentObject;
        _subManagers = new Manager[] {
            MenuManager,
            TransitionManager,
            FPSManager,
            InputManager,
            GameplayManager
        };
    }

    public override void Awake_Event()
    {
        Game.FPSManager.SetupFPS(); // TODO001 - Change the position of this function.
        Game.Input = parentObject.GetComponent<PlayerInput>();
        Object.DontDestroyOnLoad(parentObject);
        
        base.Awake_Event();
    }
    public override void Update_Event()
    {
        base.Update_Event();
    }
    public override void LateUpdate_Event()
    {
        base.LateUpdate_Event();
    }
    public override void MenuStart_Event()
    {
        base.MenuStart_Event();
    }
}

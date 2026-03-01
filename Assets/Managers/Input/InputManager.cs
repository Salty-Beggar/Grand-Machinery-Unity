using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class InputManager : Manager
{
    private enum InputBind_ID {
        Jump, Movement, PrimaryAttack
    }
    public InputBind_Jump InputBind_Jump = new();
    public InputBind_Movement InputBind_Movement = new();
    public InputBind_PrimaryAttack InputBind_PrimaryAttack = new();

    private InputBind[] _inputBindArray;

    public InputManager() {
        _inputBindArray = new InputBind[]{
            InputBind_Jump,
            InputBind_Movement,
            InputBind_PrimaryAttack
        };
    }
    private void OnActionTriggered(InputAction.CallbackContext Context) {
        foreach (InputBind CurrentInputBind in _inputBindArray) {
            if (Context.action == CurrentInputBind.BindedAction) CurrentInputBind.OnActionTriggered(Context);
        }
    }

    public override void Start_Event()
    {
        Game.Input.onActionTriggered += OnActionTriggered;
        ReadOnlyArray<InputAction> CurrentActions = Game.Input.currentActionMap.actions;

        InputBind_Jump.BindedAction = CurrentActions.ElementAt((int)InputBind_ID.Jump);
        InputBind_Movement.BindedAction = CurrentActions.ElementAt((int)InputBind_ID.Movement);
        InputBind_PrimaryAttack.BindedAction = CurrentActions.ElementAt((int)InputBind_ID.PrimaryAttack);
    }

    public override void Update_Event()
    {
        foreach (InputBind CurrentInputBind in _inputBindArray) {
            CurrentInputBind.Update();
        }
    }

    public override void LateUpdate_Event()
    {
        foreach (InputBind CurrentInputBind in _inputBindArray) {
            CurrentInputBind.LateUpdate();
        }
    }
}

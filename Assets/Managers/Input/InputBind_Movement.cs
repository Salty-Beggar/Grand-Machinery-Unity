using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBind_Movement : ValuedInputBind<Vector2> {
    public bool IsActive = false;
    public InputBind_Movement() {
        //BindedAction = InputAction.
        //Value = Vector2.zero;
    }
    public override bool IsActivated() {
        return IsActive;
    }
    public override void OnActionTriggered(InputAction.CallbackContext Context)
    {
        if (Context.phase == InputActionPhase.Started) {
            IsActive = true;
            Value = Context.ReadValue<Vector2>();
        } else if (Context.phase == InputActionPhase.Canceled) IsActive = false;
    }
}
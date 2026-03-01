using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBind_PrimaryAttack : InputBind {
    public bool IsActive = false;
    public override void LateUpdate()
    {
        IsActive = false;
    }
    public override bool IsActivated() {
        return IsActive;
    }
    public override void OnActionTriggered(InputAction.CallbackContext Context) // OBSERVATION002 - Should this be a function, or should input binds have the IsActive variable as how its state is retrieved?
    {
        if (Context.phase == InputActionPhase.Started) IsActive = true;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

abstract public class InputBind {
    public InputAction BindedAction;
    public virtual void Update() {

    }
    public virtual void LateUpdate() {

    }

    public abstract bool IsActivated();
    public abstract void OnActionTriggered(InputAction.CallbackContext Context);
}
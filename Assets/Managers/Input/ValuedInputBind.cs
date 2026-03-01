using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

abstract public class ValuedInputBind<T> : InputBind {
    public T Value;
}
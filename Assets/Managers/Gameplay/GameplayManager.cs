

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameplayManager : Manager {
    public bool isActive = false;
    
    public EntityAssetSubmanager EntityAssetSubmanager = new();
    public InterfaceSubmanager InterfaceSubmanager = new();
    public AestheticsSubmanager AestheticsSubmanager = new();
    public BlueprintSubmanager BlueprintSubmanager = new();

    public Queue<Action> __DEBUG_actionQueue = new(); // OBSERVATION008: Formalize the action queue structure, and see if it should be managed by its own manager.

    public GameplayManager() {
        _subManagers = new Manager[]{
            EntityAssetSubmanager,
            InterfaceSubmanager,
            AestheticsSubmanager,
            BlueprintSubmanager
        };
    }

    public override void Update_Event()
    {
        while (__DEBUG_actionQueue.Count != 0) {
            Action curAction = __DEBUG_actionQueue.Dequeue();
            curAction();
        }
        base.Update_Event();
    }
}
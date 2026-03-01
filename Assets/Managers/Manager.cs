using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Manager
{
    protected Manager[] _subManagers = new Manager[0];
    
    public virtual void CleanUp_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.CleanUp_Event();
        }
    }
    public virtual void Update_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.Update_Event();
        }
    }
    public virtual void LateUpdate_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.LateUpdate_Event();
        }
    }
    public virtual void Start_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.Start_Event();
        }
    }
    public virtual void Awake_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.Awake_Event();
        }
    }

    #region Game
    public virtual void GameStart_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.GameStart_Event();
        }
    }
    public virtual void GameEnd_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.GameEnd_Event();
        }
    }
    #endregion
    
    #region Scene
    public virtual void SceneStart_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.SceneStart_Event();
        }
    }
    public virtual void SceneEnd_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.SceneEnd_Event();
        }
    }
    #endregion
    
    #region Room
    public virtual void RoomStart_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.RoomStart_Event();
        }
    }
    public virtual void RoomEnd_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.RoomEnd_Event();
        }
    }
    #endregion  
    
    #region Stage
    public virtual void StageStart_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.StageStart_Event();
        }
    }
    public virtual void StageEnd_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.StageEnd_Event();
        }
    }
    #endregion
    
    #region Menu
    public virtual void MenuStart_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.MenuStart_Event();
        }
    }
    public virtual void MenuEnd_Event() {
        foreach (Manager CurManager in _subManagers) {
            CurManager.MenuEnd_Event();
        }
    }
    #endregion

}
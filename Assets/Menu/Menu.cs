using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu {
    public Menu_ID ID;
    public string Name;
    public SceneIndex SceneIndex;
    public Manager Monomanager;
    public Menu(Menu_ID ID, string Name, SceneIndex SceneIndex, Manager Monomanager) {
        this.ID = ID;
        this.Name = Name;
        this.SceneIndex = SceneIndex;
        this.Monomanager = Monomanager;
    }
}
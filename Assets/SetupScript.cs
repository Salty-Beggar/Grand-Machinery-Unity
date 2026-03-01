using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SetupScript : MonoBehaviour
{
    public GameObject GameSupermanagerObj;
    void Start()
    {
        Instantiate(GameSupermanagerObj);
        Game.TransitionManager.GoToMenu(Menu_ID.MainMenu);
    }
}

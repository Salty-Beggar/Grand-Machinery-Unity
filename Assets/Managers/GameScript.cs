using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameScript : MonoBehaviour
{
    public static GameSupermanager GameSupermanager;
    public GameObject penis;
    void Awake()
    {
        Debug.Log("FUCKKKKKKKKKKKKK");
        Game.GameSupermanager = new GameSupermanager(gameObject);
        Game.GameSupermanager.Awake_Event();
        penis.SetActive(true);
    }
    void Start()
    {
        Game.GameSupermanager.Start_Event();
    }

    // Update is called once per frame
    void Update()
    {
        Game.GameSupermanager.Update_Event();
    }
    void LateUpdate()
    {
        Game.GameSupermanager.LateUpdate_Event();
    }
}

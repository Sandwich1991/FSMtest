using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager s_instance;
    private InputManager _input = new InputManager();
    
    public static Manager Instance { get { init(); return s_instance; } }
    public static InputManager Input = Instance._input;

    static void init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                s_instance = go.AddComponent<Manager>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Manager>();
        }
    }

    private void Start()
    {
        init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }
}

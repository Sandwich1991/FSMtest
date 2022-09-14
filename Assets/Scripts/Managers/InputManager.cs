using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager
{
    public Action<Define.MouseEvent> MouseAction;
    public Action<Define.KeyEvent> KeyAction = null;

    public void OnUpdate()
    {
        if (KeyAction != null && Input.anyKey)
        {
            KeyAction.Invoke(Define.KeyEvent.Press);
        }
        else
        {
            KeyAction.Invoke(Define.KeyEvent.None);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressdTime = 0;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (Input.GetMouseButton(0))
        {
            if (!_pressed)
            {
                MouseAction.Invoke(Define.MouseEvent.PointerDown);
                _pressdTime = Time.time;
            }
            MouseAction.Invoke(Define.MouseEvent.Press);
            _pressed = true;
        }
        else
        {
            if (_pressed)
            {
                if (Time.time < _pressdTime + 1.0f)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                MouseAction.Invoke(Define.MouseEvent.PointerUp);

            }
            _pressed = false;
            _pressdTime = 0;
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}

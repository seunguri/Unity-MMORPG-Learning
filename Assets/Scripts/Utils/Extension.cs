using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public static class Extension
{
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvnet type = Define.UIEvnet.Cilck)
    {
        UI_Base.AddUIEvent(go, action, type);
    }
}
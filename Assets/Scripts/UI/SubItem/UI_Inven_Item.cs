using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Inven_Item : UI_Base
{

    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TextMeshProUGUI>().text = _name;
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => Debug.Log($"Item Clicked: {_name}"));
    }

    public void SetInfo(string name) { _name = name; }
}

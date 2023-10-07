using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    void Start()
    {
        Init();   
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // TODO: 실제 인벤토리 정보 참고
        for (int i = 0; i < 8; i++)
        {
            UI_Inven_Item item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform);
            item.SetInfo($"Item{i}");
        }
    }
}

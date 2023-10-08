using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{ 
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        //Managers.UI.ShowSceneUI<UI_Inven>();
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Frog");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        Managers.Game.Spawn(Define.WorldObject.Monster, "Snake");
    }

    public override void Clear()
    {

    }
}

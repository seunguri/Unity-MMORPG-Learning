using UnityEngine;
using System.Collections;

public class Define
{
    public enum State
    {
        Die,
        Idle,
        Moving,
        Skill,
    }

    public enum Layer
    {
        Ground = 6,
        Monster = 7,
        Block = 8,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvnet
    {
        Cilck,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum CameraMode
    {
        QuaterView,
    }
}

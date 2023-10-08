using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    Texture2D _swardIcon;
    Texture2D _arrowIcon;

    enum CursorType
    {
        None,
        Sward,
        Arrow,
    }

    CursorType _cursorType = CursorType.None;


    void Start()
    {
        _swardIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Sward");
        _arrowIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Green");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Sward)
                {
                    Cursor.SetCursor(_swardIcon, Vector2.zero, CursorMode.Auto);
                    _cursorType = CursorType.Sward;
                }
            }
            else
            {
                if (_cursorType != CursorType.Arrow)
                {
                    Cursor.SetCursor(_arrowIcon, new Vector2(_arrowIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Arrow;
                }
            }
        }
    }
}

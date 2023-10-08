using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;
    Vector3 _destPos;

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

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    public enum PlayerState
    {
        Die,
        Idle,
        Moving,
        Skill,
    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {

    }

    void UpdateIdle()
    {
        // animation
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("speed", 0);
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            // nma.CalculatePath
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // animation
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("speed", _stat.MoveSpeed);
    }

    void Update()
    {
        UpdateMouseCursor();

        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;

        }
    }

    void UpdateMouseCursor()
    {
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

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Debug.Log("Monster Click!");
            }
            else
            {
                Debug.Log("Ground Click!");
            }
        }
    }
}

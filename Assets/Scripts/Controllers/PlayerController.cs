using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die,
        Idle,
        Moving,
        Skill,
    }

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;
    Vector3 _destPos;

    [SerializeField] PlayerState _state = PlayerState.Idle;

    GameObject _lockTarget;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator animator = GetComponent<Animator>();
            switch (value)
            {
                case PlayerState.Die:
                    break;
                case PlayerState.Idle:
                    animator.CrossFade("WAIT", 0.1f);
                    break;
                case PlayerState.Moving:
                    animator.CrossFade("RUN", 0.1f);
                    break;
                case PlayerState.Skill:
                    animator.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
            }
        }
    }

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    void UpdateDie()
    {

    }

    void UpdateIdle()
    {
        
    }

    void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        float distance = dir.magnitude;

        // monster attack within range
        if (_lockTarget != null)
        {
            if (distance <= 1.5f)
            {
                State = PlayerState.Skill;
                return;
            }
        }

        // move
        if (distance < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, distance);
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    State = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");

        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            Stat myStat = gameObject.GetComponent<PlayerStat>();
            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            targetStat.Hp -= damage;
        }

        if (_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }
    }

    void Update()
    {
        switch (State)
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
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
    }

    bool _stopSkill = false;

    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        State = PlayerState.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            _lockTarget = hit.collider.gameObject;
                            _destPos = _lockTarget.transform.position;
                        }

                        else
                        {
                            _lockTarget = null;
                            _destPos = hit.point;
                        }
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }
}

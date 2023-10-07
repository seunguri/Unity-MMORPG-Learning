using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;
    Vector3 _destPos;

    public enum PlayerState
    {
        DIE,
        IDLE,
        MOVING
    }

    PlayerState _state = PlayerState.IDLE;


    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        //TEMP
        Managers.UI.ShowSceneUI<UI_Inven>();
    }

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
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.IDLE;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // animation
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("speed", _speed);
    }

    void Update()
    {
        switch(_state)
        {
            case PlayerState.DIE:
                UpdateDie();
                break;
            case PlayerState.IDLE:
                UpdateIdle();
                break;
            case PlayerState.MOVING:
                UpdateMoving();
                break;

        }
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.DIE)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.MOVING;
        }
    }
}

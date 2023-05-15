using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputBool
{
    bool value;
    public Vector2 direction;
    public float time = 0;
    public bool Value
    {
        get
        {
            return Input.GetKey(key);
        }
    }
    public KeyCode key;
    public void Update(float _deltaTime)
    {
        time += _deltaTime;
    }
    public InputBool(KeyCode _key, Vector2 _direction)
    {
        key = _key;
        direction = _direction;
    }
}
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    [SerializeField] Vector3 nextPosition = Vector3.zero;
    [SerializeField] Animator animator;
    [SerializeField] Vector2 directionEye;
    [SerializeField] bool isSprinting = false;
    float axisX;
    float axisY;
    [SerializeField] bool canMove = true;
    [SerializeField] float ratioSpeedSprint = 1.5f;
    [SerializeField] float inputSensibility = 0.1f;
    [SerializeField] List<InputBool> bools = new List<InputBool>();
    void SetEyeDirectionX(int _eyeDirectionX)
    {
        if (_eyeDirectionX == 0) return;
        directionEye.x = _eyeDirectionX;
        animator.SetFloat("directionEyeX", _eyeDirectionX);
        directionEye.y = 0;
        animator.SetFloat("directionEyeY", 0);
    }
    void SetEyeDirectionY(int _eyeDirectionY)
    {
        if (_eyeDirectionY == 0) return;
        directionEye.y = _eyeDirectionY;
        animator.SetFloat("directionEyeY", _eyeDirectionY);
        directionEye.x = 0;
        animator.SetFloat("directionEyeX", 0);
    }
    private void Start()
    {
        nextPosition = transform.position;
        directionEye = new Vector2(0, -1);
    }
    void InputKey(KeyCode _key, Vector2 _direction)
    {
        SetEyeDirectionX((int)_direction.x);
        SetEyeDirectionY((int)_direction.y);
        bools.Insert(0, new InputBool(_key, _direction));
        if (bools.Count > 4)
            bools.RemoveAt(4);
    }
    void Idle()
    {
        animator.SetFloat("axisX", 0);
        animator.SetFloat("axisY", 0);
        animator.SetBool("isWalking", false);
    }
    void Update()
    {
        Sprint();

        if (Input.GetKeyDown(KeyCode.W))
        {
            InputKey(KeyCode.W, new Vector2(0, 1));
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            InputKey(KeyCode.S, new Vector2(0, -1));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            InputKey(KeyCode.D, new Vector2(1, 0));
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            InputKey(KeyCode.A, new Vector2(-1, 0));
        }
        //if (!canMove) return;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(nextPosition, transform.position) > 0f) return;
        for (int i = 0; i < bools.Count; i++)
        {
            bools[i].Update(Time.deltaTime);
        }
        int x = 0;
        if (bools.Count == 0)
            Idle();
        while (bools.Count > 0 && x < 4)
        {
            x++;
            InputBool _input = bools[0];
            if (!_input.Value)
            {
                bools.RemoveAt(0);
                continue;
            }
            if (_input.time <= inputSensibility && bools.Count == 1)
            {
                Idle();
                break;
            }
            animator.SetFloat("axisX", _input.direction.x);
            animator.SetFloat("axisY", _input.direction.y);
            animator.SetBool("isWalking", true);
            Vector3 _eyeDirection = _input.direction;
            SetEyeDirectionX((int)_input.direction.x);
            SetEyeDirectionY((int)_input.direction.y);
            Vector3 _nextPosition = transform.position + _eyeDirection;
            RaycastHit2D raycastHit2D = Physics2D.CircleCast(_nextPosition, 0.1f, Vector3.forward);
            if(!raycastHit2D || raycastHit2D.collider.isTrigger)
            {
                nextPosition = _nextPosition;
            }
        }
    }
    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            animator.SetBool("isSprinting", isSprinting);
            moveSpeed *= ratioSpeedSprint;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            animator.SetBool("isSprinting", isSprinting);
            moveSpeed /= ratioSpeedSprint;
        }
    }
    void ActiveMovement()
    {
        canMove = true;
    }
    private void OnDrawGizmos()
    {
        Vector3 _d = directionEye;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _d);
    }
}

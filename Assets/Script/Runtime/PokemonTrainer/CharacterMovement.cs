using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InputBool
{
    public Vector2 direction;
    public float time = 0;
    public KeyCode key;
    public bool IsPressed => Input.GetKey(key);
    public void IncrementTimer(float _deltaTime)
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
    [SerializeField] bool canMove = true;
    [SerializeField] float ratioSpeedSprint = 1.5f;
    [SerializeField] float inputSensibility = 0.1f;
    List<InputBool> inputs = new List<InputBool>();
    public Vector2 DirectionEye
    {
        get => directionEye;
        set => directionEye = value;
    }
    public Vector3 NextPosition
    {
        get => nextPosition;
        set => nextPosition = value;
    }
    private void Start()
    {
        nextPosition = transform.position;
        directionEye = new Vector2(0, -1);
    }
    void Update()
    {
        if (!canMove) return;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition,(isSprinting ? ratioSpeedSprint : 1) * moveSpeed * Time.deltaTime);
        if (IsMovingToNextPosition()) return;
        
        for (int i = 0; i < inputs.Count; i++)
            inputs[i].IncrementTimer(Time.deltaTime);

        int maxIteration = 0;
        bool _inputUse = false;
        while (inputs.Count > 0 && maxIteration < 4)
        {
            maxIteration++;
            InputBool _input = inputs[0];
            if (!_input.IsPressed)
            {
                inputs.RemoveAt(0);
                continue;
            }
            if (IsKeyJustPressed(_input)) break;
            Vector3 _nextPosition = transform.position + (Vector3)_input.direction;
            RaycastHit2D raycastHit2D = Physics2D.CircleCast(_nextPosition, 0.1f, Vector3.forward);
            SetEyeDirectionX((int)_input.direction.x);
            SetEyeDirectionY((int)_input.direction.y);
            if (raycastHit2D && !raycastHit2D.collider.isTrigger) break;
            animator.SetFloat("axisX", _input.direction.x);
            animator.SetFloat("axisY", _input.direction.y);
            animator.SetBool("isWalking", true);
            nextPosition = _nextPosition;
            _inputUse = true;
        }
        if (!_inputUse)
            Idle();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)directionEye);
    }

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

    public void RegisterInputKeyDown(KeyCode _key, Vector2 _direction)
    {
        if (!Input.GetKeyDown(_key)) return;
        SetEyeDirectionX((int)_direction.x);
        SetEyeDirectionY((int)_direction.y);
        inputs.Insert(0, new InputBool(_key, _direction));
        if (inputs.Count > 4)
            inputs.RemoveAt(4);
    }
    public void ClearInput() => inputs.Clear();
    void Idle()
    {
        animator.SetFloat("axisX", 0);
        animator.SetFloat("axisY", 0);
        animator.SetBool("isWalking", false);
    }
    public void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            animator.SetBool("isSprinting", isSprinting);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            animator.SetBool("isSprinting", isSprinting);
        }
    }
    public void StopSprint()
    {
        isSprinting = false;
    }
    bool IsKeyJustPressed(InputBool _input) 
    {
        return _input.time <= inputSensibility && inputs.Count == 1;
    }
    public bool IsMovingToNextPosition()
    {
        return Vector3.Distance(transform.position, nextPosition) > 0.0f;
    }

    public void ActiveMovement() => canMove = true;
    public void DeactiveMovement() => canMove = false;
}

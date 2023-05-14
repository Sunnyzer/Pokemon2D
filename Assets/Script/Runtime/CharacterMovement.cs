using UnityEngine;

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
    private void Start()
    {
        nextPosition = transform.position;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            moveSpeed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = true;
            moveSpeed /= 2;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            directionEye.x = 1;
            animator.SetFloat("directionEyeX", (int)directionEye.x);
            directionEye.y = 0;
            animator.SetFloat("directionEyeY", 0);
            animator.GetComponent<SpriteRenderer>().flipX = false;
            canMove = false;
            CancelInvoke();
            Invoke(nameof(ActiveMovement), 0.1f);
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            directionEye.y = 1;
            animator.SetFloat("directionEyeY", (int)directionEye.y);
            directionEye.x = 0;
            animator.SetFloat("directionEyeX", 0);
            animator.GetComponent<SpriteRenderer>().flipX = false;
            canMove = false;
            CancelInvoke();
            Invoke(nameof(ActiveMovement), 0.1f);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            directionEye.y = -1;
            animator.SetFloat("directionEyeY", (int)directionEye.y);
            directionEye.x = 0;
            animator.SetFloat("directionEyeX", 0);
            animator.GetComponent<SpriteRenderer>().flipX = false;
            canMove = false;
            CancelInvoke();
            Invoke(nameof(ActiveMovement), 0.1f);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            directionEye.x = -1;
            animator.SetFloat("directionEyeX", (int)directionEye.x);
            directionEye.y = 0;
            animator.SetFloat("directionEyeY", 0);
            animator.GetComponent<SpriteRenderer>().flipX = true;
            canMove = false;
            CancelInvoke();
            Invoke(nameof(ActiveMovement), 0.1f);
            return;
        }
        if (!canMove) return;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(nextPosition, transform.position) > 0f) return;

        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");
        if (axisX != 0)
        {
            directionEye.x = axisX;
            animator.SetFloat("directionEyeX", (int)directionEye.x);
            directionEye.y = 0;
            animator.SetFloat("directionEyeY", 0);
        }
        if (axisY != 0)
        {
            directionEye.y = axisY;
            animator.SetFloat("directionEyeY", (int)directionEye.y);
            directionEye.x = 0;
            animator.SetFloat("directionEyeX", 0);
        }
        
        animator.SetFloat("axisX", Mathf.Abs(axisX));
        animator.SetFloat("axisY", axisY);

        animator.GetComponent<SpriteRenderer>().flipX = axisX == -1;
        Vector3 _direction = new Vector3(axisX, axisY, 0);

        if (_direction == Vector3.zero) return;
        Vector3 _nextPosition = transform.position + _direction;
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(_nextPosition, 0.1f, Vector3.forward);
        if(!raycastHit2D || raycastHit2D.collider.isTrigger)
        {
            nextPosition = _nextPosition;
        }
    }
    void ActiveMovement()
    {
        canMove = true;
    }
}

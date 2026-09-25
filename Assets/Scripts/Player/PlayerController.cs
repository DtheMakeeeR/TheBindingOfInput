using System;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D playerCollider;
    [Header("Передвижения")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float runSpeed = 5f;

    [Header("Гравитация")]
    [SerializeField] float raycastLength = 0.05f;
    [SerializeField] float gravityPower = 10f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] LayerMask groundedMask;

    bool isGrounded = false;
    bool isRunning = false;
    bool isJumping = false;
    bool isCrouching = false;
    bool canMove = true;
    Vector2 moveInput = Vector2.zero;
    Vector2 moveDirection = Vector2.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsGrounded();
        if (!canMove) 
        {
            rb.linearVelocity = rb.linearVelocity.With(x: 0);
            return;
        }
        if(isJumping)
        {
            Jump();
        }
        if(isCrouching)
        {
            Crouch();
        }
        //ApplyGravity();
        Move();
    }

    private void Crouch()
    {
        isCrouching = false;
        Debug.Log("PLAYER CROUCH");
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            transform.position,
            Vector2.down,
            raycastLength,
            groundedMask
        );
        Collider2D collider = hits.Where(hit => hit.collider.isTrigger == false).Select(hit => hit.collider).FirstOrDefault();
        if(collider != null)
        {
            Debug.Log($"hit in crouch: {collider.name} {collider.isTrigger}");
            Platform platform = collider.GetComponent<Platform>();
            if (platform != null)
            {
                Physics2D.IgnoreCollision(collider, playerCollider, true);
            }
        }        
    }

    public void Jump()
    {
        isJumping = false;
        Debug.Log($"JUMP: {!isGrounded}");
        if (!isGrounded) return;
        //moveDirection = moveDirection.Add(y: jumpPower);
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void ApplyGravity()
    {
        if(isGrounded)
        {
            moveDirection = moveDirection.Add(y: -.1f * Time.deltaTime);
        }
        else
        {
            moveDirection = moveDirection.Add(y: -gravityPower * Time.deltaTime);
        }
    }

    private void Move()
    {
        float speed = isRunning ? runSpeed : walkSpeed;
        
        Vector2 velocity = rb.linearVelocity.With(x: moveInput.x * speed);
        rb.linearVelocity = velocity;
    }

    private void UpdateIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            raycastLength,
            groundedMask
        );

        isGrounded = hit.collider != null;
    }

    public void SetMoveInput(Vector2 newInput)
    {
        moveInput = newInput;
    }
    public void SetIsRunning(bool flag)
    {
        isRunning = flag;
    }
    public void SetIsJumping(bool flag) => isJumping = flag;
    public void SetCanMove(bool flag) => canMove = flag;

    internal void Respawn()
    {
        transform.position = CheckpointsManager.Instance.RespawnTransform.position;
    }

    internal void SetIsCrouching(bool flag) => isCrouching = flag;
}

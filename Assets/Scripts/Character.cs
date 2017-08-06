using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private LayerMask ground;
    [SerializeField][Range(1, 10)]
    protected float gravityModifier = 3f;

    protected Vector3 movement;
    protected int speed = 10;
    protected bool isGrounded = false;
    protected float radius = 0.5f;
    protected bool facingRight = true;
    protected Animator animator;
    protected Rigidbody2D rb2D;

    protected virtual void Start ()
    {
        movement = Vector3.zero;
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        if(movement.y < 0)
            CheckGround();

        if (movement.magnitude > .1f)
        {
            CheckDirection();
            rb2D.MovePosition(transform.position + movement * speed * Time.deltaTime);
        }
    }

    protected void Move(float velocity)
    {
        animator.SetFloat("speed", Mathf.Abs(velocity));
        movement.x = velocity;

        if (!isGrounded)
            movement.y -= Time.deltaTime * gravityModifier;
        else
            movement.y = 0;
    }

    protected void CheckDirection()
    {
        if (movement.x > 0 && !facingRight)
            Flip();
        if (movement.x < 0 && facingRight)
            Flip();
    }

    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    protected void CheckGround()
    {
        Vector2 point = new Vector2(transform.position.x, transform.position.y - radius - 0.12f);
        isGrounded = Physics2D.OverlapCircle(point, .1f, ground);
        if(isGrounded)
            animator.SetBool("jump", false);
    }

    protected void Jump(float force)
    {
        if (isGrounded)
        {
            animator.SetBool("jump", true);
            movement.y = force;
            isGrounded = false;
        }
    }
}

using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private LayerMask ground;

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
            //transform.Translate(movement * speed * Time.deltaTime);
            rb2D.MovePosition(transform.position + movement * speed * Time.deltaTime);
        }
    }

    protected void Move(float velocity)
    {
        movement.x = velocity;

        if (!isGrounded)
            movement.y -= Time.deltaTime * 2;
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
    }

    protected void Jump(float force)
    {
        Debug.Log(isGrounded);
        if (isGrounded)
        {
            Debug.Log("Jump!");
            movement.y = force;
            isGrounded = false;
        }
    }
}

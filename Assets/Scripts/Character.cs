using UnityEngine;

public class Character : MonoBehaviour
{
    protected Vector3 movement;
    protected int speed = 10;
    protected bool isGrounded = false;
    protected float radius = 0.5f;
    protected bool facingRight = true;
    protected Animator animator;

    protected virtual void Start ()
    {
        movement = Vector3.zero;
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        if(movement.magnitude > .1f)
        {
            CheckGround();
            CheckDirection();
            transform.Translate(movement * speed * Time.deltaTime);
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
        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            isGrounded = distance < radius ? true : false;
            transform.position = new Vector3(transform.position.x, hit.point.y + radius, transform.position.z);
        }
        */

        Vector2 point = new Vector2(transform.position.x, transform.position.y - radius - .12f);
        isGrounded = Physics2D.OverlapCircle(point, .1f);
    }

    protected void Jump(float force)
    {
        if (isGrounded)
        {
            movement.y = force;
            transform.position = transform.position + Vector3.up;
        }
    }
}

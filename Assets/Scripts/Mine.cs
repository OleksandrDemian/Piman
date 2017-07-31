using System;
using UnityEngine;

public class Mine : MonoBehaviour, IPoolable, IDamagable
{
    private bool isGrounded;
    private float timer;

    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    private void Start()
    {
        isGrounded = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 14)
        {
            Explode();
        }
    }

    private void Explode()
    {
        ExplosionManager exp = ObjectPool.Get<ExplosionManager>();
        exp.Initialize(transform.position, 4);
        Disable();
    }

    private void FixedUpdate ()
    {
        if (!isGrounded)
        {
            transform.Translate(-Vector2.up * Time.deltaTime * 10);
            CheckGround();
        }
	}

    public void Initialize(Vector3 position)
    {
        isGrounded = false;
        timer = 0f;
        transform.position = position;
    }

    protected void CheckGround()
    {
        Vector2 point = new Vector2(transform.position.x, transform.position.y - 1f - .12f);
        isGrounded = Physics2D.OverlapCircle(point, .1f, LayerMask.GetMask("Ground"));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player")
            return;

        IDamagable target = col.GetComponent<IDamagable>();
        if (target != null)
        {
            target.Damage(6);
        }

        Explode();
    }

    private void Disable()
    {
        ObjectPool.Add(this);
    }

    public void Damage(int amount)
    {
        Explode();
    }
}

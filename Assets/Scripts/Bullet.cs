using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    private Vector3 direction = Vector3.zero;
    private Rigidbody2D rb2D;
    private int speedMult = 35;
    private float timer = 2f;
    private bool isPlayer = false;
    private Damage damage;

    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    private void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
	}

    public void Initialize(Vector3 startPosition, Vector2 direction)
    {
        this.direction = direction;
        transform.up = direction.normalized;
        transform.position = startPosition;
        timer = 2f;
    }

    public void SetDamage(Damage damage)
    {
        this.damage = damage;
    }

    public void IsPlayer()
    {
        isPlayer = true;
    }
	
	private void Update ()
    {
        rb2D.MovePosition(transform.position + direction * Time.deltaTime * speedMult);

        timer -= Time.deltaTime;
        if (timer < 0)
            Disable();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && isPlayer)
            return;
        if (col.tag == "Enemy" && !isPlayer)
            return;

        ExplosionManager exp = ObjectPool.Get<ExplosionManager>();
        exp.Initialize(transform.position, 4);

        if (damage == null)
            return;

        IDamagable target = col.GetComponent<IDamagable>();
        if (target != null)
        {
            target.Damage(damage);
        }

        Disable();
    }

    private void Disable()
    {
        damage = null;
        isPlayer = false;
        ObjectPool.Add(this);
    }
}

using UnityEngine;

public delegate void PimanEvent();

public class Piman : Character, IDamagable, IDamageListener
{
    private bool isShooting = false;
    private float shootingRange = 0f;
    private FireHotspot fireHotspot;
    private PIDamage damage;
    private Attribute health;

    public PimanEvent onDead;

    [SerializeField]
    private KeyCode fireKey;
    [SerializeField]
    private KeyCode jumpKey;

    public static Piman Instance
    {
        get;
        private set;
    }

    public void Damage(int amount)
    {
        if (health.Value < 1)
            return;
        //Jump(2);
        //GameManager.Instance.GameOver();
        health.Value--;
        if (health.Value < 1)
        {
            if (onDead != null)
                onDead();
            gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start ()
    {
        base.Start();

        health = new Attribute(3);

        damage = new PIDamage();
        fireHotspot = transform.FindChild("FireHotspot").GetComponent<FireHotspot>();
        fireHotspot.Enable(false);
	}
	
	protected override void Update ()
    {
        base.Update();

        float input = isShooting ? 0 : Input.GetAxis("Horizontal");
        //animator.SetFloat("speed", Mathf.Abs(input));
        Move(input);

        
        if (Input.GetKeyDown(jumpKey))
            Jump(1);
            
        ManageShooting();
    }

    private void ManageShooting()
    {
        if (isShooting)
        {
            shootingRange -= Time.deltaTime * 2;
            fireHotspot.SetSight(shootingRange);
            if (shootingRange < 0)
                Shoot(Vector3.up);
        }

        if (Input.GetKeyDown(fireKey))
        {
            shootingRange = 1f;
            isShooting = true;
            fireHotspot.SetSight(shootingRange);
            fireHotspot.Enable(true);
        }

        if (Input.GetKeyUp(fireKey))
        {
            if (!isShooting)
                return;

            Vector2 direction = new Vector2(Random.Range(-shootingRange, shootingRange), 1);
            Shoot(direction);
        }
    }

    private void Shoot(Vector2 direction)
    {
        isShooting = false;
        fireHotspot.Enable(false);

        Bullet bullet = ObjectPool.Get<Bullet>();
        bullet.IsPlayer();
        bullet.SetDamage(damage.GetNext());
        bullet.Initialize(fireHotspot.transform.position, direction);
    }

    public void OnDamageResult(DamageResult result)
    {
        
    }
}

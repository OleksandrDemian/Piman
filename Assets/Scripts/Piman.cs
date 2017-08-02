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
    [SerializeField]
    private ElementsBar healthBar;

    public static Piman Instance
    {
        get;
        private set;
    }

    public void Damage(Damage damage)
    {
        if (health.Value < 1)
            return;

        health.Value--;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnHealthValueChange(int value, int oldValue)
    {
        healthBar.Set(value);
        if (value < 1)
        {
            if (onDead != null)
                onDead();
            gameObject.SetActive(false);
        }
    }

    protected override void Start ()
    {
        base.Start();

        health = new Attribute(3);
        health.onValueChange = OnHealthValueChange;

        damage = new PIDamage();
        fireHotspot = transform.FindChild("FireHotspot").GetComponent<FireHotspot>();
        fireHotspot.Enable(false);
	}
	
	protected override void Update ()
    {
        float input = isShooting ? 0 : Input.GetAxis("Horizontal");
        Move(input);

        if (Input.GetKeyDown(jumpKey))
            Jump(1.5f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PoweUp powerUp = ObjectPool.Get<PoweUp>();
            powerUp.Initialize(Random.Range(0f, 1f) < .5f ? true : false);
            powerUp.SetEffect(new ResetHealthPowerUp());
        }
        ManageShooting();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (transform.position.y < -13f)
            health.Value = 0;
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
        bullet.SetDamage(new Damage(gameObject, damage.GetNext()));
        bullet.Initialize(fireHotspot.transform.position, direction);
    }

    public void OnDamageResult(DamageResult result)
    {

    }

    public Attribute GetHealth()
    {
        return health;
    }
}

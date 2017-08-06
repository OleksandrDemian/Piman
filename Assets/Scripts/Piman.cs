using UnityEngine;

public delegate void PimanEvent();

public class Piman : Character, IDamagable, IDamageListener
{
    private bool isShooting = false;
    private float shootingRange = 0f;
    private FireHotspot fireHotspot;
    private PIDamage damage;
    private Attribute health;

    public LayerMask enemies;

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
            //animator.SetBool("death", true);
            //enabled = false;
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
            Jump(2f);

        if (Input.GetKeyDown(KeyCode.P))
        {
            MinesUFO ufo = ObjectPool.Get<MinesUFO>();
            ufo.Initialize(GameManager.Instance.GetRandomPosition(true));
        }

        ManageShooting();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //Debug.Log(FindNearestEnemy(transform.position, 15));
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
    
    /*
    private GameObject FindNearestEnemy(Vector3 position, float radius)
    {
        Collider2D[] colliders = new Collider2D[10];
        Physics2D.OverlapCircleNonAlloc(position, radius, colliders, enemies);
        
        float distance = Mathf.Infinity;
        GameObject nearest = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == null)
                break;

            float curDistance = Vector2.Distance(position, colliders[i].transform.position);

            Debug.Log("Distance: " + curDistance + "; need: " + distance + "; obj: " + colliders[i].name);
            if (curDistance >= distance)
                continue;

            if (colliders[i].gameObject != gameObject)
            {
                distance = curDistance;
                nearest = colliders[i].gameObject;
            }
        }
        return nearest;
    }
    */
}
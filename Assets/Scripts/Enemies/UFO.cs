using System.Collections;
using UnityEngine;

public class UFO : MonoBehaviour, IDamagable, IPoolable
{
    protected Attribute health;
    private float shootingDelay = 3f;
    private float nextShoot = 0f;
    private bool canShoot = true;
    //private Shadow shadow;

    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public void Damage(int amount)
    {
        PopUp.ShowText(transform.position, amount.ToString());
        health.Value -= amount;
        health.onValueChange = OnHealthValueChange;
    }

    public void GoOut()
    {
        canShoot = false;
        StartCoroutine(Arrive(15f));
    }

    public IEnumerator Arrive(float targetY)
    {
        Vector3 target = new Vector3(transform.position.x, targetY, 0);

        while (transform.position.y != targetY)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 10);
            yield return new WaitForEndOfFrame();
        }
    }

    protected virtual void Start ()
    {
        
	}
	
	protected virtual void Update ()
    {
        if (Time.time > nextShoot)
            Shoot();
	}

    private void Shoot()
    {
        if (!canShoot)
            return;

        Vector3 targetPos = Piman.Instance.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        Bullet bullet = ObjectPool.Get<Bullet>();
        bullet.Initialize(transform.position - transform.up, direction);
        bullet.SetDamage(1);
        nextShoot = Time.time + shootingDelay;
    }

    public virtual void Initialize(Vector3 position)
    {
        transform.position = new Vector3(position.x, 15, 0);
        health = new Attribute(10);
        health.onValueChange = OnHealthValueChange;

        //shadow = ObjectPool.Get<Shadow>();
        //shadow.SetX(transform.position.x);
        StartCoroutine(Arrive(position.y));
        nextShoot = Time.time + shootingDelay;
        WaveManager.Instance.AddUFO(this);
    }

    protected void OnHealthValueChange(int value, int old)
    {
        if (value < 1)
            Disable();
    }

    private void Disable()
    {
        ExplosionManager exp = ObjectPool.Get<ExplosionManager>();
        //shadow.Disable();
        exp.Initialize(transform.position, 10);

        WaveManager.Instance.RemoveActiveUFO(this);
        ObjectPool.Add(this);
    }
}

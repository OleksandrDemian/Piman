using UnityEngine;

public class PoweUp : MonoBehaviour, IPoolable, IDamagable
{
    private Vector2 direction = Vector2.zero;
    private int speed = 5;
    private int border = 10;
    private PowerUpEffect effect;

    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public void Damage(Damage damage)
    {
        if (effect != null)
            effect.Trigger(damage.GetParent());

        Disable();
    }

    public void SetEffect(PowerUpEffect effect)
    {
        this.effect = effect;
    }

    private void Disable()
    {
        ObjectPool.Add(this);
    }

    public void Initialize(bool left)
    {
        direction = new Vector2(left ? 1 : -1, 0);
        border = (int)GameManager.Instance.MapBounds.x;
        Vector2 pos = GameManager.Instance.GetRandomPosition(true);

        pos.Set(left ? -border : border, pos.y);
        transform.position = pos;
    }

    private void Start ()
    {
		
	}
	
	private void Update ()
    {
        direction.y = Mathf.Sin(transform.position.x);
        transform.Translate(direction * Time.deltaTime * speed);

        if (border < Mathf.Abs(transform.position.x))
            Disable();
	}
}

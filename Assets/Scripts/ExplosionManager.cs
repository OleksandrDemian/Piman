using System.Collections;
using UnityEngine;

public class ExplosionManager : MonoBehaviour, IPoolable
{
    private ExplosionParticle[] particles;
    private float radius = 8f;

    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;

        if(particles == null)
            particles = GetComponentsInChildren<ExplosionParticle>();

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Initialize(new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0));
        }
    }

    public void Initialize(Vector3 position, float radius)
    {
        this.radius = radius;
        Initialize(position);
        StartCoroutine(DisableWait());
    }
    
    private IEnumerator DisableWait()
    {
        float timer = 0;
        while (timer < 0.7f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Disable();
    }

    private void Disable()
    {
        radius = 8f;
        ObjectPool.Add(this);
    }
}

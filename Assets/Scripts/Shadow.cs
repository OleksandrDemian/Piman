using UnityEngine;

public class Shadow : MonoBehaviour, IPoolable
{
    public GameObject GetGameObject
    {
        get
        {
            return gameObject;
        }
    }

    public void SetX(float x)
    {
        transform.position = new Vector3(x, -9.5f, 0);
    }

    public void SetScale(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }

    public void Disable()
    {
        SetScale(2);
        ObjectPool.Add(this);
    }
}

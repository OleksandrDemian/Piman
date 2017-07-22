using System.Collections.Generic;
using UnityEngine;

public interface IPoolable {
    GameObject GetGameObject { get; }
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefs;

    private List<IPoolable> poolable;

    public static ObjectPool Instance
    {
        get;
        private set;
    }

    private void Awake() {
        poolable = new List<IPoolable>();
        Instance = this;
    }

    public static void Add(IPoolable obj)
    {
        obj.GetGameObject.SetActive(false);
        Instance.poolable.Add(obj);
    }

    public static T Get<T>()
    {
        //Debug.Log("Request: " + typeof(T));
        for (int i = 0; i < Instance.poolable.Count; i++)
        {
            if(Instance.poolable[i] is T)
            {
                IPoolable go = Instance.poolable[i];
                go.GetGameObject.SetActive(true);
                Instance.poolable.Remove(go);
                return (T)go;
            }
        }

        for (int i = 0; i < Instance.prefs.Length; i++)
        {
            if (Instance.prefs[i].GetComponent<T>() != null)
            {
                GameObject instance = Instantiate(Instance.prefs[i]);
                //Debug.Log("Created!");
                return instance.GetComponent<T>();
            }
        }
        throw new System.Exception("There is no " + typeof(T));
    }
    
    /*
    public GameObject Get(EntityType type)
    {
        for (int i = 0; i < poolable.Count; i++)
        {
            if (poolable[i].Type == type)
            {
                GameObject go = poolable[i].Get;
                go.SetActive(true);
                poolable.Remove(poolable[i]);
                return go;
            }
        }

        switch (type) {
            case EntityType.ASTEROID:
                return Instantiate(asteroidPrephab);
            case EntityType.SHIP:
                return Instantiate(shipPrephab);
            case EntityType.LASER:
                return Instantiate(laserPrephab);
            case EntityType.DAMAGEPOPUP:
                return Instantiate(damagePopUpPrephab);
        }
        return null;
    }
    */
}
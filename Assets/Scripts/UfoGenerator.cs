using System.Collections.Generic;
using UnityEngine;

public class UfoGenerator : MonoBehaviour
{
    [SerializeField][Range(1, 10)]
    private int generateDelay = 10;
    private float nextGenerate = 0;
    private List<UFO> activeUFO;
    private bool hasToGenerate = true;

    public static UfoGenerator Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

	void Start ()
    {
        nextGenerate = Time.time + generateDelay;
        activeUFO = new List<UFO>();
	}
	
	void Update ()
    {
        if (Time.time > nextGenerate)
            Generate();
	}

    public void EnableGeneration(bool action)
    {
        hasToGenerate = action;
    }

    private void Generate()
    {
        if (!hasToGenerate)
            return;

        nextGenerate = Time.time + generateDelay;
        UFO ufo;

        if (Random.Range(0, 100) < 75)
            ufo = ObjectPool.Get<UFO>();
        else
            ufo = ObjectPool.Get<MovingUFO>();

        ufo.Initialize(RandomPosition());
        activeUFO.Add(ufo);
    }

    public void RemoveActiveUFO(UFO ufo)
    {
        activeUFO.Remove(ufo);
    }

    public List<UFO> GetUFOs()
    {
        return activeUFO;
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-20, 20), Random.Range(5, 10), 0);
    }
}

using System.Collections.Generic;
using UnityEngine;

public delegate void OnUFOCountChange(int qta);

public class WaveManager : MonoBehaviour
{
    private List<UFO> activeUFO;
    private Wave wave;

    public event OnUFOCountChange onUfoCountChange;

    public static WaveManager Instance
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
        activeUFO = new List<UFO>();
	}

    public void SetWave(Wave wave)
    {
        this.wave = wave;
        //wave.GetNext().TriggerEvent();
        NextEvent();
    }

    public void NextEvent()
    {
        Debug.Log("Next event!");
        WaveEvent wevent = wave.GetNext();
        if (wevent != null)
        {
            wevent.TriggerEvent();
        }
        else
        {
            Debug.Log("Wave ended");
            GameManager.Instance.OnWaveEnd(wave.GetID());
        }
    }

    public void RemoveActiveUFO(UFO ufo)
    {
        activeUFO.Remove(ufo);

        if (onUfoCountChange != null)
            onUfoCountChange(activeUFO.Count);
    }

    public void AddUFO(UFO ufo)
    {
        activeUFO.Add(ufo);

        if(onUfoCountChange != null)
            onUfoCountChange(activeUFO.Count);
    }

    public List<UFO> GetUFOs()
    {
        return activeUFO;
    }
}

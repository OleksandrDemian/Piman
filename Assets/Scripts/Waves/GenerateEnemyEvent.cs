using System.Collections;
using UnityEngine;

public enum UFOType
{
    UFO,
    MOVINGUFO,
    MINEUFO,
    NONE
}

public class GenerateEnemyEvent : WaveEvent
{
    private UFOType type;
    private int qta;
    private int delay;

    public GenerateEnemyEvent(UFOType type, int qta, int delay)
    {
        this.type = type;
        this.qta = qta;
        this.delay = delay;
    }

    public override void TriggerEvent()
    {
        WaveManager.Instance.StartCoroutine(GenerateUFOs());
    }

    private IEnumerator GenerateUFOs()
    {
        for (int i = 0; i < qta; i++)
        {
            yield return new WaitForSeconds(delay);

            UFO ufo;
            Debug.Log("I'm creating: " + type);
            switch (type)
            {
                case UFOType.UFO:
                    ufo = ObjectPool.Get<UFO>();
                    ufo.Initialize(RandomPosition());
                    break;
                case UFOType.MOVINGUFO:
                    ufo = ObjectPool.Get<MovingUFO>();
                    ufo.Initialize(RandomPosition());
                    break;

                case UFOType.MINEUFO:
                    ufo = ObjectPool.Get<MinesUFO>();
                    ufo.Initialize(RandomPosition());
                    break;

                case UFOType.NONE:
                    break;
            }
        }
        WaveManager.Instance.NextEvent();
    }

    private Vector3 RandomPosition()
    {
        return GameManager.Instance.GetRandomPosition(true);
    }
}
using UnityEngine;

public class PowerUpEvent : WaveEvent
{
    public override void TriggerEvent()
    {
        PoweUp powerUp = ObjectPool.Get<PoweUp>();
        powerUp.Initialize(Random.Range(0, 10) < 5 ? true : false);
        powerUp.SetEffect(new ResetHealthPowerUp());
        WaveManager.Instance.NextEvent();
    }
}
using System.Collections;

public class DelayEvent : WaveEvent
{
    private int delaySec;

    public DelayEvent(int delaySec)
    {
        this.delaySec = delaySec;
    }

    public override void TriggerEvent()
    {
        WaveManager.Instance.StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new UnityEngine.WaitForSeconds(delaySec);
        WaveManager.Instance.NextEvent();
    }
}
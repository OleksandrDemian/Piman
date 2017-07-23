public class WaitEvent : WaveEvent
{
    public override void TriggerEvent()
    {
        WaveManager.Instance.onUfoCountChange += Controll;
    }

    private void Controll(int count)
    {
        if (count > 0)
            return;
        WaveManager.Instance.NextEvent();
        WaveManager.Instance.onUfoCountChange -= Controll;
    }
}
using System.Collections.Generic;

public class Wave
{
    private List<WaveEvent> events;
    private int curElementIndex = 0;
    private int id;

    public Wave()
    {
        events = new List<WaveEvent>();
    }

    public void SetID(int id)
    {
        this.id = id;
    }

    public int GetID()
    {
        return id;
    }

    public void AddEvent(WaveEvent _event)
    {
        events.Add(_event);
    }

    public WaveEvent GetNext()
    {
        if (curElementIndex < events.Count)
        {
            return events[curElementIndex++];
        }

        return null;
    }
}
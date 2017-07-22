using UnityEngine;

public enum DamageResultType
{
    HIT,
    MISS,
    DESTROYED
}

public class DamageResult
{
    private GameObject target;
    private DamageResultType result;

    public DamageResult(GameObject target, DamageResultType result)
    {
        this.target = target;
        this.result = result;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public DamageResultType GetResult()
    {
        return result;
    }
}
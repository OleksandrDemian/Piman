using UnityEngine;

public class ResetHealthPowerUp : PowerUpEffect
{
    public override void Trigger(GameObject target)
    {
        Piman piman = target.GetComponent<Piman>();
        if (piman == null)
            return;
        piman.GetHealth().ResetValue();
    }
}

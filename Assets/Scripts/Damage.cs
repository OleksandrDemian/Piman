using UnityEngine;

public class Damage {

    private GameObject parent;
    private int amount;

    public Damage(GameObject parent, int amount)
    {
        this.parent = parent;
        this.amount = amount;
    }

    public GameObject GetParent()
    {
        return parent;
    }

    public int GetDamage()
    {
        return amount;
    }
}

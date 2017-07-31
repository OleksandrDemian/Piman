using System.Collections;
using UnityEngine;

public class MinesUFO : UFO
{
    private GameObject alertSign;
    private Vector3 targetPos;
    private bool hasToMove = true;

    protected override void Start()
    {
        base.Start();
        shootingDelay = 8f;
    }

    public override void Initialize(Vector3 position)
    {
        base.Initialize(position);

        health = new Attribute(45);
        health.onValueChange = OnHealthValueChange;

        alertSign = transform.FindChild("alert").gameObject;
        alertSign.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        if (hasToMove)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, targetPos, Time.deltaTime * .1f);
        }
    }

    protected override void Shoot()
    {
        StartCoroutine(ShootingFase());
    }

    private IEnumerator ShootingFase()
    {
        nextShoot = Time.time + shootingDelay;
        alertSign.SetActive(true);
        hasToMove = false;
        yield return new WaitForSeconds(1f);
        alertSign.SetActive(false);

        Mine mine = ObjectPool.Get<Mine>();
        mine.Initialize(transform.position - Vector3.up);

        RandomDirection();
        hasToMove = true;
    }

    private void RandomDirection()
    {
        targetPos = GameManager.Instance.GetRandomPosition(true);
    }
}

using System.Collections;
using UnityEngine;

public class MovingUFO : UFO
{
    private float changePositionTime = 0f;
    private LineRenderer laserLine;
    private GameObject alertSign;

    protected override void Start()
    {
        base.Start();
        laserLine = GetComponentInChildren<LineRenderer>();
        laserLine.enabled = false;
    }

    public override void Initialize(Vector3 position)
    {
        base.Initialize(position);

        health = new Attribute(30);
        health.onValueChange = OnHealthValueChange;

        alertSign = transform.FindChild("alert").gameObject;
        alertSign.SetActive(false);

        changePositionTime = Time.time + 5;
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time > changePositionTime)
            StartCoroutine(ChangePosition());
    }

    private IEnumerator ChangePosition()
    {
        changePositionTime = Time.time + 15;

        alertSign.SetActive(true);
        yield return new WaitForSeconds(1f);
        alertSign.SetActive(false);

        Transform pimanPos = Piman.Instance.transform;
        laserLine.enabled = true;
        int delta = pimanPos.position.x > transform.position.x ? 7 : -7;

        Vector2 targetPos = new Vector2(transform.position.x + delta, transform.position.y);
        while (transform.position.x != targetPos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * 8);
            if (Mathf.Abs(transform.position.x - pimanPos.position.x) < 0.5f)
            {
                Piman.Instance.Damage(1);
            }
            yield return new WaitForEndOfFrame();
        }
        laserLine.enabled = false;
    }
}

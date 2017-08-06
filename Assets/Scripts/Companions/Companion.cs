using UnityEngine;

public class Companion : MonoBehaviour
{
    private Transform target;
    private Vector3 targetPos;
    private int speed = 2;
    private int minDistance = 2;

	private void Start ()
    {
        target = Piman.Instance.transform;
        targetPos = target.position;
	}
	
	private void Update ()
    {
        Float();
	}

    private void Float()
    {
        if (Vector3.Distance(target.position, targetPos) > minDistance)
        {
            Vector3 offset = transform.position - target.position;
            targetPos = target.position + (offset.normalized * minDistance);
        }

        Vector3 endPoin = targetPos;
        endPoin.y = Mathf.Sin(Time.time) + targetPos.y + 1;
        transform.position = Vector3.Lerp(transform.position, endPoin, speed * Time.deltaTime);
    }
}

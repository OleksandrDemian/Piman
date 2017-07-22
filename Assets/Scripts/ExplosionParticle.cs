using UnityEngine;

public class ExplosionParticle : MonoBehaviour {

    private Vector2 targetPosition;

	void Start () {
		
	}
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
        targetPosition.y -= Time.deltaTime;
	}

    public void Initialize(Vector3 targetPosition)
    {
        transform.localPosition = Vector3.zero;
        targetPosition.y = Mathf.Abs(targetPosition.y);
        this.targetPosition = transform.position + targetPosition;
    }
}

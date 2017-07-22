using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHotspot : MonoBehaviour
{
    private Transform[] points;

	void Awake ()
    {
        points = new Transform[2];
        //points[0] = transform.GetChild(0).GetComponent<LineRenderer>();
        //points[1] = transform.GetChild(1).GetComponent<LineRenderer>();

        points[0] = transform.GetChild(0);
        points[1] = transform.GetChild(1);
    }

    public void SetSight(float value)
    {
        //points[0].SetPosition(1, new Vector3(value, 2, 0));
        //points[1].SetPosition(1, new Vector3(-value, 2, 0));

        points[0].localPosition = new Vector3(value, 1, 0);
        points[1].localPosition = new Vector3(-value, 1, 0);
        //Debug.Log("Sight: " + value);
    }

    public void Enable(bool action)
    {
        //points[0].enabled = action;
        //points[1].enabled = action;
        points[0].gameObject.SetActive(action);
        points[1].gameObject.SetActive(action);
    }
}

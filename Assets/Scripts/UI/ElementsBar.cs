using UnityEngine;
using UnityEngine.UI;

public class ElementsBar : MonoBehaviour
{
    private Image[] images;

	private void Start ()
    {
        images = GetComponentsInChildren<Image>();
	}

    public int Max()
    {
        return images.Length;
    }

    public void Set(int qta)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = i <= qta ? true : false;
        }
    }
}

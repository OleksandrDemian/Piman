using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    [SerializeField]
    private KeyCode nextKey;

    private Text personName;
    private Text text;
    private Dialog dialog;
    private IEnumerator spaceListener;

    public static DialogWindow Instance
    {
        get;
        private set;
    }

    public void OpenDialogWindow(Dialog dialog)
    {
        gameObject.SetActive(true);
        this.dialog = dialog;
        StartCoroutine(spaceListener);
        Next();
    }

    public void Next()
    {
        Frase frase = dialog.GetNextFrase();
        if (frase == null)
        {
            StopCoroutine(spaceListener);
            return;
        }

        personName.text = frase.PersonName;
        text.text = frase.Text;
    }

    public void Skip()
    {

    }

	private void Awake ()
    {
        Instance = this;
        personName = transform.FindChild("PersonName").GetComponent<Text>();
        text = transform.FindChild("DialogText").GetComponent<Text>();
        gameObject.SetActive(false);
        spaceListener = ListenForKey();
    }

    private IEnumerator ListenForKey()
    {
        for (;;)
        {
            if (Input.GetKeyDown(nextKey))
                Next();
            yield return null;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}

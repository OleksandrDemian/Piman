using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    private Text personName;
    private Text text;
    private Dialog dialog;

    public static DialogWindow Instance
    {
        get;
        private set;
    }

    public void OpenDialogWindow(Dialog dialog)
    {
        gameObject.SetActive(true);
        this.dialog = dialog;
        Next();
    }

    public void Next()
    {
        Frase frase = dialog.GetNextFrase();
        if (frase == null)
            return;

        personName.text = frase.PersonName;
        text.text = frase.Text;
    }

	private void Awake ()
    {
        Instance = this;
        personName = transform.FindChild("PersonName").GetComponent<Text>();
        text = transform.FindChild("DialogText").GetComponent<Text>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        
    }

    private void Update ()
    {
		
	}

    public void Close()
    {
        gameObject.SetActive(false);
    }
}

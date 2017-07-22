using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance
    {
        get;
        private set;
    }

	private void Awake ()
    {
        Instance = this;
	}

    private void Start()
    {
        OpenStartDialog();
        Piman.Instance.onDead = GameOver;
    }
	
	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F))
            OpenStartDialog();
	}

    private void OpenStartDialog()
    {
        UfoGenerator.Instance.EnableGeneration(false);
        Piman.Instance.enabled = false;

        Dialog dialog = XmlReader.GetDialog("startdialog");

        dialog.onDialogEnd = delegate ()
        {
            UfoGenerator.Instance.EnableGeneration(true);
            Piman.Instance.enabled = true;
        };

        DialogWindow.Instance.OpenDialogWindow(dialog);
    }

    public void GameOver()
    {
        List<UFO> ufos = UfoGenerator.Instance.GetUFOs();

        for (int i = 0; i < ufos.Count; i++)
        {
            ufos[i].GoOut();
        }

        UfoGenerator.Instance.enabled = false;

        Dialog dialog = XmlReader.GetDialog("pimandead");
        dialog.onDialogEnd = delegate ()
        {
            SceneLoader.LoadScene(0);
        };
        DialogWindow.Instance.OpenDialogWindow(dialog);
    }
}

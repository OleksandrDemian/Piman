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

	}

    private void OpenStartDialog()
    {
        Wave wave = XmlReader.GetWave("0");
        WaveManager.Instance.SetWave(wave);
    }

    public void OnWaveEnd(int id)
    {
        id++;
        Debug.Log("Start wave " + id);
        Wave wave = XmlReader.GetWave(id.ToString());

        if (wave != null)
            WaveManager.Instance.SetWave(wave);
        else
        {
            Dialog dialog = XmlReader.GetDialog("endgame");
            dialog.onDialogEnd = delegate ()
            {
                SceneLoader.LoadScene(0);
            };
            DialogWindow.Instance.OpenDialogWindow(dialog);
        }
    }

    public void GameOver()
    {
        List<UFO> ufos = WaveManager.Instance.GetUFOs();

        for (int i = 0; i < ufos.Count; i++)
        {
            ufos[i].GoOut();
        }

        WaveManager.Instance.enabled = false;

        Dialog dialog = XmlReader.GetDialog("pimandead");
        dialog.onDialogEnd = delegate ()
        {
            SceneLoader.LoadScene(0);
        };
        DialogWindow.Instance.OpenDialogWindow(dialog);
    }
}

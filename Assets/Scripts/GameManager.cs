using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    public Vector3 MapBounds
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //OpenStartDialog();
        GetBounds();
        Piman.Instance.onDead = GameOver;

        int level = PlayerPrefs.GetInt("Level", 0);
        OpenWave(level);
    }

    private void Update()
    {

    }

    public void OnWaveEnd(int id)
    {
        id++;
        SaveGame(id);
        OpenWave(id);
    }

    private void OpenWave(int id)
    {
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

    private void GetBounds()
    {
        MapBounds = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
    }

    public Vector3 GetRandomPosition(bool up)
    {
        return new Vector3(GetRandomX(), Random.Range(up ? 0 : -MapBounds.y, MapBounds.y), 0) * .8f;
    }

    public float GetRandomX()
    {
        return Random.Range(-MapBounds.x, MapBounds.x);
    }

    private void SaveGame(int level)
    {
        Debug.Log("Saved: " + level);
        PlayerPrefs.SetInt("Level", level);
    }
}

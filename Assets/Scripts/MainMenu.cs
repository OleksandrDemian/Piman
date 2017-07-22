using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneLoader.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

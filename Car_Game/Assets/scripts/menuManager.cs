using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("track_choice");
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("instructions");
    }

    public void Exit()
    {
        Application.Quit();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlManager : MonoBehaviour {
    public static bool isTilt = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TiltControl()
    {
        isTilt = true;
        SceneManager.LoadScene("level1");
    }

    public void TouchControl()
    {
        isTilt = false;
        SceneManager.LoadScene("level1");
    }
}

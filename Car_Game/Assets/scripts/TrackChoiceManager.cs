using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TrackChoiceManager : MonoBehaviour {
    public static bool isCityTrack = true;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CityTrack()
    {
        isCityTrack = true;
        SceneManager.LoadScene("control");
    }

    public void CountryTrack()
    {
        isCityTrack = false;
        SceneManager.LoadScene("control");
    }
}

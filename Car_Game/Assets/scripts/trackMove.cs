using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class trackMove : MonoBehaviour {
    public float speed = 10f;
    Vector2 offset;

    public Texture[] track;
    
    // Use this for initialization
    void Start () {
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);

        if (TrackChoiceManager.isCityTrack)
        {
            GetComponent<Renderer>().material.mainTexture = track[0];
        }
        else
        {
            GetComponent<Renderer>().material.mainTexture = track[1];
        }
    }
	
	// Update is called once per frame
	void Update () {
        offset = new Vector2(0, Time.time * speed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
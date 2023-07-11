using UnityEngine;
using System.Collections;

public class carController : MonoBehaviour {
    public float carSpeed = 10f;
    private float minPos = -2.8f;
    private float maxPos = 2.8f;

    Vector3 position;
    public uiManager ui;
    public AudioManager am;

    Rigidbody2D rb;
    float middle;

    bool currentPlatformAndroid = false;
    bool currentPlatformiOS = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        #if (UNITY_ANDROID)
        {
            currentPlatformAndroid = true;
        }
        #elif (UNITY_IOS)
        {
            currentPlatformiOS = true;
        }
        #else
        {
            currentPlatformAndroid = false;
            currentPlateformiOS = false;
        }

#endif
    }

    // Use this for initialization
    void Start () {
        middle = Screen.width / 2;
        am.carSound.Play();
        position = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        if (currentPlatformAndroid || currentPlatformiOS)
        {
            if(ControlManager.isTilt)
            {
                AccelerometerMove();
            }
            else
            {
                TouchMove();   
            }
        }
        else
        {
            position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
        }

        position = transform.position;
        position.x = Mathf.Clamp(position.x, minPos, maxPos);
        transform.position = position;
    }

    //called when there is a collision between two game objects
    void OnCollisionEnter2D (Collision2D col) {
        if(col.gameObject.tag == "Enemy Car")
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            am.carSound.Stop();
            ui.GameOverActivated();
        }
    }

    void TouchMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.position.x < middle && touch.phase == TouchPhase.Began)
            {
                MoveLeft();
            }
            else if(touch.position.x > middle && touch.phase == TouchPhase.Began)
            {
                MoveRight();
            }
        }
        else
        {
            SetVelocityZero();
        }
    }

    void AccelerometerMove()
    {
        float x = Input.acceleration.x;

        //Debug.Log(x);

        if(x < -0.1f)
        {
            MoveLeft();
        }
        else if(x > 0.1f)
        {
            MoveRight();
        }
        else {
            SetVelocityZero();
        }
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-carSpeed, 0);
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(carSpeed, 0);
    }

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
    }
}

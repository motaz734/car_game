using UnityEngine;
using System.Collections;

public class carSpawner : MonoBehaviour
{
    public GameObject[] cars;
    int carNo;
    private float minPos = -2.2f;
    private float maxPos = 2.2f;
    public float delayTimer = 1.0f;
    private float delay;

    // Use this for initialization
    void Start()
    {
        delay = delayTimer;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            if(delayTimer >= 0.5)
            {
                delayTimer -= 0.005f;
            }
            Vector3 pos = new Vector3(Random.Range(minPos, maxPos), transform.position.y, transform.position.z);
            carNo = Random.Range(0, 8);
            Instantiate(cars[carNo], pos, transform.rotation);
            delay = delayTimer;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour {

    public GameObject bison;
    float timer = 0;
    float randomizedDelay;
    public float timeMin, timeMax;
    // Use this for initialization
    void Start () {
        randomizedDelay = Random.Range(1, 3);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > randomizedDelay)
        {
            Instantiate(bison,transform.position, new Quaternion(0,0,0,0));
            randomizedDelay = Random.Range(timeMin, timeMax);
            timer = 0;
        }
    }
}

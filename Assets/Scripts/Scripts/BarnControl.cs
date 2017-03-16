using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnControl : MonoBehaviour {

    public GameController gc;

	// Use this for initialization
	void Start () {
        GameObject controller = GameObject.FindGameObjectWithTag("GameController");
        gc = controller.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bison")
        {
            Destroy(collision.gameObject);
            gc.IncreaseScore();
        }
    }
}

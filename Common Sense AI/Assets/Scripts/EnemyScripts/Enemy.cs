using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float amour = 5;
    public float attack = 5;
    private int health = 20;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0)
        {
            Destroy(this);
        }
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float amour = 5;
    public float attack = 5;
    public int health = 100;
    public bool selected = false;
    public List<GameObject> fellowenemies;

    bool called = false;
    float time = 0;
    // Use this for initialization
    void Start ()
    {
        fellowenemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0)
        {
            Destroy(this);
        }
        
    }

}

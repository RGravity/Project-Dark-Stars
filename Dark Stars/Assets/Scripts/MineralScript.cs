﻿using UnityEngine;
using System.Collections;

public class MineralScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	     
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.Find("SpaceshipPlayer"))
        {
            //Pick UP
            Debug.Log(gameObject.name + " picked up!");
            Destroy(gameObject); 
        }
    }
}

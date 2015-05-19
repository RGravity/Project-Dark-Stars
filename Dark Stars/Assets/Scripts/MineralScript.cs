using UnityEngine;
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
            if (gameObject.name.Contains(EnumMinerals.xenonite1.ToString()))
            {
                GameObject.FindObjectOfType<PlayerController>().Score += 1;
            }
            else if (gameObject.name.Contains(EnumMinerals.helionite2.ToString()))
            {
                GameObject.FindObjectOfType<PlayerController>().Score += 2;
            }
            else if (gameObject.name.Contains(EnumMinerals.argonite5.ToString()))
            {
                GameObject.FindObjectOfType<PlayerController>().Score += 5;
            }
            else if (gameObject.name.Contains(EnumMinerals.neonite10.ToString()))
            {
                GameObject.FindObjectOfType<PlayerController>().Score += 10;
            }
            Destroy(gameObject); 
        }
    }
}

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
                
            }
            else if (gameObject.name.Contains(EnumMinerals.helionite2.ToString()))
            {

            }
            else if (gameObject.name.Contains(EnumMinerals.argonite5.ToString()))
            {

            }
            else if (gameObject.name.Contains(EnumMinerals.neonite10.ToString()))
            {
                
            }
            Destroy(gameObject); 
        }
    }
}

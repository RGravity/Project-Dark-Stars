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
            PlayerController Player = GameObject.FindObjectOfType<PlayerController>();
            //Pick UP
            if (gameObject.name.Contains(EnumMinerals.xenonite1.ToString()))
            {
                Player.Score += 1;
                Player.AmountOfXenonite += 1;
            }
            else if (gameObject.name.Contains(EnumMinerals.helionite2.ToString()))
            {
                Player.Score += 2;
                Player.AmountOfHelionite += 1;
            }
            else if (gameObject.name.Contains(EnumMinerals.argonite5.ToString()))
            {
                Player.Score += 5;
                Player.AmountOfArgonite += 1;
            }
            else if (gameObject.name.Contains(EnumMinerals.neonite10.ToString()))
            {
                Player.Score += 10;
                Player.AmountOfNeonite += 1;
            }
            Destroy(gameObject); 
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            //--Vector3 forward = Vector3.forward; //0,0,1
            Vector3 forward = gameObject.transform.forward; //its the forward in which the character is pointing at.

            //--Add the Forward vector to the position
            forward = forward * 0.5f; //--Needs floats

            //--Apply the change
            gameObject.GetComponent<Rigidbody>().velocity = forward; 
        }
    }
}

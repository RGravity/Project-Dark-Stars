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
        if (Input.GetKey(KeyCode.W))
        {
            //--Vector3 forward = Vector3.forward; //0,0,1
            Vector3 forward = gameObject.transform.forward; //its the forward in which the character is pointing at.

            //--Add the Forward vector to the position
            forward = forward * 0.1f; //--Needs floats
            Vector3 maxforward = forward * 10;

            //--Apply the change
            gameObject.GetComponent<Rigidbody>().velocity += forward;

            if (gameObject.GetComponent<Rigidbody>().velocity == maxforward)
            {
                gameObject.GetComponent<Rigidbody>().velocity = maxforward;
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            Vector3 reverse = gameObject.transform.forward;

            reverse = reverse * 0.1f; //--Needs floats

            Vector3 maxreverse = reverse * 5;

            gameObject.GetComponent<Rigidbody>().velocity -= reverse;

            if (gameObject.GetComponent<Rigidbody>().velocity == maxreverse)
            {
                gameObject.GetComponent<Rigidbody>().velocity = maxreverse;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Vector3 leftThrust = gameObject.transform.right;

            leftThrust = leftThrust * 0.1f; //--Needs floats

            Vector3 maxleftThrust = leftThrust * 5;

            gameObject.GetComponent<Rigidbody>().velocity -= leftThrust;

            if (gameObject.GetComponent<Rigidbody>().velocity == maxleftThrust)
            {
                gameObject.GetComponent<Rigidbody>().velocity = maxleftThrust;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //--Vector3 forward = Vector3.forward; //0,0,1
            Vector3 rightThrust = gameObject.transform.right; //its the forward in which the character is pointing at.

            //--Add the Forward vector to the position
            rightThrust = rightThrust * 0.1f; //--Needs floats
            Vector3 maxrightThrust = rightThrust * 10;

            //--Apply the change
            gameObject.GetComponent<Rigidbody>().velocity += rightThrust;

            if (gameObject.GetComponent<Rigidbody>().velocity == maxrightThrust)
            {
                gameObject.GetComponent<Rigidbody>().velocity = maxrightThrust;
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 0, -1);
        }
    }
}

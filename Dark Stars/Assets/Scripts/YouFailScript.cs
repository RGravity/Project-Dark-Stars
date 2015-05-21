using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class YouFailScript : MonoBehaviour {

    private bool youDied = false;
    private bool showGUI = false;

    public bool YouDiedBool { set { youDied = value; } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (youDied)
        {
            Debug.Log("You Died");
            Time.timeScale = 0;
            youDied = true;
            showGUI = true;
        }
        if (showGUI)
        {
            GameObject.Find("Retry").GetComponent<Button>().enabled = true;
            GameObject.Find("Quit").GetComponent<Button>().enabled = true;
            gameObject.GetComponent<Image>().enabled = true;
            if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                Application.LoadLevel("Prototype");
            }
            if ((Input.GetKey(KeyCode.Joystick1Button6)))
            {
                Application.LoadLevel("MainMenu"); 
            }
        }
	}
}

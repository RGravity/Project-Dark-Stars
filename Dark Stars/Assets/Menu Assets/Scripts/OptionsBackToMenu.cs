using UnityEngine;
using System.Collections;

public class OptionsBackToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Application.LoadLevel(0);
        }
    }
}

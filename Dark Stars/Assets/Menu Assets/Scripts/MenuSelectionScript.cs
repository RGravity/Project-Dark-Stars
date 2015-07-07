using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSelectionScript : MonoBehaviour {

    Image PlayArrow;
    Image OptionsArrow;
    Image ExitArrow;

    bool selectionAllowed = true;

	// Use this for initialization
	void Start () {
        PlayArrow = GameObject.Find("PlayArrow").GetComponent<Image>();
        OptionsArrow = GameObject.Find("OptionsArrow").GetComponent<Image>();
        ExitArrow = GameObject.Find("ExitArrow").GetComponent<Image>();
        


	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") < -0.5f && selectionAllowed)
        {
            selectionAllowed = false;

            if (PlayArrow.enabled == true)
            {
                PlayArrow.enabled = false;
                OptionsArrow.enabled = true;
            }
            else if (OptionsArrow.enabled == true)
            {
                OptionsArrow.enabled = false;
                ExitArrow.enabled = true;
            }
            else if (ExitArrow.enabled == true)
            {
                ExitArrow.enabled = false;
                PlayArrow.enabled = true;
            }
        }

        if (Input.GetAxis("Vertical") > 0.5f && selectionAllowed)
        {
            selectionAllowed = false;

            if (PlayArrow.enabled == true)
            {
                PlayArrow.enabled = false;
                ExitArrow.enabled = true;
                //OptionsArrow.enabled = true;
            }
            else if (ExitArrow.enabled == true)
            {
                ExitArrow.enabled = false;
                OptionsArrow.enabled = true;
            }
            else if (OptionsArrow.enabled == true)
            {
                OptionsArrow.enabled = false;
                PlayArrow.enabled = true;
            }
        }

        if (Input.GetAxis("Vertical") > -0.5f && Input.GetAxis("Vertical") < 0.5f && !selectionAllowed)
        {
            selectionAllowed = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (PlayArrow.enabled == true)
            {
                Application.LoadLevel(3);
            }
            else if (OptionsArrow.enabled == true)
            {
                Application.LoadLevel(1);
            }
            else if (OptionsArrow.enabled == true)
            {
                Application.Quit();
            }
        }
	}
}

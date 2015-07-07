using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class YouFailScript : MonoBehaviour {

    private bool youDied = false;
    private bool showGUI = false;
    private bool showArrow = false;
    private bool ArrowsShown = false;
    private Image RetryArrow;
    private Image QuitArrow;

    private bool selectionAllowed = false;

    public bool YouDiedBool { get { return youDied; } set { youDied = value; } }

	// Use this for initialization
	void Start () {
        RetryArrow = GameObject.Find("RetryArrow").GetComponent<Image>();
        QuitArrow = GameObject.Find("QuitArrow").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (youDied)
        {
            GameObject.FindObjectOfType<Pause>().allowUnpause = false;
            Time.timeScale = 0;
            youDied = true;
            showGUI = true;
        }
        if (showGUI)
        {
            GameObject.Find("Retry").GetComponent<Button>().enabled = true;
            GameObject.Find("Quit").GetComponent<Button>().enabled = true;
            showArrow = true;

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

        if (showArrow && !ArrowsShown)
        {
            RetryArrow.enabled = true;
            QuitArrow.enabled = false;
            ArrowsShown = true;
        }

        if (Input.GetAxis("Horizontal") < -0.5f && selectionAllowed)
        {
            selectionAllowed = false;

            if (RetryArrow.enabled == true)
            {
                RetryArrow.enabled = false;
                QuitArrow.enabled = true;
            }
            else if (QuitArrow.enabled == true)
            {
                QuitArrow.enabled = false;
                RetryArrow.enabled = true;
            }
        }

        if (Input.GetAxis("Horizontal") > 0.5f && selectionAllowed)
        {
            selectionAllowed = false;

            if (RetryArrow.enabled == true)
            {
                RetryArrow.enabled = false;
                QuitArrow.enabled = true;
            }
            else if (QuitArrow.enabled == true)
            {
                QuitArrow.enabled = false;
                RetryArrow.enabled = true;
            }
        }

        if (Input.GetAxis("Horizontal") > -0.5f && Input.GetAxis("Horizontal") < 0.5f && !selectionAllowed)
        {
            selectionAllowed = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (QuitArrow.enabled == true)
            {
                Application.LoadLevel(0);
            }
            else if (RetryArrow.enabled == true)
            {
                Application.LoadLevel(3);
            }
        }
	}
}

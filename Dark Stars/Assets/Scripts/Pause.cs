using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour
{
    private Image ResumeArrow;
    private Image ExitGameArrow;
    private bool selectionAllowed = false;


    private bool pauseGame = false;
    public bool allowUnpause = false;
    private bool showGUI = false;

    void Start()
    {
        ResumeArrow = GameObject.Find("ResumeArrow").GetComponent<Image>();
        ExitGameArrow = GameObject.Find("ExitGameArrow").GetComponent<Image>();
    }

    void Update()
    {
        if (!GameObject.FindObjectOfType<YouFailScript>().YouDiedBool)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                pauseGame = !pauseGame;

                if (pauseGame == true)
                {
                    Time.timeScale = 0;
                    pauseGame = true;
                    allowUnpause = true;
                    showGUI = true;
                    ResumeArrow.enabled = true;
                }
            }

            if (pauseGame == false && allowUnpause == true)
            {
                Time.timeScale = 1;
                pauseGame = false;
                allowUnpause = false;
                showGUI = false;
                ExitGameArrow.enabled = false;
                ResumeArrow.enabled = false;
            }

            if (showGUI == true)
            {
                GameObject.Find("Pause").GetComponent<Image>().enabled = true;

            }
            else
                GameObject.Find("Pause").GetComponent<Image>().enabled = false;

            if (pauseGame == true && (Input.GetKey(KeyCode.Joystick1Button6) || Input.GetKey(KeyCode.Backspace)))
            {
                Application.LoadLevel("MainMenu");
            }



            if (Input.GetAxis("Vertical") < -0.5f && selectionAllowed)
            {
                selectionAllowed = false;

                if (ResumeArrow.enabled == true)
                {
                    ResumeArrow.enabled = false;
                    ExitGameArrow.enabled = true;
                }
                else if (ExitGameArrow.enabled == true)
                {
                    ExitGameArrow.enabled = false;
                    ResumeArrow.enabled = true;
                }
            }

            if (Input.GetAxis("Vertical") > 0.5f && selectionAllowed)
            {
                selectionAllowed = false;

                if (ResumeArrow.enabled == true)
                {
                    ResumeArrow.enabled = false;
                    ExitGameArrow.enabled = true;
                }
                else if (ExitGameArrow.enabled == true)
                {
                    ExitGameArrow.enabled = false;
                    ResumeArrow.enabled = true;
                }
            }

            if (Input.GetAxis("Vertical") > -0.5f && Input.GetAxis("Vertical") < 0.5f && !selectionAllowed)
            {
                selectionAllowed = true;
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if (ExitGameArrow.enabled == true)
                {
                    Application.LoadLevel(0);
                }
                else if (ResumeArrow.enabled == true)
                {
                    pauseGame = !pauseGame;
                }
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour
{

    private bool pauseGame = false;
    public bool allowUnpause = false;
    private bool showGUI = false;

    void Update()
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
            }
        }

        if (pauseGame == false && allowUnpause == true)
        {
            Time.timeScale = 1;
            pauseGame = false;
            allowUnpause = false;
            showGUI = false;
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
    }
}

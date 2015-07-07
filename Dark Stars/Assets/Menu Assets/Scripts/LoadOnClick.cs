using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int Prototype)
	{
		Application.LoadLevel (Prototype);
	}

    public void ExitScene()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            Application.LoadLevel(0);
        }
    }
}
using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int Prototype)
	{
		Application.LoadLevel (Prototype);
	}
}
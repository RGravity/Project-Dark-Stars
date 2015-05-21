using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindObjectOfType<ScoreData>().PassScores();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateScores(int xenonite, int helionite, int argonite, int neonite, int speeder, int assailant, int bruiser)
    {
        GameObject.Find("xeonite").GetComponent<Text>().text = "-" + xenonite;
        GameObject.Find("helionite").GetComponent<Text>().text = "-" + helionite;
        GameObject.Find("argonite").GetComponent<Text>().text = "-" + argonite;
        GameObject.Find("neonite").GetComponent<Text>().text = "-" + neonite;
        GameObject.Find("speeder").GetComponent<Text>().text = "X " + speeder;
        GameObject.Find("assailant").GetComponent<Text>().text = "X " + assailant;
        GameObject.Find("bruiser").GetComponent<Text>().text = "X " + bruiser;
    }
}

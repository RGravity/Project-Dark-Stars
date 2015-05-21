using UnityEngine;
using System.Collections;

public class ScoreData : MonoBehaviour {

    private int _xenonite;

    public int Xenonite
    {
        get { return _xenonite; }
        set { _xenonite = value; }
    }

    private int _helionite;

    public int Helionite
    {
        get { return _helionite; }
        set { _helionite = value; }
    }

    private int _argonite;

    public int Argonite
    {
        get { return _argonite; }
        set { _argonite = value; }
    }

    private int _neonite;

    public int Neonite
    {
        get { return _neonite; }
        set { _neonite = value; }
    }

    private int _speeder;

    public int Speeder
    {
        get { return _speeder; }
        set { _speeder = value; }
    }

    private int _assailant;

    public int Assailant
    {
        get { return _assailant; }
        set { _assailant = value; }
    }

    private int _bruiser;

    public int Bruiser
    {
        get { return _bruiser; }
        set { _bruiser = value; }
    }
    

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}

    public void PassScores()
    {
        GameObject.FindObjectOfType<Score>().updateScores(_xenonite, _helionite, _argonite, _neonite, _speeder, _assailant, _bruiser);
    }
}

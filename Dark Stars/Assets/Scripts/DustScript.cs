using UnityEngine;
using System.Collections;

public class DustScript : MonoBehaviour {

    private bool Activate = false;
    private Transform Location;

    public bool ActivateAnimation { set { Activate = value; } }
    public Transform PositionToPlace { set { Location = value; } }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckAnimation();
	}

    void CheckAnimation()
    {
        if (Activate)
        {
            gameObject.GetComponent<ParticleSystem>().Play();
            GameObject.Find("Inner").GetComponent<ParticleSystem>().Play();
            Activate = false;
        }
    }
}

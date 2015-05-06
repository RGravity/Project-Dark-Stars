using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public GameObject target;

    Vector3 acceleration;
    Vector3 velocty;
    float speed = 5.0f;

    GameObject[] boids;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        EnemyMovement();
	}

    void EnemyMovement()
    {

    }
}

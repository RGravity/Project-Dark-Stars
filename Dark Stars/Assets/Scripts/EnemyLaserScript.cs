﻿using UnityEngine;
using System.Collections;

public class EnemyLaserScript : MonoBehaviour {

    LineRenderer line;
    public int LaserDistance = 50;
    private GameObject target;
    private Transform parent;
    private float angle = 25;
    private string name = "";
    private ParticleSystem laserParticle;
    private ParticleSystem laserParticle2;

	// Use this for initialization
	void Start () {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        target = GameObject.FindGameObjectWithTag("Player");
        parent = gameObject.transform.parent;
        name = gameObject.name;
        string EnemyID = name.Substring(name.Length - 1);

        ParticleSystem[] ParticleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
        for (int i = 0; i < ParticleSystems.Length; i++)
        {
            if (ParticleSystems[i].name.Contains(EnemyID))
            {
                if (ParticleSystems[i].name.Contains("Laser"))
                {
                    laserParticle = ParticleSystems[i];
                }
                if (ParticleSystems[i].name.Contains("Core"))
                {
                    laserParticle2 = ParticleSystems[i];
                }
            }
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, parent.position);
        if (distance < LaserDistance && (Vector3.Angle(transform.forward, target.transform.position - transform.position) < angle))
        {
            StopCoroutine("FireLaser");
            //Start Laser
            StartCoroutine("FireLaser");
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;

        float distance = Vector3.Distance(target.transform.position, parent.position);
        while (distance < LaserDistance && (Vector3.Angle(transform.forward, target.transform.position - transform.position) < angle))
        {
            laserParticle.Play();
            laserParticle2.Play();
            Vector3 GunTip = GameObject.Find("GunTip Point").transform.position;
            gameObject.GetComponent<LineRenderer>().useWorldSpace = true;
            Ray ray = new Ray(GunTip, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, LaserDistance))
            {
                line.SetPosition(1, hit.point);
                if (hit.rigidbody)
                {
                    if (hit.collider.gameObject.name.Contains("Asteroid"))
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point);
                        GameObject asteroidHit = GameObject.Find(hit.collider.gameObject.name);
                        asteroidHit.GetComponent<AsteroidScript>().Hit = true;
                    }
                    else if (hit.collider.gameObject.GetComponent<PlayerController>())
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point);
                        GameObject playerHit = GameObject.Find("Spaceship");
                        playerHit.GetComponent<PlayerController>().Hit = true;
                    }
                }
            }
            else
                line.SetPosition(1, ray.GetPoint(LaserDistance));

            distance = Vector3.Distance(target.transform.position, parent.position);
            yield return null;
        }
        laserParticle.Stop();
        laserParticle2.Stop();
        line.enabled = false;
        gameObject.GetComponent<LineRenderer>().useWorldSpace = false;
    }
    
}

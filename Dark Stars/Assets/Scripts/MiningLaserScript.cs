using UnityEngine;
using System.Collections;

public class MiningLaserScript : MonoBehaviour {

    LineRenderer line;
    public int LaserDistance = 100;

	// Use this for initialization
	void Start () {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("Fire1"))
        {
            ShootWeapon();
        }

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    StopCoroutine("FireLaser");
        //    StartCoroutine("FireLaser");
        //}
	}

    void ShootWeapon()
    {
        line.enabled = true;

        while (Input.GetButton("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, LaserDistance))
            {
                line.SetPosition(1, hit.point);
                if (hit.rigidbody)
                {
                    AsteroidScript asteroid = hit.collider.GetComponent<AsteroidScript>();
                    if (asteroid)
                    {
                        asteroid.Kill();
                    }


                    EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
                    if (enemy)
                    {
                        enemy.TakeDamage(10f);
                    }

                    if (hit.collider.gameObject.name.Contains("Asteroid"))
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point);
                        GameObject asteroidHit = GameObject.Find(hit.collider.gameObject.name);
                        asteroidHit.GetComponent<AsteroidScript>().Hit = true;
                    }
                    else if (hit.collider.gameObject.name.Contains("Enemy"))
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point);
                        GameObject enemyHit = GameObject.Find(hit.collider.gameObject.name);
                        enemyHit.GetComponent<EnemyScript>().Hit = true;
                    }
                }
            }
            else
                line.SetPosition(1, ray.GetPoint(LaserDistance));
        }

        line.enabled = false;
    }


    IEnumerator FireLaser()
    {
        line.enabled = true;

        while (Input.GetButton("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, LaserDistance))
            {
                line.SetPosition(1, hit.point);
                if (hit.rigidbody)
                {
                    AsteroidScript asteroid = hit.collider.GetComponent<AsteroidScript>();
                    if (asteroid)
                    {
                        asteroid.Kill();
                    }


                    EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
                    if (enemy)
                    {
                        enemy.TakeDamage(10f);
                    }

                    if (hit.collider.gameObject.name.Contains("Asteroid"))
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point); 
                        GameObject asteroidHit = GameObject.Find(hit.collider.gameObject.name);
                        asteroidHit.GetComponent<AsteroidScript>().Hit = true;
                    }
                    else if (hit.collider.gameObject.name.Contains("Enemy"))
                    {
                        hit.rigidbody.AddForceAtPosition(transform.forward * 10, hit.point);
                        GameObject enemyHit = GameObject.Find(hit.collider.gameObject.name);
                        enemyHit.GetComponent<EnemyScript>().Hit = true;
                    }
                }
            }
            else
                line.SetPosition(1, ray.GetPoint(LaserDistance));

            yield return null;
        }

        line.enabled = false;
    }
}

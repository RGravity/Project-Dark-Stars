using UnityEngine;
using System.Collections;

public class EnemyCollision : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerController>())
        {
            Debug.Log("SUCCESS");
            GameObject.FindObjectOfType<PlayerController>().Hit = true;
        }
    }
}

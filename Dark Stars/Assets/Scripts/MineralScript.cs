using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MineralScript : MonoBehaviour
{
    [SerializeField]
    private int scoreAmount;

    [SerializeField]
    private int amountOfXenonite;
    
    [SerializeField]
    private int amountOfHelionite;
    
    [SerializeField]
    private int amountOfArgonite;
    
    [SerializeField]
    private int amountOfNeonite;

    [SerializeField]
    private float cristalAmount;

    void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController == null)
            return;

        playerController.Score += scoreAmount;
        playerController.AmountOfXenonite += amountOfXenonite;
        playerController.AmountOfHelionite += amountOfHelionite;
        playerController.AmountOfArgonite += amountOfArgonite;
        playerController.AmountOfNeonite += amountOfNeonite;
        playerController.AmountCrystalFill += cristalAmount;

        Destroy(gameObject);
    }
}

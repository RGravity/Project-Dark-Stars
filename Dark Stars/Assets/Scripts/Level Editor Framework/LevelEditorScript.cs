using UnityEngine;
using System.Collections;

public class LevelEditorScript : MonoBehaviour {

    LevelEditorMain main;
    private int _amountOfLevels = 2;
    public int amountOfLevels { get { return _amountOfLevels; } }

    void Start()
    {
        main = GameObject.Find("Main").GetComponent<LevelEditorMain>();
    }
    

    public void level1()
    {
        
    }

}

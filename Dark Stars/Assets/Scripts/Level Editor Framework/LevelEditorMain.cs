using UnityEngine;
using System.Collections.Generic;

public class LevelEditorMain : MonoBehaviour {

    MainScript mainScript;
    LevelEditorScript LevelScript;
    private int level = 1;
    private int MaxLevels;

	// Use this for initialization
	void Start () {
        mainScript = GameObject.Find("Main").GetComponent<MainScript>();
        LevelScript = GameObject.Find("Main").GetComponent<LevelEditorScript>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void spawnAsteroids(int amount)
    {
        mainScript.SpawnAsteroids(amount);
    }

    public void spawnEnemy(EnumEnemyShipType shiptype, int amount)
    {
        mainScript.SpawnEnemyShips(shiptype, amount);
    }

    public void setMinerals(bool Xenonite1, bool Helionite2, bool Argonite5, bool Neonite10)
    {
        List<EnumMinerals> MineralList = new List<EnumMinerals>();
        if (Xenonite1)
        {
            MineralList.Add(EnumMinerals.xenonite1);
        }
        if (Helionite2)
        {
            MineralList.Add(EnumMinerals.helionite2);
        }
        if (Argonite5)
        {
            MineralList.Add(EnumMinerals.argonite5);
        }
        if (Neonite10)
        {
            MineralList.Add(EnumMinerals.neonite10);
        }

        mainScript.MineralsAllowed = MineralList;
    }


    
    
}

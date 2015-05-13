using UnityEngine;
using System.Collections.Generic;

public class LevelEditorMain : MonoBehaviour {

    MainScript mainScript;
    LevelEditorScript LevelScript;
    private int level = 1;
    private bool _nextlevelToLoad = true;
    public bool NextLevelToLoad { set { _nextlevelToLoad = value; } }
    private int MaxLevels;

	// Use this for initialization
	void Start () {
        mainScript = GameObject.Find("Main").GetComponent<MainScript>();
        LevelScript = GameObject.Find("Main").GetComponent<LevelEditorScript>();
        MaxLevels = LevelScript.amountOfLevels;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (level <= MaxLevels)
        {
            if (_nextlevelToLoad)
            {
                LevelScript.levels(level);
                level++;
                _nextlevelToLoad = false;
            }
        }
	}

    public void spawnAsteroids(int amount)
    {
        mainScript.SpawnAsteroids(amount);
    }

    public void spawnEnemy(EnumEnemyShipType shiptype)
    {
        mainScript.SpawnEnemyShips(shiptype, 1);
    }

    public void spawnEnemies(EnumEnemyShipType shiptype, int amount)
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

    public void setMaxDistanceToPlayer(int distance)
    {
        mainScript.MaxDistanceToPlayer = distance;
        GameObject.Find("Spaceship").GetComponentInChildren<Camera>().farClipPlane = distance + 100;
    }

    public void setMinDistanceToPlayer(int distance)
    {
        mainScript.MinDistanceToPlayer = distance;
    }

    
    
}

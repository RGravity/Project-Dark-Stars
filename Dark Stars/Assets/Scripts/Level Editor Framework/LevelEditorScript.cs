using UnityEngine;
using System.Collections;

public class LevelEditorScript : MonoBehaviour {

    LevelEditorMain main;
    private int _amountOfLevels = 0;
    public int amountOfLevels { get { return _amountOfLevels; } }

    void Start()
    {
        main = GameObject.Find("Main").GetComponent<LevelEditorMain>();

        _amountOfLevels = 1;
    }
    

    public void levels(int level)
    {
        switch(level){
            case 1: //level 1
                main.setMinDistanceToPlayer(30);
                main.setMaxDistanceToPlayer(350);
                main.spawnAsteroids(100);
                main.spawnEnemy(EnumEnemyShipType.speeder);
                main.spawnEnemies(EnumEnemyShipType.assailant, 1);
                main.spawnEnemy(EnumEnemyShipType.bruiser);
                main.setMinerals(true, true, true, true);
                //   -----   skybox    -----
                //main.changeSkybox(EnumSkybox.skyboxRedBlack);
                main.changeRandomSkybox();
                break;
            case 2: //level 2
                break;
            case 3: //level 3
                break;
            case 4: //level 4
                break;
            case 5: //level 5
                break;
            case 6: //level 6
                break;
            case 7: //level 7
                break;
            case 8: //level 8
                break;
            case 9: //level 9
                break;
            case 10: //level 10
                break;
            default:
                break;
        }


        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;
    private PlanetManager planetManager;
    private PlayerManager playerManager;
    private ToolManager toolManager;
    void Awake(){
        toolManager = GetComponent<ToolManager>();
        planetManager = GetComponent<PlanetManager>();
        enemyManager = GetComponent<EnemyManager>();
        playerManager = GetComponent<PlayerManager>();
        planetManager.StvoriPlanet(4,planetManager.planet_list,planetManager.enemyPlanet);
        enemyManager.SpawnEnemy();
        toolManager.StvoriAlat();
        playerManager.StvoriIgraca(playerManager.player);
    }
}

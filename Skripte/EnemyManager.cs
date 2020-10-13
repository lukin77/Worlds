using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject slime;
    private GameObject[] planet_list1;
    public void SpawnEnemy(){
        planet_list1 = GameObject.FindGameObjectsWithTag("Planet");
        foreach(GameObject p in planet_list1){
            if(p.name != "Planet0"){
                Instantiate(slime,p.transform.position,Quaternion.identity,p.transform);
                slime.GetComponent<Enemy>().SetPlanet(p);
            }
        }           
    }
}

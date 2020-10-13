using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public GameObject nut;
    private GameObject[] AllPlanets;
    public GameObject spaceship;

    public void StvoriAlat()
    {
        AllPlanets = GameObject.FindGameObjectsWithTag("Planet");
        foreach(GameObject planet in AllPlanets){
            Vector3 dirAngle = Random.onUnitSphere * (((planet.transform.localScale.x)/2) + 3) ;
            Instantiate(nut, planet.transform.position + dirAngle , Quaternion.identity);
            if(planet.name == "Planet0"){
                Instantiate(spaceship, planet.transform.position + new Vector3(0,planet.transform.localScale.y/2 + 5f,0), Quaternion.Euler(0,0,-27f));
            }     
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEnemy : MonoBehaviour
{
    private GameObject planet;
    private GameObject waypoint;

    void Start(){
        planet = this.gameObject;
        StvoriWaypoint();
    }

    private void StvoriWaypoint(){
        for (int i = 0; i < 10; i++){
            waypoint = new GameObject();
            Vector3 waypointAngle = Random.onUnitSphere * (((planet.transform.localScale.x)/2) + 3) ;
            waypoint.transform.position = planet.transform.position + waypointAngle;
            waypoint.transform.parent = transform;
            waypoint.name  = "Waypoint" + StripPlanetName();
            waypoint.tag  = "Waypoint" + StripPlanetName();
        } 
    }

   
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Player"){
           playerPositionOnPlanet = collision.transform.position;
        }
    }
    private Vector3 playerPositionOnPlanet = Vector3.zero;
    public Vector3 PlayerPlanetPosition(){
        return playerPositionOnPlanet;
    }

    public string StripPlanetName(){
        
        string num = string.Empty;
        string namePlanet = planet.name;
        for(int i=0; i<namePlanet.Length; i++){
            if(char.IsDigit(namePlanet[i])){
                num += namePlanet[i];
            }
        }
        return num;
    }

   
}

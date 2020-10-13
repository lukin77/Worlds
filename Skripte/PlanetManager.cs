using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject enemyPlanet;
    public List<GameObject> planet_list;
    public Material[] materials;

    public void StvoriPlanet(int num_planets, List<GameObject> planet_list, GameObject planetPrefab){
        planet_list = new List<GameObject>(num_planets+1);
        for(int i = 0;i <= num_planets; i++)
        {
            GameObject new_planet = Instantiate(planetPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            planet_list.Add(new_planet); 
            planet_list[i].gameObject.name = "Planet" + i;
            planet_list[i].gameObject.tag = "Planet";
            planet_list[i].GetComponent<MeshRenderer>().material = materials[Random.Range(0,materials.Length)];
        }
        for (int i = 0; i < num_planets; i++){
            float col = planet_list[i].GetComponent<SphereCollider>().radius;
            float size = planet_list[i].transform.localScale.x * col;
            Vector3 planetAngle = (Random.onUnitSphere  * (planet_list[i].transform.localScale.x + size));
            planet_list[i+1].transform.position = planet_list[i].transform.position + planetAngle;
        } 
    }
}

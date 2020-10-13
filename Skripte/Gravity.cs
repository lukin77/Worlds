using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GameObject Object;
	private GameObject Planet;
    public Rigidbody rb;
	public Vector3 GroundNormal;
	float distanceToGround;
    public float gravityForce = 25f;
    public bool OnGround = false;
    

    void Start()
    {   
        rb = Object.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

	public void Gravitation(GameObject Object, GameObject Planet){
		Vector3 GravityDirection = (Object.transform.position - Planet.transform.position).normalized;
        if(OnGround == false){
            rb.AddForce(GravityDirection * -gravityForce);
            Quaternion toRotation = Quaternion.FromToRotation(Object.transform.up, GroundNormal) * Object.transform.rotation;
		    Object.transform.rotation = Quaternion.Lerp(Object.transform.rotation,toRotation,0.5f);
                    
        }
    }

	public void GroundControl(GameObject Object){
		RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(Object.transform.position, -Object.transform.up, out hit, 15f)){
            if(hit.collider.tag == "Planet"){
                distanceToGround = hit.distance;
                GroundNormal = hit.normal;

                if (distanceToGround < 2.0f){
                    OnGround = true;
                }
                else{
                    OnGround = false;
                }  
            }
            else{
                OnGround = false;
            }
        }	
    }

    public Vector3 GetGroundNormal(){
        return this.GroundNormal;
    }
}

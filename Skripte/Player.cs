using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {

	//planet
	private GameObject Planet;
	public Gravity gravity;
	//movment
	private Animator anim;
	private Rigidbody rbody;
	public float velocitySpeed;
	public float speed = 6f;
	public float turnSpeed = 150f;
	public float JumpHeight = 20f;
	public bool OnGround;
	
	public PlayerManager playerManager; //setPlayersprefs
	private int nutsColected = 0;
	private int timesJumped = 0;

	//healthbar i to
	public int max_health = 5;
	private int current_health;
	private GameObject healthbarobject;
	private Healthbar healthbar;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		rbody = GetComponent<Rigidbody>();
		gravity = GetComponent<Gravity>();
		healthbarobject =  GameObject.FindGameObjectWithTag("HealthBar");
		healthbar = healthbarobject.GetComponent<Healthbar>();
		current_health = max_health;
		healthbar.SetMaxHealth(max_health);
	}	

	void FixedUpdate (){
		gravity.GroundControl(gameObject);
		if(Planet == null){
			return;
		}else{
			gravity.Gravitation(this.gameObject,Planet);
		}
	}

	void Update(){
		OnGround = gravity.OnGround;
		anim.ResetTrigger("JumpStart");
		if(OnGround){
			if (Input.GetKey ("w") || Input.GetKey ("s") ) {
				anim.SetInteger ("AnimationPar", 1);
				float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
				transform.Translate(0, 0, z);
			}else{
				anim.SetInteger ("AnimationPar", 0);
			}

			if (Input.GetKey("d") || Input.GetKey("a") )
			{
				float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
				transform.Rotate(0, x * turnSpeed , 0);

			}
				//float yVel = rbody.velocity.y;
    			//Vector3 newVel = transform.forward * Input.GetAxis("Vertical")  + transform.right * Input.GetAxis("Horizontal");
    			//newVel = newVel.normalized * velocitySpeed;
    		Vector3 newVel = (transform.up + transform.forward).normalized * velocitySpeed;
				//newVel.y = yVel;
     			//rbody.velocity = newVel;
			if(Input.GetKeyDown("space")){
			//anim.ResetTrigger("JumpStart");
			anim.SetTrigger("JumpStart");
			rbody.velocity = newVel;
			rbody.AddForce(transform.up * JumpHeight * Time.deltaTime,ForceMode.Impulse);
			//rbody.AddRelativeForce(newVel * Time.fixedDeltaTime, ForceMode.Impulse);
			timesJumped++;
			}
		}
		anim.SetBool("IsJumping", !OnGround);
		anim.SetBool("OnGround", OnGround);
	}

	private void OnTriggerEnter(Collider collider){
			
		if(collider.transform.tag == "Planet" && !OnGround){
			Planet = collider.transform.gameObject;
			SetPlanet(Planet);
		
			Vector3 GravityDirection = (transform.position - Planet.transform.position).normalized;
			rbody.AddForce(GravityDirection * gravity.gravityForce);
			Quaternion toRotation = Quaternion.FromToRotation(transform.up, GravityDirection)*transform.rotation;
			transform.rotation = toRotation;
			
			//rbody.velocity = Vector3.zero;				
			
		}
	}

	private void OnCollisionEnter(Collision collision){
        
        if(collision.gameObject.tag == "Enemy"){
			TakeDamage(1);
			rbody.AddForce(transform.up + transform.forward * 20,ForceMode.Impulse);
        }

		if(collision.transform.tag == "Tool"){
			nutsColected++;
			Destroy(collision.transform.gameObject);
		}

		if(collision.transform.tag == "Planet" && OnGround){
			Planet = collision.transform.gameObject;
			SetPlanet(Planet);

			Vector3 GravityDirection = (transform.position - Planet.transform.position).normalized;
			rbody.AddForce(GravityDirection * gravity.gravityForce);
			Quaternion toRotation = Quaternion.FromToRotation(transform.up, GravityDirection)*transform.rotation;
			transform.rotation = toRotation;
			
			//rbody.velocity = Vector3.zero;				
			
		}

    }

	public int GetNutsCollected(){
		return nutsColected;
	}

	public void setPlayersPrefs(){
		PlayerPrefs.SetInt("Jump",timesJumped);
		PlayerPrefs.SetInt("Nuts", nutsColected);
		PlayerPrefs.Save();
	}

	public void SetPlanet(GameObject planet){
        this.Planet = planet;
    }

	public GameObject GetPlanet(){
       	return Planet;
    }

	void TakeDamage(int damage){
		current_health -= damage;
		healthbar.SetHealthUI(current_health);
	}
	
	public int GetHealth(){
		return this.current_health;
	}

	

}	

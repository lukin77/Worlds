using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    private GameObject planet;
    private Rigidbody myRigidbody;
    private Animator myAnimator;
    public Gravity myGravity;
    public float speed;
    GameObject[] waypoints;
    List<Vector3> vectorList;
    PlanetEnemy planetEnemy;

    int currentWP;
    bool IsPlayerInMyArea = false;

    private void SetCurrentWP(int value){
        this.currentWP = value;
    }

    public string StripPlanetName(){
        
        string num = string.Empty;
        string namePlanet = transform.parent.name;
        for(int i=0; i<namePlanet.Length; i++){
            if(char.IsDigit(namePlanet[i])){
                num += namePlanet[i];
            }
        }
        return num;
    }

    // Start is called before the first frame update
    private void Start()
    {   
        InvokeRepeating("SetRandomJumpAnimation",1.0f,5.0f);
        this.planetEnemy = GetComponentInParent<PlanetEnemy>();
        this.vectorList = new List<Vector3>();
        myAnimator = GetComponent<Animator>();
        //player = PlayerManager.instance.player;
        player = GameObject.FindGameObjectWithTag("Player");
        myRigidbody = GetComponent<Rigidbody>();

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint" + StripPlanetName()); // + GetYourPlanetNumber());
        foreach(GameObject a in waypoints){
            vectorList.Add(a.transform.position);
            //Debug.Log(a.transform.position);    
        }
    }

   
    public void MoveTo(Vector3 targetPosition){
        
        transform.position  = Vector3.MoveTowards(transform.position,targetPosition,speed * Time.fixedDeltaTime);
        myAnimator.SetBool("Walk" , true);
        
    }


    public void Patrol(){
        if(vectorList.Count == 0) return;
        if(Vector3.Distance(this.vectorList[currentWP], transform.position) < 3.0f){
            //Debug.Log("Velicina vekora manja");
            this.currentWP = Random.Range(0,this.vectorList.Count);
            //SetCurrentWP(currentWP++);
            /*
            currentWP++;
            */
            if(this.currentWP >= this.vectorList.Count-1){
                this.currentWP = Random.Range(0,vectorList.Count);
            }
            
        }else{
            MoveTo(this.vectorList[this.currentWP]);
            //Debug.Log("Vectro3 current: " + vectorList[currentWP]);
            Debug.Log("Current: " + currentWP);
            Debug.Log("Patroliram ovjde: " + vectorList[currentWP]);
        }
    }
    
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            this.IsPlayerInMyArea = true;
        }
    }
    private void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            this.IsPlayerInMyArea = false;
        }
    }
    private void OnCollisionEnter(Collision collision){
        
        if(collision.gameObject.tag == "Player"){
            this.myAnimator.SetBool("Attack" , true);
            //play sound
            
        }
    }

    private void OnCollisionStay(Collision collision){
        if(collision.gameObject.tag == "Planet"){
            this.myAnimator.SetBool("Attack" , false);
        }
    }
    private void OnCollisionExit(Collision collision){
        
        if(collision.gameObject.tag == "Player"){
            
        }

    }
    

    void FixedUpdate()
    {
		this.myGravity.GroundControl(this.gameObject);
        if(planet == null) return;
        {
            this.myGravity.Gravitation(this.gameObject, this.planet);
        }
	}
    // Update is called once per frame
    void Update()
    {
        if(this.IsPlayerInMyArea){
            this.transform.LookAt(player.transform.position);
            MoveTo(player.transform.position);            
            Rotation();    
        }else{
            Patrol();
            AddPositionToList();
            Rotation();
        }

    }

    public void SetRandomJumpAnimation(){
        this.myAnimator.SetBool("RandomJump",  Random.Range(1,10) >= 5);
    }

    public void Rotation(){
        Quaternion toRotation = Quaternion.FromToRotation(this.transform.up, this.myGravity.GetGroundNormal()) * this.transform.rotation;
        this.transform.rotation = toRotation;
    }

    public void SetPlanet(GameObject new_planet){
        this.planet = new_planet;
    }

    public void AddPositionToList(){
        if(vectorList.Count == 0) return;
        else{
            if(Vector3.Distance(vectorList[vectorList.Count-1], planetEnemy.PlayerPlanetPosition()) > 5f){
                if(planetEnemy.PlayerPlanetPosition() != Vector3.zero){
                    if(!vectorList.Contains(planetEnemy.PlayerPlanetPosition())){
                        vectorList.Add(planetEnemy.PlayerPlanetPosition());
                        currentWP = vectorList.Count - 1; 
                    }
                }
            }    
        }
    }
    
}

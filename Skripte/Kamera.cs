using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    private Vector3 currentVelocity = Vector3.zero;
    public float MovementSmoothingValue = 25f;
    public float RotationSmoothingValue = 5.0f;
    private Vector3 moveVector;
    private float mouseWheel;
    public float FollowDistance = 30.0f;
    public float MaxFollowDistance = 100.0f;
    public float MinFollowDistance = 2.0f;

    public Transform player;
    public GameObject planet;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void LateUpdate()
    {
        planet = player.GetComponent<Player>().GetPlanet();
        FollowPlayer();
        
    }

    void FollowPlayer()
        {
            if(Input.GetMouseButton(0)){
                transform.RotateAround(player.transform.position,player.transform.up, Input.GetAxis("Mouse X"));
                transform.RotateAround(player.transform.position,player.transform.right, Input.GetAxis("Mouse Y"));
            }else{
                Vector3 direction = (player.position - planet.transform.position).normalized;
                Vector3 targetPosition = player.position + direction * FollowDistance;
                
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, MovementSmoothingValue * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position, Vector3.up ), RotationSmoothingValue * Time.deltaTime);
            }
            mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseWheel < -0.01f || mouseWheel > 0.01f)
            {
                FollowDistance -= mouseWheel * 15.0f;
                FollowDistance = Mathf.Clamp(FollowDistance, MinFollowDistance, MaxFollowDistance);
            }
        }

}

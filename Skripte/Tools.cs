using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    Transform myTransform;


    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        posOffset = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up * degreesPerSecond * Time.deltaTime,Space.Self);
        transform.Rotate(transform.right * degreesPerSecond * Time.deltaTime,Space.Self);
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
 
        transform.position = tempPos;
    }

    private void OnCollisionEnter(Collision collision){
        
		if(collision.transform.tag == "Player"){
			Destroy(gameObject);
		}

    }
}

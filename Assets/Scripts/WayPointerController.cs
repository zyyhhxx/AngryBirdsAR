using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class WayPointerController : MonoBehaviour
{
	// Overview: the life cycle of a waypointer
    // Start is called before the first frame update
    //public GameObject map;
    // method 2: 
    private float lifttime = 3.0f;
    bool processing = false;
    private GameObject camera;
   
    void Awake(){
        camera = GameObject.Find("ARCamera").gameObject;
    }
    // Update is called once per frame 
    void Update()
    {   
        /*
    	// method 1: when the waypointer has out of screen, then destory
        Vector3 viewPos = camera.WorldToViewportPoint(transform.position);

        if (viewPos.x >= 1.0F || viewPos.x <= 0.0F){
            Destroy(gameObject);
        }
        else if(viewPos.y >= 1.0F || viewPos.y <= 0.0F){
            Destroy(gameObject);
        }*/

        // method 2: timing decides destory 
        
        if(!processing){
            StartCoroutine(ExampleCoroutine());
        }
        
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(lifttime);
        processing = true;
        Destroy(gameObject);
    }
}





/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


transform.LookAt(target.transform, Vector3.up);
transform.Rotate(0.0f, -180.0f, 0.0f, Space.Self);

float step =  speed * Time.deltaTime; // calculate distance to move
transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

// Check if the position of the cube and sphere are approximately equal.

if (Vector3.Distance(transform.position, target.transform.position) < 0.0001f)
{
    return;
}    
*/
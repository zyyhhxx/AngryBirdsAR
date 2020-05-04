using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
public class MapController : MonoBehaviour
{	
	// a set of viewPoints of image targets
	public GameObject viewPoint1;
	public GameObject viewPoint2;
	public GameObject viewPoint3;
	
	public GameObject sceneObject1;
	public GameObject sceneObject2;
	public GameObject sceneObject3;
	

	public Text testInfo;

    // the gameobject of the current player
    // we can set this object to the singleshot object
	public GameObject currentObject;

	public GameObject arrowPointerPrefab;

	//private Transform target;
	// when flag == 1: target is the playerGround1
	// when flag == 2: target is the playerGround2
	// when falg == 3: target is the playerGround3
	private int flag = 1;
	// store a set of image target's position and orientation
  // Start is called before the first frame update
	//HashSet<GameObject> wayPointers = new HashSet<GameObject>();
	private int properWayPointeNumber = 10;
	private float wayPointerStep;
	bool canDraw = true;


	// the current recognized status: 
	private bool  firstSceneFound = false;
  private bool secondSceneFound = false;
  private bool thirdSceneFound = false;

  // the reference standard value of step
  private float step = 0.1f;

  private float lifttime = 3.0f;
  void Start()
  {
      // default: set the target as the bestView points of the first playGroud;
      //drawTrail(viewPoint1.transform);
      //drawTrail(viewPoint2.transform);
      //drawTrail(viewPoint3.transform);
  }

  // Update is called once per frame
  void Update()
  {	
    
    if(flag == 1){
      testInfo.text = "target at the first scene";
    }
    else if(flag == 2){
      testInfo.text = "target at the second scene";
    }
    else if(flag == 3){
      testInfo.text = "target at the third scene";
    }
    


  	if(canDraw && firstSceneFound && flag == 1){
  		canDraw = false;
      testInfo.text = "have draw the first scene";
  		drawTrail(viewPoint1.transform);
  	}
  	else if(canDraw && secondSceneFound && flag == 2){
  		canDraw = false;
      testInfo.text = "have draw the second scene";
      drawTrail(viewPoint2.transform);
  	}
  	else if(canDraw && thirdSceneFound && flag == 3){
  		canDraw = false;
      testInfo.text = "have draw the third scene";
  		drawTrail(viewPoint3.transform);
  	} 	
  }

  void drawTrail(Transform target){
  	// draw a set of waypointer between the current object and the target points with a proper step
  	// using rotateTowards and moveTowards: compute the proper position
  	// Determine which direction to rotate towards
    
  	//properWayPointeNumber = (int)(Vector3.Distance(target.forward, transform.forward) / step);
    properWayPointeNumber = (int)(Vector3.Distance(target.position, transform.position) / step);
  	float rotateStep = Vector3.Distance(target.forward, transform.forward) / properWayPointeNumber;
  	float positionStep =  Vector3.Distance(target.position, transform.position) / properWayPointeNumber;
  	GameObject tempObject = currentObject;
  	Vector3 tempPosition = new Vector3(0.0f, 0.0f, 0.0f);

  	for(int i=0; i< properWayPointeNumber; i++){
  		// The step size is equal to speed times frame time.
        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(tempObject.transform.forward, target.forward, rotateStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        Quaternion tempRotation = Quaternion.LookRotation(newDirection);
        // distributed by position
        // Move our position a step closer to the target.
        tempPosition = Vector3.MoveTowards(tempObject.transform.position, target.position, positionStep);
      	GameObject tempWayPointer = (GameObject)Instantiate(arrowPointerPrefab, tempPosition, tempRotation);
      	tempObject = tempWayPointer;
        // method 2: lookAt: rotation
        tempObject.transform.LookAt(target);
    }
  



    StartCoroutine(ExampleCoroutine());
  }

 	// clicked, move to the next playground
 	public void OnClickPass(){
 		if(flag == 1){
 			flag = 2;
 			canDraw = true;

 		}
 		else if(flag == 2){
 			flag = 3;
 			canDraw = true;
 		}
 	}

// clicked, move to the previous playground
 	public void OnClickBack(){
 		if(flag == 2){
 			flag = 1;
 			canDraw = true;
 		}
 		else if(flag ==3){
 			flag = 2;
 			canDraw = true;
 		}
 	}


 	// recognize if scenes have been recognized in the field of camera view
 	public void FoundTarget1(){
      firstSceneFound = true;
  }
  public void LostTarget1(){
      firstSceneFound = false;
  }

  public void FoundTarget2(){
      secondSceneFound = true;
  }
  public void LostTarget2(){
      secondSceneFound = false;
  }

  public void FoundTarget3(){
      thirdSceneFound = true;
  }
  public void LostTarget3(){
      thirdSceneFound= false;
  }


  //every fixed frequence, update the wayfinding pointers
  IEnumerator ExampleCoroutine()
  {
      //yield on a new YieldInstruction that waits for 5 seconds.
      yield return new WaitForSeconds(lifttime);
      canDraw = true;
  }



}


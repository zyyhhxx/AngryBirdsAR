﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
public class MapController : MonoBehaviour
{	
	// a set of viewPoints of image targets
	public GameObject viewPoint1;
	public GameObject viewPoint2;
	
	//public GameObject sceneObject1;
	//public GameObject sceneObject2;
	
	public Text testInfo;

    // the gameobject of the current player
    // we can set this object to the singleshot object
	//public GameObject currentObject;

	public GameObject arrowPointerPrefab;

	//private Transform target;
	// when flag == 1: target is the playerGround1
	// when flag == 2: target is the playerGround2
	private int flag = 1;
	// store a set of image target's position and orientation
  	// Start is called before the first frame update
	private int properWayPointeNumber = 10;
	private float wayPointerStep;
	bool canDraw = true;

	// the current recognized status: 
	private bool  firstSceneFound = false;
  	private bool secondSceneFound = false;

  	// the reference standard value of step
	private float step = 0.1f;
	private float lifttime = 3.0f;

	//HashSet<GameObject> wayPointers = new HashSet<GameObject>();
	// the first viewPoint1
	private Transform transform1;
	private Transform transform2;


	private bool allTargetsRegistered = false;


	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{	
		/*
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
		*/


		// test the currentobject transform changing
		testInfo.text = "the current position of the first scene" + viewPoint2.transform.position.ToString(); 
		// all targets are fixed:
		if(firstSceneFound && secondSceneFound){
			// record the relative position
			transform1 = viewPoint1.transform;
			transform2 = viewPoint2.transform;
			allTargetsRegistered = true;

		}
		else if(canDraw && firstSceneFound && !secondSceneFound && allTargetsRegistered){
			// arrow pointing from the firstScene to secondScene
			canDraw = false;
			drawTrail(transform1, transform2);
			testInfo.text = "pointing from target 1 to target 2";
		}
		else if(canDraw &&!firstSceneFound && secondSceneFound && allTargetsRegistered){
			// arrow pointing form the secondScene to the firstScene
			canDraw = false;
			drawTrail(transform2, transform1);
			testInfo.text = "pointing from target 2 to target 1";
		}

	}

	void drawTrail(Transform start, Transform target){
		// draw a set of waypointer between the current object and the target points with a proper step
		// using rotateTowards and moveTowards: compute the proper position
		// Determine which direction to rotate towards

		//properWayPointeNumber = (int)(Vector3.Distance(target.forward, transform.forward) / step);
		properWayPointeNumber = (int)(Vector3.Distance(target.position, start.position) / step);
		float rotateStep = Vector3.Distance(target.forward, start.forward) / properWayPointeNumber;
		float positionStep =  Vector3.Distance(target.position, start.position) / properWayPointeNumber;
		Transform tempObject = start;
		Vector3 tempPosition = new Vector3(0.0f, 0.0f, 0.0f);

		for(int i=0; i< properWayPointeNumber; i++){
			// The step size is equal to speed times frame time.
	    	// Rotate the forward vector towards the target direction by one step
		    Vector3 newDirection = Vector3.RotateTowards(tempObject.forward, target.forward, rotateStep, 0.0f);

		    // Calculate a rotation a step closer to the target and applies rotation to this object
		    Quaternion tempRotation = Quaternion.LookRotation(newDirection);
		    // distributed by position
		    // Move our position a step closer to the target.
		    tempPosition = Vector3.MoveTowards(tempObject.position, target.position, positionStep);
		  	GameObject tempWayPointer = (GameObject)Instantiate(arrowPointerPrefab, tempPosition, tempRotation);
		  	tempObject = tempWayPointer.transform;
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
	}

	// clicked, move to the previous playground
	public void OnClickBack(){
		if(flag == 2){
			flag = 1;
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

	//every fixed frequence, update the wayfinding pointers
	IEnumerator ExampleCoroutine()
	{
	  //yield on a new YieldInstruction that waits for 5 seconds.
	  yield return new WaitForSeconds(lifttime);
	  canDraw = true;
	}



}


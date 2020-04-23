using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class wallFindController : MonoBehaviour
{
    //public GameObject wayPointer;
    public GameObject wayPointerPrefab;
    private bool  firstSceneFound = false;
    private bool secondSceneFound = false;
    public GameObject currentTarget1;
    public GameObject currentTarget2;
    public GameObject wayPointerPoint;
    private GameObject newWayPointer;
    // Angular speed in radians per sec.
    private float speed = 1.0f;
    //public GameObject player;
    // Start is called before the first frame update
    void Awake(){
        // 
        //wayPointer.SetActive(false);
    }
  
    // Update is called once per frame
    void Update()
    {
        // if all two imageTarget Founded, then show the arrow
        // setActive or initiate one new arrow and set the direction
        if(firstSceneFound && secondSceneFound){
            // method 1: by SetActive
            //wayPointer.SetActive(true);

            // method 2: initiate one new arrow and set the direction
            // need a approporiate position such the player postion and next scenen position
            // using transform.LookAt(next.transform) to update the direction

            // when entering the next scene, Destory this wayPointer
            // then iteratively
            if(newWayPointer == null){
                newWayPointer = (GameObject)Instantiate(wayPointerPrefab, wayPointerPoint.transform.position, wayPointerPoint.transform.rotation);
                newWayPointer.transform.SetParent(currentTarget1.transform);
            }
            else{
                
                // method 1
                newWayPointer.transform.LookAt(currentTarget2.transform);

                // method 2: rotatetowards
                /*
                 Vector3 targetDirection = currentTarget2.transform.position - newWayPointer.transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(newWayPointer.transform.forward, targetDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                //Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                newWayPointer.transform.rotation = Quaternion.LookRotation(newDirection);
                */

            }
        }
    }

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private float setup_time = 0.5f;
	private float start_time;
    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
    	if (Time.time > start_time + setup_time){
        	Destroy(this.gameObject);
    	}
    }
}

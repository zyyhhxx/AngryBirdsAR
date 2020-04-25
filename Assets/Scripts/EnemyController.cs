using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private float setup_time = 0.2f;
	private float start_time;

    private float HP = 100;
    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
    	if (Time.time > start_time + setup_time){
            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player")){
                GetDamage(100);
            }else{
                // Debug.Log("Hit something?");
                GetDamage(50);
            }
    	}
    }

    void GetDamage(float damage){
        HP -= damage;
        if (HP <= 0){
            Destroy(this.gameObject);
        }
        // Debug.Log("HP: " + HP);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private float setup_time = 0.2f;
	private float start_time;
    private float death_time = 0.0f;

    public ParticleSystem effect;
    public float HP = 100;

    // Start is called before the first frame update
    void Start()
    {
        //effect.Stop();
        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (death_time != 0.0f && Time.time > death_time + 0.5f){
            Debug.Log("death:" + death_time);
            Destroy(this.gameObject);
        }
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

    public void GetDamage(float damage){
        HP -= damage;
        if (HP <= 0){
            //effect.Play();
            //effect.enableEmission = true;
            death_time = Time.time;
        }
        // Debug.Log("HP: " + HP);
    }
}

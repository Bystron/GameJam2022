using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class EnemyGeneric : MonoBehaviour
{
    public int life;
    public int speed;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    Movement();
        //                          ENEMY DEATH
    if(life<=0) {Death();}
    }

void Death(){
    //                              ENEMY DEATH
    //Debug.Log("Oh no! I have Died!");
    Destroy(gameObject);
}

//                                  BULLET COLLIDER
 private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.tag == "Bullet")
        {
         //   Debug.Log("Oh no! I have been Shot");
            life--;

        }
    }

    void Movement(){
        //transform.position = Vector2.MoveTowards(transform.position,Time.deltaTime*speed);
    }

}

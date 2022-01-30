using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyKamikaze : EnemyGeneric
{
	private GameObject playerObject;
	private Vector2 target;
	private Vector2 position;
    // Start is called before the first frame update
    public override void Start()
    {
		playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
		if(!isSpawning || dead)return;
		else
		{
			target = playerObject.transform.position;
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards(transform.position, target, step); 
		}
    }
//                                  BULLET COLLIDER
}

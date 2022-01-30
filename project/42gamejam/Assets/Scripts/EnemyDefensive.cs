using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyDefensive : EnemyGeneric
{
	//private GameObject playerObject;
    public LPBullet enemyBullet;
    private Tramsform parentBE;
    Transform PlayerPos;
	private Vector3 target;
    bool landingPhase,formationPhaseAxis,readyToShoot;
    float random1,random2;
    int unUnoxD,unoRandomjaja;
    public LPBullet[] eBulletsArray;

    
	// Start is called before the first frame update
    void Start()
    {
        //                          TAKING PLAYER AND ENEMY POSITIONS
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        parentBE = GameObject.FindGameObjectWithTag("Respawn").GetComponent;
        //                          SPAWNS OWN BULLETS
        CreateBullets();
        //this.gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 3,0);

        readyToShoot=true;
        landingPhase=true;
        unUnoxD=(Random.Range(0,2));
        if(unUnoxD==1)
        {  
            unoRandomjaja=1;formationPhaseAxis=!formationPhaseAxis;}else{unoRandomjaja=-1;
        }
		random1=(Random.Range(unoRandomjaja,0));
        random2=(Random.Range(0f,-1f));
        //                          MOVEMENT        The enemy falls from the sky 
        target = new Vector3 (random1,random2,0f);
        
    }

    // Update is called once per frame
    public void Update()
    {//                             MOVEMENT        Constant movement
        if(!isSpawning)return;
        else
        
        transform.Translate(target*speed*Time.deltaTime, Space.World);
        //                              MOVEMENT        The enemy falls from the sky 
        if(landingPhase==false)
        {
            //                        MOVEMEMNT       The enemy gets in "formation"
            if(formationPhaseAxis==false)
            {
                target = new Vector3(-1,0);
            }
            else
            {
                target = new Vector3(1,0);
            }
        }
        //                          ENEMY DEATH
		if(life<=0) 
		{
			Death();
		}
        //                          ENEMY SHOOTING
      if (PlayerPos != null)
        {
            if ((int) gameObject.transform.position.x == (int) PlayerPos.position.x && readyToShoot)
            {
                for(int i = 0; i < eBulletsArray.Length; i++)
                {
                    if(!eBulletsArray[i].isMoving)
                    {
                        eBulletsArray[i].transform.position = transform.position;
                        eBulletsArray[i].Shoot(new Vector3(0, -1, 0));
                        eBulletsArray[i].isMoving = true;
                        readyToShoot = false;
                        StartCoroutine(ShootDelay());
                        Debug.Log("Enemy Shooting");
                        
                    }
                }
                readyToShoot = false;   
                StartCoroutine(ShootDelay());
                
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bound_Enemy")
        {
            landingPhase=false;
            
            formationPhaseAxis=!formationPhaseAxis;
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(3f);
        
    }

    private void CreateBullets()
    {
        Debug.Log("Bro wtf");
        for(int i = 0; i < eBulletsArray.Length; i++)
        {
            eBulletsArray[i] = Instantiate(enemyBullet, new Vector3(0,0,0), Quaternion.identity, parentBE);
            eBulletsArray[i].transform.localPosition = new Vector3(i,-5f,0);
        }
        
    }
}

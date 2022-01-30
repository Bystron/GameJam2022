using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnerScript : MonoBehaviour
{

    public EnemyDefensive defEnemyWhite;
    public EnemyKamikaze kamiEnemyWhite;
    public EnemyDefensive defEnemyBlack;
    public EnemyKamikaze kamiEnemyBlack;
    public EnemyDefensive[] enemyArrayDW;
    public EnemyKamikaze[] enemyArrayKW;
    public EnemyDefensive[] enemyArrayDB;
    public EnemyKamikaze[] enemyArrayKB;

    public Transform parentE;
    private bool canSpawn = false;
    public float spawnRate = 1f;
    private int enemyToSpawn = 0;

    private int limitW = 10;
    private int limitB = 10;
    public int currentW = 0;
    public int currentB = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreateEnemies();
        StartCoroutine(SpawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        /*if(canSpawn)
        {
            DecideEnemy();
        }*/
    }

    private void CreateEnemies()
    {
        for(int i = 0; i < enemyArrayDW.Length; i++)
        {
            enemyArrayDW[i] = Instantiate(defEnemyWhite, new Vector3(0,0,0), Quaternion.identity, parentE);
            enemyArrayDW[i].transform.localPosition = new Vector3(i,0,0);
        }
        for(int j = 0; j < enemyArrayKW.Length; j++)
        {
            enemyArrayKW[j] = Instantiate(kamiEnemyWhite, new Vector3(0,0,0), Quaternion.identity, parentE);
            enemyArrayKW[j].transform.localPosition = new Vector3(j,-1,0);
        }
        for(int k = 0; k < enemyArrayDB.Length; k++)
        {
            enemyArrayDB[k] = Instantiate(defEnemyBlack, new Vector3(0,0,0), Quaternion.identity, parentE);
            enemyArrayDB[k].transform.localPosition = new Vector3(k,-2,0);
        }
        for(int l = 0; l < enemyArrayKB.Length; l++)
        {
            enemyArrayKB[l] = Instantiate(kamiEnemyBlack, new Vector3(0,0,0), Quaternion.identity, parentE);
            enemyArrayKB[l].transform.localPosition = new Vector3(l,-3,0);
        }
    }
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnRate);
        canSpawn = true;
        Debug.Log("Waiting");
        DecideEnemy();
    }
    private void DecideEnemy()
    {
        int toSpawn = Random.Range(1, 5);
        if(toSpawn == 1)
        {
            //Spawn(enemyArrayDW);
            //SpawnDW();
            if(currentW >= limitW)
            {
                Generate(enemyArrayKW);
            }
            else {
                Generate(enemyArrayDW);
                currentW += 1;
            }
        }
        else if(toSpawn == 2)
        {
            //Spawn(enemyArrayKW);
            Generate(enemyArrayKW);
        }
        else if(toSpawn == 3)
        {
            
            if(currentB >= limitB)
            {
                Generate(enemyArrayKB);
            }
            //Spawn(enemyArrayDB);
            else {
                Generate(enemyArrayDB);
                currentB += 1;
            }
        }
        else
        {
            //Spawn(enemyArrayKB);
            //SpawnDW();
            Generate(enemyArrayKB);
        }
    }
    private void Generate(EnemyGeneric[] enemyArray)
    {
        //if(!canSpawn)return;
        for(int i = 0; i < enemyArray.Length; i++)
        {
            if(!enemyArray[i].isSpawning)
            {
                enemyArray[i].transform.position = transform.position;
                enemyArray[i].isSpawning = true;
                enemyArray[i].life = 1;
                enemyArray[i].dead = false;
                canSpawn = false;
                StartCoroutine(SpawnDelay());
                return;
            }
        }
    }
}

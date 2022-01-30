using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //private InputManager iM;
    private Vector3 move;
    private float initialSpeed;
    public float speed = 2.0f;
    public float fireRate = 0.5f;
    private bool canShoot = true;
    private bool triShoot = false;
    private bool divideShoot = false;
    public PBullet fBullet;
    public PBullet iBullet;
    public PBullet[] fBulletsArray;
    public PBullet[] iBulletsArray;
    public Transform parentB;
    private Animator animator;
    private int life = 3;

    //Raycast
    public float distance;
    public LayerMask bound;

    //Bools Movement
    private bool canMoveL;
    private bool canMoveR;
    private bool canMoveU;
    private bool canMoveD;
    public GameObject deadScreen;
    // Start is called before the first frame update
    void Start()
    {
        deadScreen.SetActive(false);
        //iM = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        animator = GetComponentInChildren<Animator>();
        CreateBullets();
        initialSpeed = speed;

        canMoveL = true;
        canMoveR = true;
        canMoveU = true;
        canMoveD = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Raycast Left
        Vector2 direction1 = transform.TransformDirection(Vector2.left) * distance;
        Gizmos.DrawRay(transform.position, direction1);
        Vector2 direction10 = transform.TransformDirection(new Vector2(-1,1) * distance);
        Gizmos.DrawRay(transform.position, direction10);
        //Raycast Right
        Vector2 direction2 = transform.TransformDirection(Vector2.right) * distance;
        Gizmos.DrawRay(transform.position, direction2);
        Vector2 direction20 = transform.TransformDirection(new Vector2(1,1) * distance);
        Gizmos.DrawRay(transform.position, direction20);
        //Raycast Up
        Vector2 direction3 = transform.TransformDirection(Vector2.up) * distance;
        Gizmos.DrawRay(transform.position, direction3);
        
        //Raycast Down
        Vector2 direction4 = transform.TransformDirection(Vector2.down) * distance;
        Gizmos.DrawRay(transform.position, direction4);
        Vector2 direction40 = transform.TransformDirection(new Vector2(1,-1) * distance);
        Gizmos.DrawRay(transform.position, direction40);
        //leftdown
        Vector2 direction30 = transform.TransformDirection(new Vector2(-1,-1) * distance);
        Gizmos.DrawRay(transform.position, direction30);
    }
    void FixedUpdate()
    {
        Vector2 direction1 = transform.TransformDirection(Vector2.left);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, direction1, distance, bound);
        Vector2 direction2 = transform.TransformDirection(Vector2.right);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, direction2, distance, bound);
        Vector2 direction3 = transform.TransformDirection(Vector3.up);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, direction3, distance, bound);
        Vector2 direction4 = transform.TransformDirection(Vector2.down);
        RaycastHit2D hit4 = Physics2D.Raycast(transform.position, direction4, distance, bound);

        Vector2 direction10 = transform.TransformDirection(new Vector2(-1,1));
        RaycastHit2D hit10 = Physics2D.Raycast(transform.position, direction10, distance, bound);
        Vector2 direction20 = transform.TransformDirection(new Vector2(1,1));
        RaycastHit2D hit20 = Physics2D.Raycast(transform.position, direction20, distance, bound);
        Vector2 direction30 = transform.TransformDirection(new Vector2(-1,-1));
        RaycastHit2D hit30 = Physics2D.Raycast(transform.position, direction30, distance, bound);
        Vector2 direction40 = transform.TransformDirection(new Vector2(1,-1));
        RaycastHit2D hit40 = Physics2D.Raycast(transform.position, direction40, distance, bound);
        if (hit1.collider == null && hit10.collider == null && hit30.collider == null)
        {
            //Left
            canMoveL = false;
        }
        else canMoveL = true;
        if(hit2.collider == null && hit20.collider == null && hit40.collider == null)
        {
            //Right
            canMoveR = false;
        }
        else canMoveR = true;
        if(hit3.collider == null && hit10.collider == null && hit20.collider ==null)
        {
            //Up
            canMoveU = false;
        }
        else canMoveU = true;
        if(hit4.collider == null && hit30.collider == null && hit40.collider == null)
        {
            //Down
            canMoveD = false;
        }
        else canMoveD = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(life == 2)
        {
            //lose one wing;
            move = new Vector3 (1, 0, 0);
            move = move * speed/5 * Time.deltaTime;
            //transform.position += move;
            transform.Translate(move, Space.World);
        }

    }
    public void Move(Vector3 axis)
    {
        if(!canMoveL && axis.x < 0) axis.x = 0;
        if(!canMoveR && axis.x > 0) axis.x = 0;
        if(!canMoveU && axis.y > 0) axis.y = 0;
        if(!canMoveD && axis.y < 0) axis.y = 0;
        move = new Vector3 (axis.x, axis.y, 0);
        move = Vector3.ClampMagnitude(move, 1f);
        move = move * speed * Time.deltaTime;
        //transform.position += move;
        transform.Translate(move, Space.World);
    }
    public void ShootF()
    {
        if(!triShoot)
        {
            NormalShoot();
        }
        if(triShoot)
        {
            TripleShoot();
        }
    }
    private void NormalShoot()
    {
        if(!canShoot) return;
        for(int i = 0; i < fBulletsArray.Length; i++)
        {
            if(!fBulletsArray[i].isMoving)
            {
                fBulletsArray[i].transform.position = transform.position;
                fBulletsArray[i].Shoot(new Vector3(0, 1, 0), 0f);
                fBulletsArray[i].isMoving = true;
                if(divideShoot)
                {
                    fBulletsArray[i].divideShoot = true;
                }
                canShoot = false;
                StartCoroutine(ShootDelay());
                Debug.Log("Shooting");
                return;
            }
        }
    }
    private void TripleShoot()
    {
        if(!canShoot) return;
        for(int i = 0; i < fBulletsArray.Length; i++)
        {
            if(!fBulletsArray[i].isMoving)
            {
                fBulletsArray[i].transform.position = transform.position;
                fBulletsArray[i].Shoot(new Vector3(0, 1, 0), 0f);
                fBulletsArray[i].isMoving = true;
                if(divideShoot)
                {
                    fBulletsArray[i].divideShoot = true;
                }

                while(fBulletsArray[i].isMoving)
                {
                    i++;
                }
                fBulletsArray[i].transform.position = transform.position;
                fBulletsArray[i].Shoot(new Vector3(0.5f, 1, 0), -10f);
                fBulletsArray[i].isMoving = true;
                if(divideShoot)
                {
                    fBulletsArray[i].divideShoot = true;
                }

                while(fBulletsArray[i].isMoving)
                {
                    i++;
                }
                fBulletsArray[i].transform.position = transform.position;
                fBulletsArray[i].Shoot(new Vector3(-0.5f, 1, 0), 10f);
                fBulletsArray[i].isMoving = true;
                if(divideShoot)
                {
                    fBulletsArray[i].divideShoot = true;
                }
                canShoot = false;

                StartCoroutine(ShootDelay());
                Debug.Log("Player Shooting");
                return;
            }
            else i++;
        }
    }
    public void ShootI()
    {
        if(!triShoot)
        {
            NormalIShoot();
        }
        if(triShoot)
        {
            TripleIShoot();
        }
    }
    public void NormalIShoot()
    {
        if(!canShoot) return;
        for(int i = 0; i < iBulletsArray.Length; i++)
        {
            if(!iBulletsArray[i].isMoving)
            {
                iBulletsArray[i].transform.position = transform.position;
                iBulletsArray[i].Shoot(new Vector3(0, 1, 0), 0f);
                iBulletsArray[i].isMoving = true;
                if(divideShoot)
                {
                    iBulletsArray[i].divideShoot = true;
                }
                canShoot = false;
                StartCoroutine(ShootDelay());
                Debug.Log("Shooting");
                return;
            }
        }
    }
    public void TripleIShoot()
    {
        if(!canShoot) return;
        for(int i = 0; i < iBulletsArray.Length; i++)
        {
            if(!iBulletsArray[i].isMoving)
            {
                iBulletsArray[i].transform.position = transform.position;
                iBulletsArray[i].Shoot(new Vector3(0, 1, 0), 0f);
                iBulletsArray[i].isMoving = true;
                if(divideShoot)
                {
                    iBulletsArray[i].divideShoot = true;
                }

                while(iBulletsArray[i].isMoving)
                {
                    i++;
                }
                iBulletsArray[i].transform.position = transform.position;
                iBulletsArray[i].Shoot(new Vector3(0.5f, 1, 0), -10f);
                iBulletsArray[i].isMoving = true;
                if(divideShoot)
                {
                    iBulletsArray[i].divideShoot = true;
                }

                while(iBulletsArray[i].isMoving)
                {
                    i++;
                }
                iBulletsArray[i].transform.position = transform.position;
                iBulletsArray[i].Shoot(new Vector3(-0.5f, 1, 0), 10f);
                iBulletsArray[i].isMoving = true;
                if(divideShoot)
                {
                    iBulletsArray[i].divideShoot = true;
                }

                canShoot = false;

                StartCoroutine(ShootDelay());
                Debug.Log("Shooting");
                return;
            }
            //else i++;
        }
    }
    private void CreateBullets()
    {
        for(int i = 0; i < fBulletsArray.Length; i++)
        {
            fBulletsArray[i] = Instantiate(fBullet, new Vector3(i,0,0), Quaternion.identity, parentB);
            fBulletsArray[i].transform.localPosition = new Vector3(i,0,0);
        }
        for(int i = 0; i < iBulletsArray.Length; i++)
        {
            iBulletsArray[i] = Instantiate(iBullet, new Vector3(i,0,0), Quaternion.identity, parentB);
            iBulletsArray[i].transform.localPosition = new Vector3(i,-1,0);
        }
    }
    public void ReceiveDamage()
    {
        life -= 1;
        if(life == 2)
        {
            //lose one wing;
            /*move = new Vector3 (axis.x, 0, 0);
            move = move * speed/3 * Time.deltaTime;
            transform.position += move;*/
            animator.SetTrigger("Hurt");
        }
        else if(life == 1)
        {
            //lose two wings;
            speed /= 1.5f;
            animator.SetTrigger("Wingless");
        }
        else Die();
    }
    private void Die()
    {
        speed = 0;
        canShoot = false;
        deadScreen.SetActive(true);
        gameObject.SetActive(false);
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
    public void GetPowerUp(string power)
    {
        if(power == "Triple")
        {
            triShoot = true;
        }
        if(power == "Divide")
        {
            divideShoot = true;
        }
        if(power == "Life")
        {
            if(life >= 3) 
            {
                animator.SetTrigger("Full");
                return;
            }
            life += 1;
            speed = initialSpeed;
        }
    }
}
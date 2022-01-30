using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPBullet : MonoBehaviour
{
    public bool isRed;
    public bool isMoving = false;
    private bool canDoDamage = false;
    private Vector3 direction;
    private Vector3 startingPos;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            transform.position += direction * speed * Time.deltaTime;
            
        }
    }
    public void Shoot(Vector3 nDirection)
    {
        isMoving = true;
        direction = nDirection;
        StartCoroutine(ShootDelay());
    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if(!canDoDamage) return;
        if(other.tag == "Enemy" && canDoDamage)
        {
            //TakeDamage
            Debug.Log("Hit");
            Reset();
            //Insanciar mas balas
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Bound")
        {
            Reset();
        }
    }
    public void Reset()
    {
        transform.position = startingPos;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        isMoving = false;
        canDoDamage = false;
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.2f);
        canDoDamage = true;
    }
}

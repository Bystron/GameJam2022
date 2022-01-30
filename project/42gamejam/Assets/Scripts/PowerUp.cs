using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PlayerBehaviour player;
    public bool tripleShoot;
    public bool divideShoot;
    public bool life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<PlayerBehaviour>();
            if(tripleShoot)
            {
                player.GetPowerUp("Triple");
            }
            if(divideShoot)
            {
                player.GetPowerUp("Divide");
            }
            if(life)
            {
                player.GetPowerUp("Life");
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            Killable playerLife = col.gameObject.GetComponent<Killable>();
            if (playerLife.currentHitPoints < 100)
            {
                /*//FindObjectOfType<AudioManager>().Play("heal");
                FindObjectOfType<AudioManager>().Play2(1);*/
                playerLife.currentHitPoints = playerLife.HitPoints;
                Destroy(gameObject);
            }
            

        }
    }
}

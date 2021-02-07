using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public int dmg;
    public float force;
    public Vector2 direction;
    public int bounces;
    public float onScreenStay;
    private float currentOnScreenTime;
    void Start()
    {
        currentOnScreenTime = onScreenStay;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(11,13);
    }
    void Update()
    {
        rb2D.AddForce(direction * force);
        if (currentOnScreenTime > 0)
        {
            currentOnScreenTime -= Time.deltaTime;
        }
        if (currentOnScreenTime <= 0 || bounces <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        bool bounce=false;
        foreach (ContactPoint2D contact in col.contacts)
        {
            //Color color = Color.green;
            if (col.gameObject.tag != "Enemy")
            {
                int normalxInt = Mathf.RoundToInt(contact.normal.x);
                if (normalxInt == 1 || normalxInt == -1)
                {
                    //Debug.Log("vertical collide on: " + contact.normal.x+" rounded to " + normalxInt);
                    if (bounces > 0)
                    {
                        bounce = true;
                    }
                    //color = Color.red;
                }
                /*else
                {
                    color = Color.green;
                }*/
                //Debug.DrawLine(contact.point, contact.point + contact.normal, color, 2, false);
                //Debug.Log("contact point x: " + contact.normal.x + " y: " + contact.normal.y);
            }
        }
        Killable target = col.transform.GetComponent<Killable>();
        if (target != null && (col.gameObject.layer == 12 || col.gameObject.layer == 14)) 
        { 
            target.ChangeHitPoints(-dmg);
        }
        if (bounces > 0 && bounce)
        {
            direction *= -1;
            bounces--;
            //Debug.Log("bounces: " + bounces);
        }
        if (bounces <= 0)
        {
            Destroy(gameObject);
        }
    }
}

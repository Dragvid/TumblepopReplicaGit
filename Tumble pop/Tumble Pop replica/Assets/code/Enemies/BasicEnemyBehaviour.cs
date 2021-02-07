using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehaviour : MonoBehaviour
{
    public float speed;
    public float reactionTime;
    public float jumpSpeed;
    public GameObject feet;
    public LayerMask climbableLayers;
    public float climbDownRange;
    private Vector2 dir;
    private SpriteRenderer spriteRenderer;
    private float inicialReactionTime;
    private Rigidbody2D rb;
    private Animator animator;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        dir.x = -1;
        inicialReactionTime = reactionTime;
    }
    private void Update()
    {
        FlipSprite();
        Walk();
        if (reactionTime > 0)
        {
            reactionTime -= Time.deltaTime;
        }
        else
        {
            DecideAction();
            reactionTime = inicialReactionTime;
        }
    }
    private void DecideAction()
    {
        int decision = Random.Range(1, 7);
        //Debug.Log("decision: " + decision);
        switch (decision)
        {
            case 1:
                //stop
                dir.x = 0;
                break;
            case 2:
                //change direction
                dir.x = -dir.x;
                break;
            case 3:
                //walk 
                dir.x = 1;
                break;
            case 4:
                dir.x = -1;
                break;
            case 5:
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                break;
            case 6:
                ClimbDown();
                break;
        }
    }
    private void Walk()
    {
        bool isWalking;
        if (dir.x != 0)
            isWalking = true;

        else
        {
            isWalking = false;
        }
        //Debug.Log("isWalking: " + isWalking);
        animator.SetBool("isWalking", isWalking);
        transform.Translate(dir * speed * Time.deltaTime);
    }
    public void ClimbDown()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(feet.transform.position, climbDownRange, climbableLayers);
        foreach (Collider2D ground in hitEnemies)
        {
            PlatformEffector2D platformEffector2D = ground.GetComponent<PlatformEffector2D>();
            if (platformEffector2D != null)
            {
                platformEffector2D.rotationalOffset = 180;
            }
        }
    }
    private void FlipSprite()
    {
        if (dir.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.gameObject.layer);
        if (col.gameObject.layer == 9)
        {
            dir *= -1;
        }
        if (col.gameObject.tag == "Player")
        {
            Killable playerLifes = col.gameObject.GetComponent<Killable>();
            playerLifes.ChangeHitPoints(-1);
        }
    }
}
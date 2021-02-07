using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    PlayerControls controler;
    //[SerializeField] private tag groundLayers;
    public float gravity;
    public float speed;
    public float jumpSpeed;
    public GameObject feet;
    public LayerMask climbableLayers;
    public float climbDownRange;
    private float speedIni;
    private Vector3 playerDir;
    [HideInInspector]
    public int dirX;
    private float dirY;
    private bool jumpOn;
    private bool jumpReady;
    private bool grounded;
    private bool walkRight;
    private bool walkLeft;
    [HideInInspector]
    public bool isWalking;
    //save inputs
    /*public string[] playerInputs;
    private int playerInputIndex;*/
    //camera
    //private GameObject cam;
    //components
    private Animator animator;
    private CapsuleCollider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private GameObject[] atackOrigins;
    Rigidbody2D rb;

    private void Start()
    {
        //cam = GameObject.FindGameObjectWithTag("MainCamera");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        collider2D = gameObject.GetComponent<CapsuleCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        speedIni = speed;
        //playerInputIndex = 0;
        dirX = 1;
    }
    private void Awake()
    {
        controler = new PlayerControls();
        //d-pad
        controler.Gameplay.MoveRight.performed += ctx => MoveX(1);
        controler.Gameplay.MoveRight.performed += ctx => walkRight = true;
        controler.Gameplay.MoveRight.canceled += ctx => walkRight = false;
        controler.Gameplay.MoveRight.canceled += ctx => Stop();

        controler.Gameplay.MoveLeft.performed += ctx => MoveX(-1);
        controler.Gameplay.MoveLeft.performed += ctx => walkLeft = true;
        controler.Gameplay.MoveLeft.canceled += ctx => walkLeft = false;
        controler.Gameplay.MoveLeft.canceled += ctx => Stop();

        controler.Gameplay.Down.performed += ctx => ClimbDown();
        controler.Gameplay.Down.canceled += ctx => Stop();

        controler.Gameplay.Jump.performed += ctx => Jump(1);
        //controler.Gameplay.Jump.canceled += ctx => Stop();
    }
    void MoveX(int dir)
    {
        //speed = speedIni;
        if (dirX != dir)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        dirX = dir;
        switch (dir)
        {
            case 1:
                playerDir.x = 1;
                break;
            case -1:
                playerDir.x = -1;
                break;
        }
        //playerDir.x = (playerDir.x + dirX);
        //Debug.Log(playerDir.x);
    }
    void MoveY()
    {
        if (jumpOn && jumpReady && grounded)
        {
            //playerDir.y = dirY;
            rb.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
            jumpOn = false;
            jumpReady = false;
            
        }
        if (dirY < 0)
        {
            
            if (collider2D.enabled != true)
            {
                collider2D.enabled = true;
            }
            playerDir.y = dirY;
        }
    }
    void Jump(float dir)
    {
        dirY = dir;
        if(dir>0)
            jumpOn = true;
        MoveY();
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
    private void OnEnable()
    {
        controler.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controler.Gameplay.Disable();
    }
    private void Stop(){
        //isWalking = false;
        //Debug.Log("stop");
        if (playerDir.x !=0 && !walkLeft && !walkRight)
        {
            playerDir.x = 0;
            //playerDir.x -=Time.deltaTime*friction;
        }
        if (playerDir.y != 0)
        {
            playerDir.y = 0;
            //playerDir.x -=Time.deltaTime*friction;
        }
        rb.velocity = Vector3.zero;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10){
            grounded = true;
            jumpReady = true;
            playerDir.y = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        jumpReady = true;
        playerDir.y = 0;
    }
    private void FixedUpdate()
    {
        if (!grounded && playerDir.y<=0)
        {
            playerDir.y = gravity;
        }
        rb.AddForce(playerDir * speed, ForceMode2D.Impulse);
        //Debug.Log("player dir: " + playerDir);
        //Debug.Log("key walking pressed:" + Keyboard.current);
        if (playerDir.x != 0)
            isWalking = true;
        else
        {
            isWalking = false;
        }
        animator.SetBool("Walking", isWalking);
        animator.SetBool("grounded", grounded);
        //rb.MovePosition(transform.position + (playerDir * Time.deltaTime * speed));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public float HitPoints;
    public bool destroyOnDeath;
    [HideInInspector]
    public float currentHitPoints;
    private bool invulnerable;
    private Vector2 spawnLocation;
    private Animator animator;
    private PlayerController playerMovement;
    private SuckEnemy playerSuckEnemy;
    private GameObject scoreText;
    SetScoreBehaviour setScore;
    void Start()
    {
        invulnerable = false;
        if (gameObject.tag == "Player")
        {
            playerMovement = gameObject.GetComponent<PlayerController>();
            playerSuckEnemy = gameObject.GetComponent<SuckEnemy>();
            spawnLocation = transform.position;
        }
        setScore = null;
        if (gameObject.tag == "Enemy")
        {
            scoreText = GameObject.FindGameObjectWithTag("Score");
            setScore = scoreText.GetComponent<SetScoreBehaviour>();
        }
        animator = gameObject.GetComponent<Animator>();
        currentHitPoints = HitPoints;
    }
    void Update()
    {

    }
    public void ChangeHitPoints(int change)
    {
        if (!invulnerable)
        {
            if (animator != null)
            {
                animator.SetTrigger("HurtTrigger");
                StartCoroutine(TakeDamageAnimation());
            }
            currentHitPoints += change;
            if (gameObject.tag == "Enemy")
            {
                if (change < 0)
                {
                    change *= -1;
                }
                if(scoreText!=null)
                    setScore.UpdateScore(change);
            }
            invulnerable = true;    
            //Debug.Log("current Hit Points: " + currentHitPoints);
        }
    }
    IEnumerator TakeDamageAnimation()
    {
        if (gameObject.tag == "Player")
        {
            playerMovement.enabled = false;
            playerSuckEnemy.enabled = false;
        }
        CapsuleCollider2D capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        if (capsuleCollider2D != null)
        {
            capsuleCollider2D.enabled = false;
        }
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        if (currentHitPoints <= 0)
        {
            Die();
        }
        else
        {
            if (capsuleCollider2D != null)
            {
                capsuleCollider2D.enabled = transform;
            }
            Respawn();
        }
    }
    public void Respawn()
    {
        transform.position = spawnLocation;
        invulnerable = false;
        playerMovement.enabled = true;
        playerSuckEnemy.enabled = true;
    }
    public void Die()
    {
        if (destroyOnDeath)
            Destroy(gameObject);
    }
}

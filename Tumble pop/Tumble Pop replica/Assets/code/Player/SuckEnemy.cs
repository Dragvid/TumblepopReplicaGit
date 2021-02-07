using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckEnemy : MonoBehaviour
{
    PlayerControls controler;
    public GameObject projectilePrefab;
    public GameObject rayOriginLeft;
    public GameObject rayOriginRight;
    public GameObject rayEffectRight;
    public GameObject rayEffectLeft;
    public GameObject tanqPrefab;
    public GameObject tanqOriginLeft;
    public GameObject tanqOriginRight;
    //public LayerMask enemyLayers;
    public LayerMask suckableEnemyLayers;
    public int suckDmg;
    public int projectileDmgMultiplier;
    public float projectileShootForce;
    public float overHeatTime;
    public float rangeAtk;
    private bool rayOn;
    private GameObject ray;
    private GameObject tanq;
    private PlayerController dir;
    [HideInInspector]
    public int ammo;
    private float currentOverHeatTime;
    SpriteRenderer raySprRenderer;
    private void Awake()
    {
        
        controler = new PlayerControls();
        //attack
        controler.Gameplay.Suck.performed += ctx => rayOn = true;
        controler.Gameplay.Suck.canceled += ctx => rayOn = false;
        controler.Gameplay.Suck.canceled += ctx => ReleaseVacuum();
    }

    void Start()
    {
        dir = gameObject.GetComponent<PlayerController>();
        currentOverHeatTime = overHeatTime;
    }
    void Update()
    {
        if (rayOn)
        {
            SuckTarget(dir.dirX, suckDmg, rangeAtk);
        }
        //UpdateTanq(dir.dirX);
    }
    private void OnEnable()
    {
        controler.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controler.Gameplay.Disable();
    }
    void SuckTarget(int direction, int dmg, float range)
    {
        GameObject attackOrigin = null;
        GameObject rayEffect = null;
        if (direction == 1)
        {
            attackOrigin = rayOriginRight;
            rayEffect = rayEffectRight;
        }
        if (direction == -1)
        {
            attackOrigin = rayOriginLeft;
            rayEffect = rayEffectLeft;
        }
        //Debug.Log(attackOrigin.name);
        if (ray == null)
        {
            ray = Instantiate(rayEffect, attackOrigin.transform.position, Quaternion.identity);
            ray.transform.SetParent(attackOrigin.transform);
        }
        if (attackOrigin.transform.childCount <= 0)
        {
            Destroy(ray);
            ray = Instantiate(rayEffect, attackOrigin.transform.position, Quaternion.identity);
            ray.transform.SetParent(attackOrigin.transform);
        }
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackOrigin.transform.position, range, suckableEnemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Killable target = enemy.transform.GetComponent<Killable>();
            if (target != null)
            {
                target.ChangeHitPoints(-dmg);
                ammo++;
            }
        }
        if (currentOverHeatTime > 0 && ammo > 0) 
        {
            currentOverHeatTime -= Time.deltaTime;
        }
        if (currentOverHeatTime <= 0 && ammo > 0)
        {
            ReleaseVacuum();   
        }
    }
    private void UpdateTanq(int direction)
    {
        GameObject tanqOrigin = null;
        tanq = null;
        if (direction == 1)
        {
            tanqOrigin = tanqOriginLeft;
        }
        if (direction == -1)
        {
            tanqOrigin = tanqOriginRight;
        }
        if (tanq == null)
        {
            tanq = Instantiate(tanqPrefab, tanqOrigin.transform.position, Quaternion.identity);
            tanq.transform.SetParent(tanqOrigin.transform);
        }
        if (tanqOrigin.transform.childCount <= 0)
        {
            Destroy(tanq);
            tanq = Instantiate(tanqPrefab, tanqOrigin.transform.position, Quaternion.identity);
            tanq.transform.SetParent(tanqOrigin.transform);
        }
    }
    private void ReleaseVacuum()
    {
        currentOverHeatTime = overHeatTime;
        //Debug.Log("ammo: " + ammo);
        if (ammo > 0)
        {
            shoot(dir.dirX,ammo,projectileShootForce);
            ammo = 0;
        }
        Destroy(ray);
    }

    public void shoot(int direction,int ammo, float projectileforce)
    {
        GameObject attackOrigin = null;
        if (direction == 1)
        {
            attackOrigin = rayOriginRight;
        }
        if (direction == -1)
        {
            attackOrigin = rayOriginLeft;
        }
        int projectileDmg = projectileDmgMultiplier * ammo;
        Vector2 projSpawnPos = attackOrigin.transform.position;
        GameObject projectile = Instantiate(projectilePrefab, projSpawnPos, Quaternion.identity);
        ProjectileBehaviour projectilebehaviour = projectile.transform.GetComponent<ProjectileBehaviour>();
        projectilebehaviour.dmg = projectileDmg;
        projectilebehaviour.force = projectileforce;
        projectilebehaviour.bounces = ammo;
        projectilebehaviour.direction = new Vector2(direction, 0);
    }
    /*private void OnDrawGizmosSelected()
    {
        GameObject mediumOrigin = null;
        if (dir.dirX != 0)
        {
            if (dir.dirX == 1)
            {
                mediumOrigin = rayOriginRight;
            }
            if (dir.dirX == -1)
            {
                mediumOrigin = rayOriginLeft;
            }
        }
        Gizmos.DrawWireSphere(mediumOrigin.transform.position, rangeAtk);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] prefab;
    public int onScreenSize;
    public float spawnRate;
    private float spawnTimer;
    public GameObject[] enemiesInScene;
    private int enemiesInSceneCount;

    void Start()
    {
        enemiesInSceneCount = 0;
        spawnTimer = spawnRate;
    }
    void Update()
    {
        
        spawnTimer--;
        if (spawnTimer <= 0)
        {
            enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesInSceneCount = enemiesInScene.Length;
            if (enemiesInSceneCount <= onScreenSize)
            {
                //Debug.Log(enemiesInSceneCount);
                SpawnEnemy();
            }
        }
    }
    private void SpawnEnemy()
    {
        int index = Random.Range(0, prefab.Length);
        Instantiate(prefab[index], transform.position, Quaternion.identity);
        spawnTimer = spawnRate;
    }
}

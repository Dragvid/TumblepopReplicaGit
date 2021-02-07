using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageState : MonoBehaviour
{
    private GameObject[] enemiesInScene;
    private GameObject[] playersInScene;
    int enemiesInSceneCount;
    Killable playerLife;
    void Start()
    {
        
    }
    void Update()
    {
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesInSceneCount = enemiesInScene.Length;
        if (enemiesInSceneCount <= 0)
        {
            EndStageWin();
        }
        playersInScene = GameObject.FindGameObjectsWithTag("Player");
        if (playersInScene.Length <= 0)
        {
            EndStageLose();
        }
    }
    private void EndStageWin()
    {
        //SuckEnemy[] playerAmmo=new SuckEnemy[playersInScene.Length];
        bool ammoDepleted = true;
        for (int i = 0; i <= playersInScene.Length-1; i++)
        {
            SuckEnemy playerAmmo = playersInScene[i].GetComponent<SuckEnemy>();
            if (playerAmmo.ammo > 0)
            {
                ammoDepleted = false;
            }
        }
        if (transform.childCount > 0 && ammoDepleted)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void EndStageLose()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
}

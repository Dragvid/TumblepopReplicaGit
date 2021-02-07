using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerLifes : MonoBehaviour
{
    private GameObject player;
    private Text lifeText;
    void Start()
    {
        lifeText = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateLifeText();
    }
    public void UpdateLifeText()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Killable life = player.GetComponent<Killable>();
            lifeText.text = "X" + life.currentHitPoints;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnMusic : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    //public GameObject musicManager;
    private bool state;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        state = true;
    }
    public void OnOffMusic()
    {
        state = !state;
        if (state)
        {
            //musicManager.SetActive(true);
            canvasGroup.alpha = 1f;
        }
        else
        {
            //musicManager.SetActive(false);
            canvasGroup.alpha = 0.5f;
        }
    }
}

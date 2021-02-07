using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOffset : MonoBehaviour
{
    public float reloadBreak;
    private float currentReloadBreak;
    PlatformEffector2D platformEffector2D;
    PlayerControls controler;
    void Start()
    {
        currentReloadBreak = reloadBreak;
        platformEffector2D = gameObject.GetComponent<PlatformEffector2D>();
    }
    private void Awake()
    {
        controler = new PlayerControls();
        controler.Gameplay.Down.canceled += ctx => ResetPlatformOffset();
        controler.Gameplay.Jump.performed += ctx => ResetPlatformOffset();
    }
    private void OnEnable()
    {
        controler.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controler.Gameplay.Disable();
    }
    void Update()
    {
        /*if (platformEffector2D.rotationalOffset > 0)
        {
            Debug.Log(gameObject.name + "offset :" + platformEffector2D.rotationalOffset);
            
            if (currentReloadBreak > 0)
            {
                Debug.Log(reloadBreak);
                currentReloadBreak = -Time.deltaTime;
            }
            if(currentReloadBreak<=0)
            {
                currentReloadBreak = reloadBreak;
                ResetPlatformOffset();
            }
        }*/
    }
    public void ResetPlatformOffset()
    {
        platformEffector2D.rotationalOffset = 0;
    }
}

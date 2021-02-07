using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    private bool state;
    //public static AudioManager instance;
    private void Awake()
    {
        GameObject[] instance = GameObject.FindGameObjectsWithTag("Music");
        if (instance.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.GetComponent<AudioSource>();
            s.source.clip = s.clip;            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        state = true;
    }
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
            return;
        s.source.Play();
        //Debug.Log("clip lenght :"+s.clip.length);
        //Debug.Log("clip name :" + s.clip.name);
    }
    public void Play2(int index)
    {
        //Sounds s = Array.Find(sounds, sounds => sounds.name == name);
        if (sounds[index] == null)
            return;
        sounds[index].source.Play();
        //Debug.Log("clip lenght :"+s.clip.length);
        //Debug.Log("clip name :" + s.clip.name);
    }
    public void VolumeUpdate(float newVolume)
    {
        foreach (Sounds s in sounds)
        {
            s.source.volume = newVolume;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class soundFXmanager:MonoBehaviour
{
    public static soundFXmanager instance;
    [SerializeField] private AudioSource soundFXobject;
    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
    }
    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform,float volum)
    {
        //spawn in game object
        AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);
        //assign the audio clip
        audioSource.clip = audioClip;
        //assign volume
        audioSource.volume = volum;
        //play sound
        audioSource.Play();
        //get legth of soundFX `clip
        float cliplength=audioSource.clip.length;
        //destroy the clip after its done playing
        Destroy(audioSource.gameObject, cliplength);
    }
    public void PlayrandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volum)
    {
        
        //assign a rando index
        int rand=UnityEngine.Random.Range(0, audioClip.Length);
        //spawn in game object
        AudioSource audioSource = Instantiate(soundFXobject, spawnTransform.position, Quaternion.identity);
        //assign the audio clip
        audioSource.clip = audioClip[rand];
        //assign volume
        audioSource.volume = volum;
        //play sound
        audioSource.Play();
        //get legth of soundFX `clip
        float cliplength = audioSource.clip.length;
        //destroy the clip after its done playing
        Destroy(audioSource.gameObject, cliplength);
    }
}

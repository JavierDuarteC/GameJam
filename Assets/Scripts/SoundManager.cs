using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Intro")] public AudioSource introEfx;
    
[Header("Ambiente:")] 
    public AudioSource playingEfx;

    [Header("Winning:")] public AudioSource endEfx;

    
    public static SoundManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }else if (instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void playIntroEfx(AudioClip clip)
    {
        introEfx.clip = clip;
        introEfx.Play();
    }
    public void stopIntroEfx()
    {
        introEfx.Stop();
    }

    public void playPlayingEfx(AudioClip clip)
    {
        playingEfx.clip = clip;
        playingEfx.Play();
    }
    public void stopPlayingEfx()
    {
        playingEfx.Stop();
    }
    public void playEndEfx(AudioClip clip)
    {
        endEfx.clip = clip;
        endEfx.Play();
    }
    public void stopEndEfx()
    {
        endEfx.Stop();
    }
   

    // Update is called once per frame
    void Update()
    {

    }
}
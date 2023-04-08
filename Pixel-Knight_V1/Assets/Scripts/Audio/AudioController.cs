using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    int musicToPlay;

    [SerializeField] AudioSource[] SFX;
    [SerializeField] AudioSource[] music;
    [SerializeField] AudioSource levelEnd;
    
    [SerializeField] AudioSource bossBattle;

    [SerializeField] bool isMenu;
    [SerializeField] bool isSummer;
    [SerializeField] bool isForest;
    [SerializeField] bool isWinter;
    [SerializeField] bool isCave;
    [SerializeField] bool isDesert;
    [SerializeField] bool isCastle;
    [SerializeField] bool isOutro;

    void Start()
    {
        if(isMenu) 
        {
            PlayMusic(6);
        }

        if (isSummer)
        {
            PlayMusic(3);
        }

        if (isForest)
        {
            PlayMusic(5);
        }

        if (isWinter)
        {
            PlayMusic(1);
        }
        if (isCave)
        {
            PlayMusic(2);
        }

        if (isDesert)
        {
            PlayMusic(0);
        }

        if (isCastle)
        {
            PlayMusic(4);
        }

        if (isOutro)
        {
            //PlayMusic(7);               // not added yet
        }
    }

    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        SFX[soundToPlay].Stop();
        SFX[soundToPlay].Play();
    }

    public void PlayMusic(int musicToPlay)
    {
        music[musicToPlay].Play();
    }

    public void PlayLevelEndMusic()
    {
        music[musicToPlay].Stop();        // this might not work

        levelEnd.Play();
    }

    public void PlayBossBattleMusic()
    {
        music[musicToPlay].Stop();        // this might not work

        bossBattle.Play();
    }

    public void EndBossBattleMusic()
    {
        bossBattle.Stop();
    }
}

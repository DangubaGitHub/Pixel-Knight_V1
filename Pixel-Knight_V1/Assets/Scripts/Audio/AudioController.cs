using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    int musicToPlay;

    [SerializeField] AudioSource[] SFX;
    
    [SerializeField] AudioSource Menu;
    [SerializeField] AudioSource Summer;
    [SerializeField] AudioSource Forest;
    [SerializeField] AudioSource Winter;
    [SerializeField] AudioSource Cave;
    [SerializeField] AudioSource Desert;
    [SerializeField] AudioSource Castle;
    [SerializeField] AudioSource Outro;
    [SerializeField] AudioSource bossBattle;
    [SerializeField] AudioSource levelEnd;

    [SerializeField] bool isMenu;
    [SerializeField] bool isSummer;
    [SerializeField] bool isForest;
    [SerializeField] bool isWinter;
    [SerializeField] bool isCave;
    [SerializeField] bool isDesert;
    [SerializeField] bool isCastle;
    [SerializeField] bool isOutro;

    public static AudioController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(isMenu) 
        {
            Menu.Play();
        }

        if (isSummer)
        {
            Summer.Play();
        }

        if (isForest)
        {
            Forest.Play();
        }

        if (isWinter)
        {
            Winter.Play();
        }
        if (isCave)
        {
            Cave.Play();
        }

        if (isDesert)
        {
            Desert.Play();
        }

        if (isCastle)
        {
            Castle.Play();
        }

        if (isOutro)
        {
            Outro.Play();
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

    /*public void PlayMusic(int musicToPlay)
    {
        music[musicToPlay].Play();
    }*/

    public void PlayLevelEndMusic()
    {
        Menu.Stop();
        Summer.Stop();
        Forest.Stop();
        Winter.Stop();
        Cave.Stop();
        Desert.Stop();
        Castle.Stop();

        levelEnd.Play();
    }

    public void PlayBossBattleMusic()
    {
        Castle.Stop();

        bossBattle.Play();
    }

    public void EndBossBattleMusic()
    {
        //bossBattle.Stop();
    }
}

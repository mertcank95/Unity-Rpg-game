using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource menuMusic, gameMusic;

    public AudioSource[] sfx;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayMenuMusic()
    {
        if (gameMusic.isPlaying)
        {
            gameMusic.Stop();
        }
        menuMusic.Play();

    }


    public void PlayGameMusic()
    {
        if (menuMusic.isPlaying)
        {
            menuMusic.Stop();
        }
        gameMusic.Play();

    }

    public void PlaySfx(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();

    }


}

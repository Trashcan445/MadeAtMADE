using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChanger : MonoBehaviour
{
    public AudioClip mainSong;
    public AudioClip pageantSong;
    public AudioSource musicSource;

    public void SwitchToMainSong()
    {
        musicSource.Stop();
        musicSource.clip = mainSong;
        musicSource.Play();
    }

    public void SwitchToPageantSong()
    {
        musicSource.Stop();
        musicSource.clip = pageantSong;
        musicSource.Play();
    }

    public void ChangeSongPitch(float pitch)
    {
        musicSource.pitch = pitch;
    }

    public void ChangeSongVol(float volume)
    {
        musicSource.volume = volume;
    }
}

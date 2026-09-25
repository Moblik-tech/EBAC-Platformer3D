using System.Collections.Generic;
using UnityEngine;
using Moblik.Utils;
using Moblik.Core.Singleton;

public class SoundManager : Singleton<SoundManager>
{
    [Header("<------ Sounds Configs ------>")]
    public AudioSource musicSource;
    public List<MusicSetup> musicSetup;

    [Space(15)]

    public AudioSource sFXSource;
    public List<SFXSetup> sFXSetup;

    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetup.Find(i => i.musicType == musicType);
    }

    public void PlaySFXByType(SFXType sfxType)
    {
        var sfx = GetSFXByType(sfxType);
        sFXSource.clip = sfx.audioClip;
        sFXSource.Play();
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sFXSetup.Find(i => i.sFXType == sfxType);
    }
}
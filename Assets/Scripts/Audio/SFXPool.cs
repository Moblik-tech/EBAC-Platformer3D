using System.Collections.Generic;
using UnityEngine;
using Moblik.Utils;
using Moblik.Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    public GameObject audioSourcePrefab;
    public int poolSize = 5;
    [SerializeField, NaughtyAttributes.ReadOnly]  private int _index = 0;
    [SerializeField, NaughtyAttributes.ReadOnly] private List<AudioSource> _audioSourceList;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = Instantiate(audioSourcePrefab);

        go.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(go.GetComponent<AudioSource>());
    }

    public void PlaySFX(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE)
        {
            Debug.LogWarning("Nenhum tipo de SFX foi definido.");
            return;
        }

        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++;
        if (_index >= _audioSourceList.Count) _index = 0;
    }
}
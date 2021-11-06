using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AC_Sound
{
    public string name;
    public AudioClip audioClip;

    public AC_Sound(string _name, AudioClip _audioClip)
    {
        name = _name;
        audioClip = _audioClip;
    }
}

public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    private void Awake()
    {
        instance = this;

    }

    [SerializeField] public AC_Sound[] common_clips;
    public AudioSource BGM;
    public List<AudioSource> commonAudioSources;

    private void Start()
    {
        BGM.Play();
        commonAudioSources = new List<AudioSource>();
        foreach (var clip in common_clips)
        {
            GameObject newAudio = new GameObject();
            newAudio.transform.parent = this.transform;
            AudioSource newSource = newAudio.AddComponent<AudioSource>();
            newSource.clip = clip.audioClip;
            newAudio.gameObject.name = clip.name;
            commonAudioSources.Add(newSource);
        }
    }

    public void SoundControll(string clipName)
    {
        foreach(var i  in commonAudioSources)
        {
            if (i.gameObject.name == clipName)
                i.Play();
        }
    }
}
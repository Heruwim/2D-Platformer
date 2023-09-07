using System;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _introSource, _loopSource;

    private void Start()
    {
        _introSource.Play();
        _loopSource.PlayScheduled(AudioSettings.dspTime + _introSource.clip.length);
    }
}

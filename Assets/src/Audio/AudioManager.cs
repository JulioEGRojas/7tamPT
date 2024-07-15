using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    /// <summary>
    /// Audio source that plays 2D Sounds
    /// </summary>
    [SerializeField] private AudioSource globalAudioSource;
    
    /// <summary>
    /// Audio source that plays 3d sounds
    /// </summary>
    [SerializeField] private AudioSource localAudioSource;

    private void Awake() {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this;
    }
    
    public void PlayClipAtPosition(AudioClip clip, Vector3 soundPosition) {
        AudioSource.PlayClipAtPoint(clip,soundPosition);
    }

    public void PlayClipAtPosition(AudioClip clip, Vector3 soundPosition, float volume) {
        AudioSource.PlayClipAtPoint(clip,soundPosition,volume);
    }
    
    public void PlayGlobal(AudioClip clip) {
        globalAudioSource.PlayOneShot(clip);
    }
    
    public void PlayRandomClipGlobal(AudioClipGroup audioClips) {
        PlayGlobal(audioClips.GetRandom());
    }
    
    public void PlayRandomClipAtPosition(AudioClipGroup audioClips, Vector3 position) {
        PlayRandomClipAtPosition(audioClips,position,1f);
    }
    
    public void PlayRandomClipAtPosition(AudioClipGroup audioClips, Vector3 position, float volume) {
        instance.PlayClipAtPosition(audioClips.GetRandom(), position, volume);
    }
    
    public void PlayRandomClipAtPosition(List<AudioClip> audioClips, Vector3 position) {
        PlayRandomClipAtPosition(audioClips,position,1f);
    }
    
    public void PlayRandomClipAtPosition(List<AudioClip> audioClips, Vector3 position, float volume) {
        localAudioSource.volume = volume;
        if (audioClips.Count == 0) {
            return;
        }
        instance.PlayClipAtPosition(audioClips[UnityEngine.Random.Range(0, audioClips.Count)], position, volume);
    }
    
    public void PlayRandomClipAtPosition(AudioClip[] audioClips, Vector3 position) {
        PlayRandomClipAtPosition(audioClips,position,1f);
    }
    
    public void PlayRandomClipAtPosition(AudioClip[] audioClips, Vector3 position, float volume) {
        localAudioSource.volume = volume;
        if (audioClips.Length == 0) {
            return;
        }
        instance.PlayClipAtPosition(audioClips[UnityEngine.Random.Range(0, audioClips.Length)], position, volume);
    }
}

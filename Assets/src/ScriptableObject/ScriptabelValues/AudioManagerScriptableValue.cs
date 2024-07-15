using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Manager Reference", menuName = "Values/Runtime Audio Manager")]
public class AudioManagerScriptableValue : NonSerializedScriptableValue<AudioManager> {
    
    public void PlayGlobal(AudioClip clip) {
        value.PlayGlobal(clip);
    }
    
    public void PlayRandomClipGlobal(AudioClipGroup audioClips) {
        value.PlayRandomClipGlobal(audioClips);
    }
}

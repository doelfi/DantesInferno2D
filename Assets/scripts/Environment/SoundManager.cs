using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    // there are multiple audiosources now
    // the function just takes the first that is not currently playing anything
    // this allows us to play multiple sounds at once
    private AudioSource[] _audioSources;
    
    // all possible Sound Effects
    public enum SoundOptions
    {
        PlayerJump,
        PlayerMove,
        PlayerLand,
        CoinCollected,
        LifeGained,
        DamageTaken,
        EnemyKilled,
        FinishReached,
    }
    
    // this simulates a dictionary in the editor
    // since dictionaries can not be filled over the editor
    [System.Serializable]
    public struct AudioClips
    {
        public SoundManager.SoundOptions name;
        public AudioClip clip;
    }
    
    public AudioClips[] SoundAssets;
    
    void Start()
    {
        _audioSources = gameObject.GetComponents<AudioSource>();
    }
    
    // takes one of the Options listed above and plays it once
    public void PlaySound(SoundOptions name)
    {
        foreach (AudioClips entry in SoundAssets)
        {
            if (entry.name == name)
            {
                foreach (AudioSource audioSource in _audioSources)
                {
                    if (!audioSource.isPlaying)
                    { 
                        audioSource.clip = entry.clip;
                        audioSource.PlayOneShot(entry.clip);
                        return;
                    }
                }
            }
        }
        UnityEngine.Debug.LogError("Clip " + name + " not found");
    }
    
    // takes one of the Options listed above and plays it once
    // returns the length of the played audio clip
    // this is useful for the coroutines who should wait for the duration of the clip
    public float PlaySoundFloat(SoundOptions name)
    {
        foreach (AudioClips entry in SoundAssets)
        {
            if (entry.name == name)
            {
                foreach (AudioSource audioSource in _audioSources)
                {
                    if (!audioSource.isPlaying)
                    { 
                        audioSource.clip = entry.clip;
                        audioSource.PlayOneShot(entry.clip);
                        return audioSource.clip.length;  
                    }
                }
            }
        }
        UnityEngine.Debug.LogError("Clip " + name + " not found");
        return 0;
    }
}
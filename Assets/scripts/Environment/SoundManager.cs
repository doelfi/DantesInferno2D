using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Plays sound clips based on the action the Player is taking in the game.
    // The function just takes the first that is not currently playing anything,
    // this makes it possible to play multiple sounds at once.

    private AudioSource[] _audioSources;
    
    // All possible sound effects.
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
    
    // Simulates a dictionary in the editor
    // since dictionaries can not be filled over the editor.
    [System.Serializable]
    public struct AudioClips
    {
        public SoundManager.SoundOptions name;
        public AudioClip clip;
    }
    
    public AudioClips[] SoundAssets;
    
    void Start()
    {
        // Gets the audio sources the play them.
        _audioSources = gameObject.GetComponents<AudioSource>();
    }
    

    public void PlaySound(SoundOptions name)
    {
        // Takes one of the options listed above and plays it once
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
    
    public float PlaySoundFloat(SoundOptions name)
    {
        // Takes one of the Options listed above and plays it once.
        // Returns the length of the played audio clip.
        // This is useful for the coroutines who should wait for the duration of the clip.
        
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
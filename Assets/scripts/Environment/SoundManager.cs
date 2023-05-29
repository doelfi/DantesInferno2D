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
    // an audio clip is always a combination of one of the possible options from above and the actual clip
    [System.Serializable]
    public struct AudioClips
    {
        public SoundManager.SoundOptions name;
        public AudioClip clip;
    }
    
    public AudioClips[] SoundAssets;
    
    void Start()
    {
        // Gets the list of audio sources to play from.
        _audioSources = gameObject.GetComponents<AudioSource>();
    }
    

    // goes through the list of audio sources and chooses the first that is not currently playing anything
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
    }
    
    // does the same as the one above but returns the length of the clip played as a float
    // this is helpful when making the game wait for the clip to finish (as with taking damage or reaching the goal)
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
        return 0;
    }
}
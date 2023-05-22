using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    
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
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    // takes one of the Options listed above and plays it once
    public void PlaySound(SoundOptions name)
    {
        foreach (AudioClips entry in SoundAssets)
        {
            if (entry.name == name)
            {
                audioSource.clip = entry.clip;
                audioSource.Play();
                return;
            }
        }
        UnityEngine.Debug.LogError("Clip " + name + " not found");
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
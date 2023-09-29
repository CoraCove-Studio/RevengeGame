using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudioClip(int clipIndex)
    {
        if (clipIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[clipIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip index is out of range.");
        }
    }

    public void StopAudioClip()
    {
        audioSource.Stop();
    }
}

// To play audio on start in a new script (attach to audio manager game object)
//{
//    private AudioSource audioSource;
//
//void Start()
//{
//    audioSource = GetComponent<AudioSource>();
//    audioSource.Play();
//}
//}


//to play on a specific scene (attach to empty game object)
//{
//public AudioClip audioClip;

//void Start()
//{
//    AudioSource.PlayClipAtPoint(audioClip, transform.position);
//}
//}


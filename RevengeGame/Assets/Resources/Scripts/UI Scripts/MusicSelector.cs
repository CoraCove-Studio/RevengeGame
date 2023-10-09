using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public AudioSource Track1;
    public AudioSource Track2;
    public AudioSource Track3;
    public int TrackSelector;
    public int TrackHistory;
    public int secondsToZero = 5;


    void Start()
    {
        TrackSelector = Random.Range(0, 3);

        if(TrackSelector == 0)
        {
            Track1.Play();
            TrackHistory = 1;
        }
        else if (TrackSelector == 1)
        {
            Track1.Play();
            TrackHistory = 2;
        }
        else if (TrackSelector == 2)
        {
            Track1.Play();
            TrackHistory = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Track1.isPlaying == false && Track2.isPlaying == false && Track3.isPlaying == false)
        {
            TrackSelector = Random.Range(0, 3);

            if(TrackSelector == 0 && TrackHistory != 1)
            {
                Track1.Play();
                TrackHistory = 1;
            }
            else if (TrackSelector == 1 && TrackHistory != 2)
            {
                Track2.Play();
                TrackHistory = 2;
            }
            if (TrackSelector == 2 && TrackHistory != 3)
            {
                Track3.Play();
                TrackHistory = 3;
            }
        }
    }

    public void MusicStop()
    {
        StartCoroutine(findAudioAndFadeOut());
    }

    IEnumerator findAudioAndFadeOut()
    {
        // Find Audio Music in scene
        AudioSource audioMusic = GameObject.Find("AudioManager").GetComponent<AudioSource>();

        // Check Music Volume and Fade Out
        while (audioMusic.volume > 0.1f)
        {
            audioMusic.volume -= Time.deltaTime / secondsToZero;
            yield return null;
        }

        // Make sure volume is set to 0
        audioMusic.volume = 0;

        // Stop Music
        audioMusic.Stop();

        // Destroy
        Destroy(this);
    }
}

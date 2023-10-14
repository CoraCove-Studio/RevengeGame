using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEnd : MonoBehaviour
{

    public int secondsToZero = 5;
    public MusicSelector musicSelectorComponent;
    public void MusicStop()
    {
        StartCoroutine(findAudioAndFadeOut());
    }

    IEnumerator findAudioAndFadeOut()
    {
        // Find Audio Music in scene
        AudioSource audioMusic = GameObject.FindGameObjectWithTag("MainMenuMusic").GetComponent<AudioSource>();
        GetComponent<AudioSource>().clip = audioMusic.clip;

        // Check Music Volume and Fade Out
        while (audioMusic.volume > 0.01f)
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

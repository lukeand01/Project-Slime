using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicUnit : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayMusic(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
        Invoke("Decay", clip.length + 0.5f);
    }

    void Decay()
    {
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _shared;
    public AudioClip  waterSound;
    private AudioSource mySource;
    // Start is called before the first frame
    void Start()
    {
        _shared = this;
        mySource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    public void PlaySound(string myAudio)
    {
        switch (myAudio)
        {
            case "waterSound":
                mySource.PlayOneShot(waterSound);
                break;
        }
    }

    public void StopSound()
    {
        ;
    }
}
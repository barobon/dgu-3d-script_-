using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip bgm;
    // Start is called before the first frame update
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = bgm;

        audio.volume = 1f;
        audio.loop = true;
        audio.mute = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

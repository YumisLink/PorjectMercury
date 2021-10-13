using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource Audio;
    float LifeTime;
    private void Start()
    {
        Audio.Play();
        LifeTime = Audio.clip.length;
    }
    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
            Destroy(gameObject);
    }
    public static Sound Play(AudioClip audioClip)
    {
        Sound s = GameManager.CreateAudio();
        s.Audio = s.GetComponent<AudioSource>();
        s.Audio.clip = audioClip;
        return s;
    }

}

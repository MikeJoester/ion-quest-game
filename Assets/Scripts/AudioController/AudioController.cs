using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Clip {
    public string name;
    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}

public class AudioController : MonoBehaviour
{
    public Clip[] soundClips;
    public static AudioController audioInstance;

    void Awake() {
        if (audioInstance == null) {
            audioInstance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach(Clip s in soundClips) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
        // if (SceneManager.GetActiveScene().buildIndex == 0) {
        //     playClip("MainMenu");
        // }
        // if (SceneManager.GetActiveScene().buildIndex == 1) {
        //     playClip("Scene1");
        // }
    }

    public void playClip(string name) {
        Clip s = Array.Find(soundClips, sound => sound.name == name);
        if (s == null)
            return;
        
        s.source.Play();
    }
}

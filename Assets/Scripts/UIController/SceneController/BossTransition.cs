using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BossTransition : MonoBehaviour
{
    public GameObject bossWall;
    public GameObject bossInfo;
    public AudioSource audioSource;
    public AudioClip clip;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            audioSource.clip = clip;
            audioSource.Play();
            bossWall.SetActive(true);
            bossInfo.SetActive(true);
        }
    }
}

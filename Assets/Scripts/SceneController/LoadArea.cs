using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour
{
    [SerializeField] string sceneName, exitPoint;
    [SerializeField] Animator doorAnim;
    private PlayerController player;


    void Start() {
        player = FindObjectOfType<PlayerController>();
        doorAnim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            player.startPoint = exitPoint;
            player.Fade(true);
            // sfxMan.playerEnter.Play();
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel() {
        if (doorAnim != null) {
            doorAnim.SetBool("isEnter", true);
        }

        Debug.Log($"Loaded {sceneName}!");
        yield return new WaitForSeconds(1);
        // SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour
{
    [SerializeField] string sceneName, exitPoint, transitionTrigger;
    [SerializeField] Animator doorAnim;
    [SerializeField] Animator sceneTransition;
    [SerializeField] float loadDelay;
    private PlayerController player;


    void Start() {
        player = FindObjectOfType<PlayerController>();
        doorAnim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            player.startPoint = exitPoint;
            player.setInteract = true;
            player.Fade(true);
            // sfxMan.playerEnter.Play();
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel() {
        if (doorAnim != null) {
            doorAnim.SetBool("isEnter", true);
        }
        
        yield return new WaitForSeconds(loadDelay);
        sceneTransition.SetTrigger(transitionTrigger);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        player.Fade(false);
        player.setInteract = false;
    }
}

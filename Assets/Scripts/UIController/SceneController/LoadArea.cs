using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour
{
    [SerializeField] string sceneName, exitPoint;
    [SerializeField] Animator doorAnim;
    [SerializeField] GameObject sceneTransition;
    [SerializeField] float loadDelay;
    private PlayerController player;
    private Animator sceneAnim;


    void Start() {
        player = FindObjectOfType<PlayerController>();
        doorAnim = GetComponent<Animator>();
        sceneAnim = sceneTransition.GetComponent<Animator>();
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

    public IEnumerator LoadLevel() {
        if (doorAnim != null) {
            doorAnim.SetBool("isEnter", true);
        }
        
        yield return new WaitForSeconds(loadDelay);
        if (sceneTransition != null) sceneTransition.SetActive(true);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);

        player.Fade(false);
        player.setInteract = false;
    }
}

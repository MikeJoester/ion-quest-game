using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    // public GameObject equipMenu;

    private Canvas canvas;

    void Start() {
        canvas = GetComponent<Canvas>();
    }
    
    void Update() {
        if ((Input.GetKeyDown(KeyCode.Escape)) && (SceneManager.GetActiveScene().buildIndex != 0)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        FindObjectOfType<AudioController>().playClip("Select");
        pauseMenuUI.SetActive(false);
        // equipMenu.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        canvas.sortingOrder = -1;
    }

    void Pause() {
        FindObjectOfType<AudioController>().playClip("Inventory");
        pauseMenuUI.SetActive(true);
        // equipMenu.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        canvas.sortingOrder = 1;
    }
}

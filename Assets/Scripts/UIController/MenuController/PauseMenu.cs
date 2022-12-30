using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject equipMenu;

    private Canvas canvas;

    void Start() {
        canvas = GetComponent<Canvas>();
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        equipMenu.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        canvas.sortingOrder = -1;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        equipMenu.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        canvas.sortingOrder = 1;
    }
}
